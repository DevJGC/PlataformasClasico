using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    
    [SerializeField] float moveDuration = 1f; // Duración de la animación de movimiento de la comida
    [SerializeField] private float energyBoost = 0.2f; // Esto determina cuánta energía se suma cuando el jugador recoge la comida.

    // referencia audio source
    private AudioSource audioSource;

    // referencia audio clip
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    // Si la comida toca al player se suma, se reproduce el sonido y se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // playsound
            audioSource.PlayOneShot(audioClip);
            MoveFoodToTopCenterAndDestroy();

            // Sumar energía al jugador
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript)
            {
                playerScript.AddEnergy(energyBoost);
            }
        }
    }

    // Mueve la comida a la parte superior central y la destruye
    private void MoveFoodToTopCenterAndDestroy()
    {
        // Mover el objeto a la parte superior central de la pantalla
        Vector2 topCenterScreenPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height)); // Obtener la posición de la parte superior central de la pantalla
        transform.DOLocalMove(topCenterScreenPosition, moveDuration).OnComplete(() => Destroy(gameObject)); //  Mueve la comida a la posición de la parte superior central de la pantalla y destruye la comida
    }
}
