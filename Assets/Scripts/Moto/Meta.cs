using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField] private Moto moto;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // ontriggerenter2d tag player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // set win true
            moto.meta = true;
           // moto.speed = 0f;
           // moto.forceAmount = 0f;
            moto.LlegadaMeta();
        }
    }
}
