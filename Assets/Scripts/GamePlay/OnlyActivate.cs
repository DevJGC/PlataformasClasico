using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyActivate : MonoBehaviour
{
    // referencia component active
    [SerializeField] private Enemy enemy;

    bool oneTime = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ontrigger enter 2d tag player
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
