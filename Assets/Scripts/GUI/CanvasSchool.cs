using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSchool : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; //    referencia audio source
    [SerializeField] private AudioClip audioPlay; //    referencia audio clip

    [SerializeField] private AudioSource audioMusic; //   referencia audio source

    void Start()
    {
        // baja el volumen de la musica
        audioMusic.volume = 0;
        audioSource.PlayOneShot(audioPlay);
    }

    //  Si se pulsa Enter, sale al menú de niveles
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ContinueGame();
        }
    }

    //  va al menu de niveles
    public void ContinueGame()
    {

        SceneManager.LoadScene("MenuLevel");

    }

    //  sale al menu principal
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}