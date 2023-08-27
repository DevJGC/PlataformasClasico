using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // audio source
    [SerializeField] AudioSource audioSource;
    // audio clip explosionbarril
    [SerializeField] AudioClip audioClipExplosionBarrel;

    
    void Start()
    {

        // play sound
        audioSource.PlayOneShot(audioClipExplosionBarrel);

    }

    void Update()
    {
        
    }
}
