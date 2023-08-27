using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDeactive : MonoBehaviour
{
    [SerializeField] private GameObject _active; // referencia gameobject active

    // audio source
    [SerializeField] private AudioSource _soundSource;
    // audio clip
    [SerializeField] private AudioClip _audioClip;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // si el trigger toda al player, activa al objeto referenciado
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if tag player
        if (other.CompareTag("Player"))
        {
            // active gameobject
            _active.SetActive(true);
            // playsound
            _soundSource.PlayOneShot(_audioClip);
        }
    }

    // Si sale del trigger, desactiva al objeto referenciado
    private void OnTriggerExit2D(Collider2D other)
    {
        // if tag player
        if (other.CompareTag("Player"))
        {
            // deactive gameobject
            _active.SetActive(false);
            // playsound
            _soundSource.PlayOneShot(_audioClip);
        }
    }
}
