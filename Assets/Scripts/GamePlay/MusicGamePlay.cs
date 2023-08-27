using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGamePlay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Añade una referencia al componente AudioSource
    [SerializeField] private AudioClip audioGameOver; // Añade una referencia al clip de audio de Game Over
    [SerializeField] private AudioClip audioOriginal; // Añade una referencia al clip de audio original del juego

    private const string MUSIC_STATE_PREF = "MusicState"; //    Añade una constante para el nombre de la preferencia

    private void Start()
    {
        LoadMusicState(); //    Asegúrate de que la música esté activada o desactivada
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

    //  Método para cambiar a la música de Game Over
    public void MusicGameOver()
    {
        audioSource.clip = audioGameOver;
        audioSource.Play();
    }

    //  Método para guardar el estado de la música
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

    //  Método para cambiar el estado de la música
    public void MusicOriginal()
    {
        // Aquí, pon el código para reproducir la música original del juego.
        audioSource.clip = audioOriginal;
        audioSource.Play();
    }

}

