using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float upwardForce = 5f;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject principalCanvas;
    [SerializeField] private float delayBeforeGameOver = 2f;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private ParallaxEffect parallaxEffect;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipDie;
    [SerializeField] MusicGamePlay musicGamePlay;

    private Vector3 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverCanvas.SetActive(false);
        originalPosition = transform.position; // Guarda la posición original del jugador.
    }

    public void Die()
    {
        audioSource.PlayOneShot(audioClipDie);
        animator.SetTrigger("isDie");
        player.enabled = false;
        rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
        Invoke("GameOver", delayBeforeGameOver);
    }

    private void GameOver()
    {
        musicGamePlay.MusicGameOver();
        gameOverCanvas.SetActive(true);
        principalCanvas.SetActive(false);
        cameraFollow.enabled = false;
        parallaxEffect.enabled = false;
        gameObject.SetActive(false);
    }

    public void RespawnAtCheckpoint()
    {
        gameObject.SetActive(true);
        Vector3 respawnPosition = GameManager.Instance.GetLastCheckPointPosition();

        if (respawnPosition == Vector3.zero)
            respawnPosition = originalPosition;

        transform.position = respawnPosition;
        player.enabled = true;
        cameraFollow.enabled = true;
        parallaxEffect.enabled = true;
        principalCanvas.SetActive(true); // Reactiva el canvas principal
        musicGamePlay.MusicOriginal();  // Reiniciar la música al track original
    }


}
