using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float runSpeed = 7.5f;
    [SerializeField] private float timeToStartRunning = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioJump;
    [SerializeField] private AudioClip[] audioPasos;
    [SerializeField] private Image energyImage;
    [SerializeField] private float energyRecoveryRate = 0.001f;

    private bool wasGrounded;
    private bool isRunning;
    private float walkingTime;

    [SerializeField] private Vector3 startPosition;

    public bool inSchool = false;

    void Start()
    {
        wasGrounded = true;
        Respawn();
    }

    void Update()
    {
        // if inSchool,sale
        if (inSchool)
        {
            return;
        }
        
        Move();
        Jump();
        UpdateJumpAnimation();
        RecoverEnergy();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float speed = isRunning ? runSpeed : moveSpeed;

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveInput * speed, GetComponent<Rigidbody2D>().velocity.y);

        // Flip sprite
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Mathf.Abs(moveInput) > 0.01f && isGrounded)
        {
            walkingTime += Time.deltaTime;

            if (walkingTime >= timeToStartRunning && energyImage.fillAmount > 0)
            {
                isRunning = true;
                energyImage.fillAmount = Mathf.Max(energyImage.fillAmount - 0.001f, 0);
            }

            if (energyImage.fillAmount <= 0 || !isRunning)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            else if (isRunning)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            walkingTime = 0;
            isRunning = false;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        if (energyImage.fillAmount <= 0)
        {
            isRunning = false;
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            audioSource.PlayOneShot(audioJump);
        }
    }

    void UpdateJumpAnimation()
    {
        if (wasGrounded && !isGrounded)
        {
            animator.SetTrigger("isJumping");
        }
        animator.SetBool("isGrounded", isGrounded);
        wasGrounded = isGrounded;
    }

    void RecoverEnergy()
    {
        if (!isRunning && energyImage.fillAmount < 1)
        {
            energyImage.fillAmount = Mathf.Min(energyImage.fillAmount + energyRecoveryRate * Time.deltaTime, 1);
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public void SoundPasos()
    {
        audioSource.PlayOneShot(audioPasos[Random.Range(0, audioPasos.Length)]);
    }


    public void AddEnergy(float amount)
    {
        energyImage.fillAmount = Mathf.Min(energyImage.fillAmount + amount, 1);
    }

    public void Respawn()
    {
        Vector3 respawnPosition = GameManager.Instance.GetLastCheckPointPosition();

        // Si respawnPosition es Vector3.zero, significa que no se ha activado ningún checkpoint en el nivel actual.
        // En ese caso, usaremos la posición inicial del jugador como posición de reaparición.
        if (respawnPosition == Vector3.zero)
        {
            respawnPosition = startPosition;
        }

        transform.position = respawnPosition;
    }



}
