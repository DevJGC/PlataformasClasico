using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Player player; //  referencia al componente player
    [SerializeField] private Animator animator; //  referencia al componente animator
    private Rigidbody2D rb; //  referencia al componente rigidbody2d
    [SerializeField] private float upwardForce = 5f; // fuerza hacia arriba
    [SerializeField] private GameObject gameOverCanvas; //  referencia al canvas de game over
    [SerializeField] private GameObject principalCanvas; // referencia al canvas principal
    [SerializeField] private float delayBeforeGameOver = 2f; // tiempo antes de game over
    [SerializeField] private CameraFollow cameraFollow; //  referencia al componente camera follow
    [SerializeField] private ParallaxEffect parallaxEffect; //  referencia al componente parallax effect
    [SerializeField] private AudioSource audioSource; //    referencia al componente audio source
    [SerializeField] private AudioClip audioClipDie; // referencia al audio clip die
    [SerializeField] MusicGamePlay musicGamePlay; //    referencia al componente music gameplay

    private Vector3 originalPosition; //    referencia a la posici�n original del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //    referencia al componente rigidbody2d
        gameOverCanvas.SetActive(false); //   desactiva el canvas de game over
        originalPosition = transform.position; // Guarda la posici�n original del jugador.
    }

    //  el jugador muere
    public void Die()
    {
        audioSource.PlayOneShot(audioClipDie); //   reproduce el audio clip die
        animator.SetTrigger("isDie"); //    activa el trigger isDie
        player.enabled = false; //  desactiva el componente player
        rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse); //  a�ade fuerza hacia arriba
        Invoke("GameOver", delayBeforeGameOver); // invoca el m�todo GameOver
    }

    //  game over   
    private void GameOver()
    {
        musicGamePlay.MusicGameOver(); //  cambia la m�sica a game over
        gameOverCanvas.SetActive(true); //  activa el canvas de game over
        principalCanvas.SetActive(false); //    desactiva el canvas principal
        cameraFollow.enabled = false; //    desactiva el componente camera follow
        parallaxEffect.enabled = false; //  desactiva el componente parallax effect
        gameObject.SetActive(false); // desactiva el gameobject
    }

    //  respawn en el �ltimo checkpoint
    public void RespawnAtCheckpoint()
    {
        gameObject.SetActive(true); // activa el gameobject
        Vector3 respawnPosition = GameManager.Instance.GetLastCheckPointPosition(); //  referencia a la posici�n del �ltimo checkpoint

        //  si no hay checkpoint, la posici�n de respawn es la original
        if (respawnPosition == Vector3.zero)
            respawnPosition = originalPosition;

        transform.position = respawnPosition; //    la posici�n del jugador es la posici�n de respawn
        player.enabled = true; //   activa el componente player
        cameraFollow.enabled = true; //  activa el componente camera follow
        parallaxEffect.enabled = true; //   activa el componente parallax effect
        principalCanvas.SetActive(true); // Reactiva el canvas principal
        musicGamePlay.MusicOriginal();  // Reiniciar la m�sica al track original
    }


}
