using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoxDown : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float yLimit = -5.0f; // L�mite en Y para comenzar la animaci�n de regreso
    [SerializeField] float delayToReset = 2.0f; // Tiempo de espera antes de volver a la posici�n original
    [SerializeField] float durationToReset = 1.0f; // Duraci�n de la animaci�n de regreso

    private Vector3 initialPosition; // Guardar la posici�n inicial

    private bool onlyOne;// Variable para asegurarnos de que la explosi�n solo se ejecute una vez.

    void Start()
    {
        // Guardamos la posici�n inicial
        initialPosition = transform.position;
    }

    void Update()
    {
        // Si llegamos al l�mite y no est� en proceso de regreso
        if (transform.position.y <= yLimit && !DOTween.IsTweening(gameObject))
        {
            ReturnToInitialPosition();
        }
    }

    // Si toca con el player, se le aplica f�sica una vez
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !onlyOne)
        {
            rb.gravityScale = 1;
            rb.isKinematic = false;
            onlyOne = true;
        }
    }

    // Lo devuelve a la posici�n inicial    
    void ReturnToInitialPosition()
    {
        // Detenemos cualquier f�sica en la caja
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.isKinematic = true;

        // Despu�s de un retraso, mover la caja a su posici�n original usando DOTween
        transform.DOMove(initialPosition, durationToReset).SetDelay(delayToReset).OnComplete(() =>
        {
            onlyOne = false; // Reset the flag so the box can fall again if stepped on
        });
    }
}

