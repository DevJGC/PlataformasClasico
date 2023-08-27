using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyActivate : MonoBehaviour
{
    // referencia component active
    [SerializeField] private Enemy enemy;

    bool oneTime = false; // solo una vez

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Si el jugador entra en el trigger, el enemigo se activa
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if tag player
        if (other.CompareTag("Player") && !oneTime)
        {
            // active enemy
            enemy.enabled = true;
            // one time
            oneTime = true;
        }
    }
}
