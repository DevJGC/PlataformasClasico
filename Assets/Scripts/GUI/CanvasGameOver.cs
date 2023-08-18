using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameOver : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioPlay;
    [SerializeField] private PlayerDie playerDie; // Referencia al script PlayerDie del jugador

    void Start()
    {
        audioSource.PlayOneShot(audioPlay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        playerDie.RespawnAtCheckpoint(); // Reinicia al jugador en el último checkpoint

        // Desactiva el canvas GameOver
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
