using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMoto : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCamera;

   


    void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>();


    }

    void Update()
    {
        // Seguir al jugador en el eje X
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
