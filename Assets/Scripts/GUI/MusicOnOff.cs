using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnOff : MonoBehaviour
{
    // referencia a la imagen del canvas
    [SerializeField] GameObject musicOnOffImage;

    // referencia audiosource
    [SerializeField] AudioSource audioSource;

    // audio source tap
    [SerializeField] AudioSource audioSourceTap;
    // audio clip tap
    [SerializeField] AudioClip audioClipTap;

    private const string MUSIC_STATE_PREF = "MusicState"; //    Añade una constante para el nombre de la preferencia

    void Start()
    {
        LoadMusicState();
    }

    // Carga el estado de la música desde PlayerPrefs al inicio
    private void LoadMusicState()
    {
        int musicState = PlayerPrefs.GetInt(MUSIC_STATE_PREF, 1); // Por defecto, la música está activa
        if (musicState == 0)
        {
            audioSource.volume = 0; //  Desactiva el volumen
            musicOnOffImage.SetActive(true); // Activa la imagen de música desactivada
        }
        else
        {
            audioSource.volume = 1; //  Activa el volumen
            musicOnOffImage.SetActive(false); //    Desactiva la imagen de música desactivada
        }
    }

    // music on/off button
    public void MusicOnOffButton()
    {
        // play sound tap
        audioSourceTap.PlayOneShot(audioClipTap);
        
        if (audioSource.volume == 0)
        {
            audioSource.volume = 1; //  Activa el volumen
            musicOnOffImage.SetActive(false); //    Desactiva la imagen de música desactivada
            PlayerPrefs.SetInt(MUSIC_STATE_PREF, 1); // Guarda el estado de la música en 1
        }
        else
        {
            audioSource.volume = 0; //  Desactiva el volumen
            musicOnOffImage.SetActive(true); // Activa la imagen de música desactivada
            PlayerPrefs.SetInt(MUSIC_STATE_PREF, 0); // Guarda el estado de la música en 0
        }
        PlayerPrefs.Save();  // Guarda los cambios en PlayerPrefs
    }
}
