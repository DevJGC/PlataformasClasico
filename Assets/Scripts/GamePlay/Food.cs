using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    // Duración de la animación de movimiento de la comida
    [SerializeField] float moveDuration = 1f;
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

    private void MoveFoodToTopCenterAndDestroy()
    {
        // Mover el objeto a la parte superior central de la pantalla
        Vector2 topCenterScreenPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height));
        transform.DOLocalMove(topCenterScreenPosition, moveDuration).OnComplete(() => Destroy(gameObject));
    }
}
