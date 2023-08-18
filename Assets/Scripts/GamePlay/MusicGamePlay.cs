using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGamePlay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioOriginal; // A�ade una referencia al clip de audio original del juego

    private const string MUSIC_STATE_PREF = "MusicState";

    private void Start()
    {
        LoadMusicState();
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

    public void MusicGameOver()
    {
        audioSource.clip = audioGameOver;
        audioSource.Play();
    }

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

    public void MusicOriginal()
    {
        // Aqu�, pon el c�digo para reproducir la m�sica original del juego.
        // Si est�s usando un AudioSource, podr�a ser algo como:
        audioSource.clip = audioOriginal;
        audioSource.Play();
    }

}
