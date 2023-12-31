using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGamePlay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // A�ade una referencia al componente AudioSource
    [SerializeField] private AudioClip audioGameOver; // A�ade una referencia al clip de audio de Game Over
    [SerializeField] private AudioClip audioOriginal; // A�ade una referencia al clip de audio original del juego

    private const string MUSIC_STATE_PREF = "MusicState"; //    A�ade una constante para el nombre de la preferencia

    private void Start()
    {
        LoadMusicState(); //    Aseg�rate de que la m�sica est� activada o desactivada
        PlayOriginalMusic(); // Aseg�rate de que la m�sica original est� sonando al comienzo
    }

    // M�todo para cambiar a la m�sica original
    public void PlayOriginalMusic()
    {
        if (audioSource.clip != audioOriginal)
        {
            audioSource.clip = audioOriginal;
            audioSource.Play();
        }
    }

    //  M�todo para cambiar a la m�sica de Game Over
    public void MusicGameOver()
    {
        audioSource.clip = audioGameOver;
        audioSource.Play();
    }

    //  M�todo para guardar el estado de la m�sica
    private void LoadMusicState()
    {
        int musicState = PlayerPrefs.GetInt(MUSIC_STATE_PREF, 1); // Por defecto, la m�sica est� activa
        if (musicState == 0)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1;
        }
    }

    //  M�todo para cambiar el estado de la m�sica
    public void MusicOriginal()
    {
        // Aqu�, pon el c�digo para reproducir la m�sica original del juego.
        audioSource.clip = audioOriginal;
        audioSource.Play();
    }

}

