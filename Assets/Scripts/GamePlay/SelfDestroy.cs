using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float timeToDestroy; //    tiempo para destruir el gameobject

    void Start()
    {
        Destroy(gameObject, timeToDestroy); //  destruye el gameobject

    }

    
    void Update()
    {
        
    }
}
