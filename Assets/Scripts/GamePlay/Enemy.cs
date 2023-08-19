using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    // Player
    [SerializeField] GameObject player;

    // Velocidad, rango, dirección, rango de espera y rango de muerte
    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float waitingRange = 3f;
    [SerializeField] private float reactivationRange = 5f;
    [SerializeField] private float killRange = 0.5f;
    private float lastDirection = 1;

    [SerializeField] private float minYHeight = -50f;

    // Estado del jugador
    private bool playerIsDead = false;

    // Posición de inicio del enemigo
    private Vector3 initialPosition;

    // Audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioAttack;

    // Animator
    [SerializeField] private Animator animator;

    // SpriteRenderer
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        CheckForFallenOff();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (playerIsDead && distanceToPlayer < reactivationRange)
        {
            playerIsDead = false;
        }

        if (!playerIsDead)
        {
            Move();
        }
    }

    private void Move()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        float direction = (player.transform.position.x - transform.position.x) > 0 ? 1 : -1;

        bool isGroundAhead = Physics2D.OverlapCircle(groundCheck.position + new Vector3(direction, 0) * 0.5f, checkRadius, whatIsGround);

        if (isGrounded || isGroundAhead || (distanceToPlayer < waitingRange && Physics2D.OverlapCircle(groundCheck.position + new Vector3(direction, 0) * 1f, checkRadius, whatIsGround)))
        {
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
                animator.SetTrigger("isAttack");

                // Comprobar si el jugador está en rango de muerte
                if (distanceToPlayer <= killRange && !playerIsDead)
                {
                    KillPlayer();
                }

                animator.SetBool("isRun", false);
            }
        }
        else
        {
            // Si el enemigo no está en el suelo, se queda quieto
            animator.SetBool("isRun", false);
        }
    }

    private void KillPlayer()
    {
        PlayerDie playerDie = player.GetComponent<PlayerDie>();

        if (playerDie != null)
        {
            playerDie.Die();
            playerIsDead = true;
            animator.SetBool("isRun", false); // El enemigo deja de correr después de matar al jugador
            
            Invoke("RetardoEnemySpawn", 2f);

        }
    }

    void RetardoEnemySpawn()
    {
        // Regresa al enemigo a su posición inicial
        transform.position = initialPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DieZone"))
        {
            Destroy(gameObject);
        }
    }

    public void SoundSword()
    {
        audioSource.PlayOneShot(audioAttack);
    }

    private void CheckForFallenOff()
    {
        if (transform.position.y <= minYHeight)
        {
            Destroy(gameObject);
        }
    }
}
