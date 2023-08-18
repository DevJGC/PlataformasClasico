using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDown : MonoBehaviour
{
    // referencia al rigidbody
    [SerializeField] Rigidbody2D rb;

    bool onlyOne;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    // on collision enter 2d
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si el tag es player
        if (collision.gameObject.tag == "Player" && !onlyOne)
        {
            // set gravity scale 1
            rb.gravityScale = 1;
            // kinematic false
            rb.isKinematic = false;
            onlyOne = true;
        }
    }
}
