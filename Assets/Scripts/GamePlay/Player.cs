using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f; // velocidad de movimiento
    [SerializeField] private float runSpeed = 7.5f; //  velocidad de movimiento corriendo
    [SerializeField] private float superRunSpeed = 10.0f; //    velocidad de movimiento corriendo con superpoder
    [SerializeField] private float jumpForce = 5.0f; // fuerza de salto
    [SerializeField] private float superJumpForce = 8.0f; //    fuerza de salto con superpoder
    [SerializeField] private float timeToStartRunning = 2.0f; //    tiempo que tarda en empezar a correr
    [SerializeField] private bool isGrounded; //    si est� en el suelo
    [SerializeField] private Transform groundCheck; //  transform del objeto que comprueba si est� en el suelo
    [SerializeField] private float checkRadius; //  radio del objeto que comprueba si est� en el suelo
    [SerializeField] private LayerMask whatIsGround; // layer del suelo
    [SerializeField] private SpriteRenderer spriteRenderer; //  referencia al sprite renderer para gestionar el FlipX
    [SerializeField] private Animator animator; //  referencia al animator para gestionar las animaciones
    [SerializeField] private AudioSource audioSource; //    referencia al audio source para reproducir sonidos
    [SerializeField] private AudioClip audioJump; //    sonido de salto
    [SerializeField] private AudioClip[] audioPasos; // sonidos de pasos
    [SerializeField] private Image energyImage; //  imagen de la barra de energ�a
    [SerializeField] private float energyRecoveryRate = 0.001f; //  velocidad de recuperaci�n de energ�a

    private bool wasGrounded; //    si estaba en el suelo
    private bool isRunning; //  si est� corriendo
    private float walkingTime; //   tiempo que lleva andando

    [SerializeField] private Vector3 startPosition; //  posici�n de inicio

    public bool inSchool = false; //    si est� en la escuela

    void Start()
    {
        wasGrounded = true; //  estaba en el suelo
        Respawn(); //   respawn
        ConfigureUpgrades(); // configurar mejoras
    }

    void Update()
    {
        //  si est� en la escuela, no se puede mover
        if (inSchool)
        {
            return;
        }

        Move(); //  mover
        Jump(); //  saltar
        UpdateJumpAnimation(); //   actualizar animaci�n de salto
        RecoverEnergy(); //  recuperar energ�a
    }

    //  recuperar energ�a
    void ConfigureUpgrades()
    {
        if (PlayerPrefs.GetInt("SuperJumpBought", 0) == 1) //   si se ha comprado el super salto
        {
            jumpForce = superJumpForce; //  fuerza de salto = fuerza de salto con superpoder
        }

        if (PlayerPrefs.GetInt("SuperSpeedBought", 0) == 1) //  sie se ha comprado super velocidad  
        {
            runSpeed = superRunSpeed;   //  velocidad de movimiento corriendo = velocidad de movimiento corriendo con superpoder
        }
    }

    //  movimiento
    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); //   input horizontal
        float speed = isRunning ? runSpeed : moveSpeed; //  velocidad = si est� corriendo ? velocidad de movimiento corriendo : velocidad de movimiento

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * speed, GetComponent<Rigidbody2D>().velocity.y); //   velocidad = input horizontal * velocidad, velocidad vertical

        if (moveInput > 0) //   si el input horizontal es mayor que 0
        {
            spriteRenderer.flipX = false; //    no voltear sprite
        }
        else if (moveInput < 0) //  si el input horizontal es menor que 0
        {
            spriteRenderer.flipX = true; // voltear sprite
        }


        //  si se est� moviendo y est� en el suelo
        if (Mathf.Abs(moveInput) > 0.01f && isGrounded) 
        {
            walkingTime += Time.deltaTime; // suma el tiempo andando

            if (walkingTime >= timeToStartRunning && energyImage.fillAmount > 0) // si el tiempo andando es mayor que el tiempo que tarda en empezar a correr y la energ�a es mayor que 0
            {
                isRunning = true; //    est� corriendo
                energyImage.fillAmount = Mathf.Max(energyImage.fillAmount - 0.001f, 0); //  resta energia
            }

            if (energyImage.fillAmount <= 0 || !isRunning) //   si la energ�a es menor o igual que 0 o no est� corriendo
            {
                animator.SetBool("isWalking", true); //    est� andando
                animator.SetBool("isRunning", false); //   no est� corriendo
            }
            else if (isRunning) //  si est� corriendo
            {
                animator.SetBool("isRunning", true); //  est� corriendo
                animator.SetBool("isWalking", false); //    no est� andando
            }
        }
        else
        {
            walkingTime = 0; // tiempo andando = 0
            isRunning = false; //   no est� corriendo
            animator.SetBool("isWalking", false); //    no est� andando
            animator.SetBool("isRunning", false); //    no est� corriendo
        }

        if (energyImage.fillAmount <= 0) //  si la energ�a es menor o igual que 0
        {
            isRunning = false; //   no est� corriendo
        }
    }

    // salto
    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); //   est� en el suelo = hay un collider en el suelo

        if (isGrounded && Input.GetButtonDown("Jump")) //   si est� en el suelo y se pulsa el bot�n de saltar
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce); //   velocidad = velocidad horizontal, fuerza de salto
            audioSource.PlayOneShot(audioJump); //  sonido de salto
        }
    }

    //  animaci�n de salto
    void UpdateJumpAnimation()
    {
        //  si estaba en el suelo y no est� en el suelo
        if (wasGrounded && !isGrounded)
        {
            animator.SetTrigger("isJumping"); //    animaci�n de salto
        }
        animator.SetBool("isGrounded", isGrounded); //  est� en el suelo = est� en el suelo
        wasGrounded = isGrounded; //    estaba en el suelo = est� en el suelo
    }

    //  recuperar energ�a
    void RecoverEnergy()
    {
        //  si no est� corriendo y la energ�a es menor que 1
        if (!isRunning && energyImage.fillAmount < 1)
        {
            energyImage.fillAmount = Mathf.Min(energyImage.fillAmount + energyRecoveryRate * Time.deltaTime, 1); // energ�a = energ�a + tasa de recuperaci�n de energ�a * tiempo, 1
        }
    }

    //  devuelve si est� corriendo
    public bool IsRunning()
    {
        return isRunning;
    }

    //  sonido pasos
    public void SoundPasos()
    {
        audioSource.PlayOneShot(audioPasos[Random.Range(0, audioPasos.Length)]); //  sonido de pasos aleatorio
    }

    //  a�ade energ�a
    public void AddEnergy(float amount)
    {
        energyImage.fillAmount = Mathf.Min(energyImage.fillAmount + amount, 1); //  energ�a = energ�a + cantidad, 1
    }

    //  respawn
    public void Respawn()
    {
        Vector3 respawnPosition = GameManager.Instance.GetLastCheckPointPosition(); //  posici�n de respawn = posici�n del �ltimo checkpoint

        //  si no hay checkpoint
        if (respawnPosition == Vector3.zero)
        {
            respawnPosition = startPosition; // posici�n de respawn = posici�n de inicio
        }

        transform.position = respawnPosition; //    posici�n = posici�n de respawn
    }
}
