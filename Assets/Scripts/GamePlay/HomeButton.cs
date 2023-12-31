using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    // audio source
    [SerializeField] private AudioSource audioSource;
    // audio play
    [SerializeField] private AudioClip audioPlay;

    void Start()
    {
        
    }

    // Si se pulsa Escape, sale al men� principal
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HomeButtonPressed();
        }

    }

    // home button
    public void HomeButtonPressed()
    {
        // play audio
        audioSource.PlayOneShot(audioPlay);

        Invoke("HomeButtonAction", 0.1f);
    }

    // home button action
    void HomeButtonAction()
    {
        SceneManager.LoadScene("Menu");
    }


}
