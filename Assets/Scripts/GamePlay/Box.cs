using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ontriggerenter2d tag DieZone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if tag DieZone
        if (other.CompareTag("DieZone"))
        {
            // destroy gameobject
            Destroy(gameObject);
        }
    }
}
