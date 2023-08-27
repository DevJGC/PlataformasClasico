using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectorRueda : MonoBehaviour
{
    [SerializeField] public int segundosSinTocar = 0; // Segundos que lleva sin tocar el suelo
    [SerializeField] private bool estaTocando = false; // Si está tocando el suelo o no

    [SerializeField] private TextMeshProUGUI textMeshProUGUI; //    Texto que se muestra en pantalla

    // audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    // prefab mas1
    [SerializeField] private GameObject mas1Prefab; // prefab del +1

    [SerializeField] private Moto moto; // Referencia a la moto (script)

    void Start()
    {
        // Comenzamos la cuenta desde el inicio
        StartCoroutine(ContarSegundosSinTocar());
    }

    void Update()
    {

    }

    //  Si el player toda el suelo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            estaTocando = true; // Está tocando el suelo
            //segundosSinTocar = 0;
            //textMeshProUGUI.text = "";  // Ocultamos el texto cuando está tocando al enemigo
        }
    }

    // Si el player sale del suelo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            estaTocando = false;
        }
    }

    //  Corrutina que cuenta los segundos que lleva sin tocar el suelo
    private IEnumerator ContarSegundosSinTocar()
    {
        while (true)
        {
            if (moto.GetMeta() || moto.GetDead()) //    Si la moto está en meta o muerta, paramos la cuenta
            {
                break;
            }

            yield return new WaitForSeconds(1);  // Primero esperamos 1 segundo

            if (!estaTocando) //    Si no está tocando el suelo, aumentamos el contador
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
