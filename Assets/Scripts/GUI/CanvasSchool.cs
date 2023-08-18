using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSchool : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioPlay;

    [SerializeField] private AudioSource audioMusic;

    void Start()
    {
        // baja el volumen de la musica
        audioMusic.volume = 0;
        audioSource.PlayOneShot(audioPlay);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ContinueGame();
        }
    }

    public void ContinueGame()
    {

        SceneManager.LoadScene("MenuLevel");

    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}