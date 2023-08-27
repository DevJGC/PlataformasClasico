using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxDown : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float yLimit = -5.0f; // Límite en Y para comenzar la animación de regreso
    [SerializeField] float delayToReset = 2.0f; // Tiempo de espera antes de volver a la posición original
    [SerializeField] float durationToReset = 1.0f; // Duración de la animación de regreso

    private Vector3 initialPosition; // Guardar la posición inicial

    private bool onlyOne;// Variable para asegurarnos de que la explosión solo se ejecute una vez.

    void Start()
    {
        // Guardamos la posición inicial
        initialPosition = transform.position;
    }

    void Update()
    {
        // Si llegamos al límite y no está en proceso de regreso
        if (transform.position.y <= yLimit && !DOTween.IsTweening(gameObject))
        {
            ReturnToInitialPosition();
        }
    }

    // Si toca con el player, se le aplica física una vez
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !onlyOne)
        {
            rb.gravityScale = 1;
            rb.isKinematic = false;
            onlyOne = true;
        }
    }

    // Lo devuelve a la posición inicial    
    void ReturnToInitialPosition()
    {
        // Detenemos cualquier física en la caja
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.isKinematic = true;

        // Después de un retraso, mover la caja a su posición original usando DOTween
        transform.DOMove(initialPosition, durationToReset).SetDelay(delayToReset).OnComplete(() =>
        {
            onlyOne = false; // Reset the flag so the box can fall again if stepped on
        });
    }
}

