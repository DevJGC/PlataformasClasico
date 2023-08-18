using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    
    void Start()
    {
        Destroy(gameObject, timeToDestroy);

    }

    
    void Update()
    {
        
    }
}
