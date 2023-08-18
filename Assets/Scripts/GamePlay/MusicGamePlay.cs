using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGamePlay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioOriginal; // Añade una referencia al clip de audio original del juego

    private const string MUSIC_STATE_PREF = "MusicState";

    private void Start()
    {
        LoadMusicState();
        PlayOriginalMusic(); // Asegúrate de que la música original esté sonando al comienzo
    }

    // Método para cambiar a la música original
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
        int musicState = PlayerPrefs.GetInt(MUSIC_STATE_PREF, 1); // Por defecto, la música está activa
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
        // Aquí, pon el código para reproducir la música original del juego.
        // Si estás usando un AudioSource, podría ser algo como:
        audioSource.clip = audioOriginal;
        audioSource.Play();
    }

}

