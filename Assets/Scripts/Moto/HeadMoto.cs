using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMoto : MonoBehaviour
{
    [SerializeField] GameObject cabezaDead;

    // audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // on trigger enter 2d tag enemy destroy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //active cabezaDead
            cabezaDead.SetActive(true);
            // play sound
            audioSource.PlayOneShot(audioClip);
            //Destroy(gameObject);
        }
    }

}
