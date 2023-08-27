using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadMoto : MonoBehaviour
{
    [SerializeField] GameObject cabezaDead; // cabeza del player

    // audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private Moto moto; // referencia a moto (script)

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // si la variable dead de moto es true, se activa la cabezaDead ( es que ha caido )
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //active cabezaDead
            cabezaDead.SetActive(true);
            // play sound
            audioSource.PlayOneShot(audioClip);
            //Destroy(gameObject);
            // restart level
            Invoke("RestartLevel", 1f);

            // set dead true
            moto.dead = true;

        }
    }

    // restart level
    private void RestartLevel()
    {
        // reload level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
