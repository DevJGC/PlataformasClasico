using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectorRueda : MonoBehaviour
{
    [SerializeField] private int segundosSinTocar = 0;
    [SerializeField] private bool estaTocando = false;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    // audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    // prefab mas1
    [SerializeField] private GameObject mas1Prefab;

    void Start()
    {
        // Comenzamos la cuenta desde el inicio
        StartCoroutine(ContarSegundosSinTocar());
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            estaTocando = true;
            //segundosSinTocar = 0;
            //textMeshProUGUI.text = "";  // Ocultamos el texto cuando está tocando al enemigo
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            estaTocando = false;
        }
    }

    private IEnumerator ContarSegundosSinTocar()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);  // Primero esperamos 1 segundo

            if (!estaTocando)
            {
                segundosSinTocar++;
                textMeshProUGUI.text = segundosSinTocar.ToString();  // Mostramos el contador en el TextMeshProUGUI

                if (segundosSinTocar > 0)  // Evita que se reproduzca el sonido justo al inicio
                {
                    audioSource.PlayOneShot(audioClip);  // Reproducimos el sonido
                    //instantiate mas1
                    Instantiate(mas1Prefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

}
