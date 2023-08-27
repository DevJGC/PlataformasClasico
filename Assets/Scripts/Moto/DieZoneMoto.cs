using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZoneMoto : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {

    }

    // ontriggerenter 2d player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if player
        if (other.CompareTag("Player"))
        {
            // sin utilizar, porque no puede salirse por abajo de la pantalla
        }
    }
}