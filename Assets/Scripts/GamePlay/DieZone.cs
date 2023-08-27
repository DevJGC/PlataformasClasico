using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    // Si el player cae en la zona de abajo del todo, muere
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if player
        if (other.CompareTag("Player"))
        {
            // get component player die
            PlayerDie playerDie = other.GetComponent<PlayerDie>();  
            // if player die
            if (playerDie != null)
            {
                // call function die
                playerDie.Die();
            }
        }
    }
}
