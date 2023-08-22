using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // referencia prefab coin
    [SerializeField] private GameObject coinPrefab;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    // ontriggerenter2d tag DieZone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if tag DieZone
        if (other.CompareTag("DieZone"))
        {
            // launch coin
            LaunchCoin();
            // destroy gameobject
            Destroy(gameObject);
        }
    }

    // instantiate coin
    private void LaunchCoin()
    {
        // instantiate coin
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}
