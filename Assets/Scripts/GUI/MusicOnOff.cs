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

    private const string MUSIC_STATE_PREF = "MusicState";

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
            audioSource.volume = 0;
            musicOnOffImage.SetActive(true);
        }
        else
        {
            audioSource.volume = 1;
            musicOnOffImage.SetActive(false);
        }
    }

    // music on/off button
    public void MusicOnOffButton()
    {
        // play sound tap
        audioSourceTap.PlayOneShot(audioClipTap);
        
        if (audioSource.volume == 0)
        {
            audioSource.volume = 1;
            musicOnOffImage.SetActive(false);
            PlayerPrefs.SetInt(MUSIC_STATE_PREF, 1);
        }
        else
        {
            audioSource.volume = 0;
            musicOnOffImage.SetActive(true);
            PlayerPrefs.SetInt(MUSIC_STATE_PREF, 0);
        }
        PlayerPrefs.Save();  // Guarda los cambios en PlayerPrefs
    }
}
