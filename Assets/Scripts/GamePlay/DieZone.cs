using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ontriggerenter 2d player
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
