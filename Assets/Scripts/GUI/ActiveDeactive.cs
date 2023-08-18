using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDeactive : MonoBehaviour
{
    [SerializeField] private GameObject _active;

    // audio source
    [SerializeField] private AudioSource _soundSource;
    // audio clip
    [SerializeField] private AudioClip _audioClip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ontriggerenter2d tag player
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

    // ontriggerexit2d tag player
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
