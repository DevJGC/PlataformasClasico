using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private bool isGrounded; // si está en el suelo
    [SerializeField] private Transform groundCheck; // referencia al ground check
    [SerializeField] private float checkRadius; //  radio del ground check
    [SerializeField] private LayerMask whatIsGround; //  referencia a la layer ground

    // Player
    [SerializeField] GameObject player;

    // Velocidad, rango, dirección, rango de espera y rango de muerte
    [SerializeField] private float speed = 2f;  //  velocidad
    [SerializeField] private float attackRange = 1f; // rango de ataque
    [SerializeField] private float waitingRange = 3f; //    rango de espera
    [SerializeField] private float reactivationRange = 5f; //   rango de reactivación
    [SerializeField] private float killRange = 0.5f;    //  rango de muerte
    private float lastDirection = 1;    //  última dirección

    [SerializeField] private float minYHeight = -50f;   //  altura mínima

    // Estado del jugador
    private bool playerIsDead = false;

    // Posición de inicio del enemigo
    private Vector3 initialPosition;

    // Audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioAttack;

    // Animator
    [SerializeField] private Animator animator; //  referencia al animator para gestionar las animaciones

    // SpriteRenderer
    [SerializeField] private SpriteRenderer spriteRenderer; //  referencia al sprite renderer para gestionar el FlipX

    private void Start()
    {
        initialPosition = transform.position; //    guardar la posición inicial
    }

    private void Update()
    {
        CheckForFallenOff();    //  comprobar si se ha caído

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);  //  comprobar si está en el suelo
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position); //    calcular la distancia al jugador

        //  comprobar si el jugador está muerto
        if (playerIsDead && distanceToPlayer < reactivationRange)
        {
            playerIsDead = false;
        }

        //  comprobar si el jugador está en rango de espera
        if (!playerIsDead)
        {
            Move();
        }
    }

    // Movimiento del enemigo
    private void Move()
    {
        if (player == null) return; //  si el jugador no existe, salir

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position); //    calcular la distancia al jugador
        float direction = (player.transform.position.x - transform.position.x) > 0 ? 1 : -1; // calcular la dirección

        bool isGroundAhead = Physics2D.OverlapCircle(groundCheck.position + new Vector3(direction, 0) * 0.5f, checkRadius, whatIsGround); //    comprobar si hay suelo delante

        if (isGrounded || isGroundAhead || (distanceToPlayer < waitingRange && Physics2D.OverlapCircle(groundCheck.position + new Vector3(direction, 0) * 1f, checkRadius, whatIsGround))) //   comprobar si está en el suelo o hay suelo delante
        {
            //  comprobar si el jugador está en rango de espera
            if (distanceToPlayer > attackRange)
            {
                // Hacer flip en el sprite si es necesario
                if (direction == 1 && spriteRenderer.flipX || direction == -1 && !spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }

                // Mover al enemigo
                transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
                lastDirection = direction;

                // Cambiar animación a "Run"
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetTrigger("isAttack"); //   cambiar animación a "Attack"

                // Comprobar si el jugador está en rango de muerte
                if (distanceToPlayer <= killRange && !playerIsDead)
                {
                    KillPlayer(); //    matar al jugador
                }

                animator.SetBool("isRun", false); //    cambiar animación a "Run"
            }
        }
        else
        {
            // Si el enemigo no está en el suelo, se queda quieto
            animator.SetBool("isRun", false);
        }
    }

    //  mata al player
    private void KillPlayer()
    {
        PlayerDie playerDie = player.GetComponent<PlayerDie>(); //  referencia al script PlayerDie

        //  comprobar si el script existe
        if (playerDie != null)
        {
            playerDie.Die(); // matar al jugador
            playerIsDead = true; //  el jugador está muerto
            animator.SetBool("isRun", false); // El enemigo deja de correr después de matar al jugador

            Invoke("RetardoEnemySpawn", 2f); //     invocar el método RetardoEnemySpawn() después de 2 segundos

        }
    }

    //  método para retrasar el respawn del enemigo
    void RetardoEnemySpawn()
    {
        // Regresa al enemigo a su posición inicial
        transform.position = initialPosition;
    }

    //  Si el enemigo cae, se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DieZone"))
        {
            Destroy(gameObject);
        }
    }

    //  método para reproducir el sonido del cuchillo
    public void SoundSword()
    {
        audioSource.PlayOneShot(audioAttack);
    }

    // método para comprobar si el enemigo se ha caído
    private void CheckForFallenOff()
    {
        // si la posición del enemigo es menor que la altura mínima, se destruye
        if (transform.position.y <= minYHeight)
        {
            Destroy(gameObject);
        }
    }
}
