using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField] private Moto moto; // referencia component moto (script moto)

    [SerializeField] private GameObject canvasSchool; // referencia canvasSchool (meta)

    [SerializeField] private GameObject timeCanvas; // referencia timeCanvas (meta)

    // sound
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private GameObject canvasPrincipal; // referencia canvasPrincipal (meta)

    [SerializeField] private GameObject contadorSegundos; // referencia contadorSegundos (meta)


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // si el trigger detecta que el player llega a la meta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // set win true
            moto.meta = true;
            // moto.speed = 0f;
            // moto.forceAmount = 0f;
            moto.LlegadaMeta(); //  call function LlegadaMeta
            Invoke("LlegadaMeta", 1f);
            // set timeCanvas active
            timeCanvas.SetActive(false);
            // play sound
            audioSource.PlayOneShot(audioClip);

            // disable component contadorSegundos
            contadorSegundos.GetComponent<DetectorRueda>().enabled = false; // disable component DetectorRueda
        }
    }

    // function LlegadaMeta (set canvasSchool active)
    void LlegadaMeta()
    {
        // set canvasSchool active
        canvasSchool.SetActive(true);
        // set canvasPrincipal active
        canvasPrincipal.SetActive(false);
    }
}
