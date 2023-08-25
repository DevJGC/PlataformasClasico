using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField] private Moto moto;

    [SerializeField] private GameObject canvasSchool;

    [SerializeField] private GameObject timeCanvas;

    // sound
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private GameObject canvasPrincipal;

    [SerializeField] private GameObject contadorSegundos;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // ontriggerenter2d tag player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // set win true
            moto.meta = true;
           // moto.speed = 0f;
           // moto.forceAmount = 0f;
            moto.LlegadaMeta();
            Invoke("LlegadaMeta", 1f);
            // set timeCanvas active
            timeCanvas.SetActive(false);
            // play sound
            audioSource.PlayOneShot(audioClip);

            // disable component contadorSegundos
            contadorSegundos.GetComponent<DetectorRueda>().enabled = false;
        }
    }

    void LlegadaMeta()
    {
        // set canvasSchool active
        canvasSchool.SetActive(true);
        // set canvasPrincipal active
        canvasPrincipal.SetActive(false);
    }
}
