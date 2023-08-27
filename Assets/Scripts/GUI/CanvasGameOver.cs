using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameOver : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; //    Referencia al componente AudioSource
    [SerializeField] private AudioClip audioPlay; //    Referencia al audio clip
    [SerializeField] private PlayerDie playerDie; // Referencia al script PlayerDie del jugador

    void Start()
    {
        audioSource.PlayOneShot(audioPlay); //  Reproduce el audio clip
    }

    void Update()
    {
        // Si se pulsa Enter, reinicia el juego
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    //  Reinicia el juego
    public void RestartGame()
    {
        playerDie.RespawnAtCheckpoint(); // Reinicia al jugador en el último checkpoint

        // Desactiva el canvas GameOver
        gameObject.SetActive(false);
    }

    //  Sale de la partida
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
