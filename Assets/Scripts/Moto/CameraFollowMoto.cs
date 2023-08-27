using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMoto : MonoBehaviour
{
    [SerializeField] private Transform target; //   referencia al jugador
    [SerializeField] private Camera mainCamera; //  referencia a la cámara principal


    void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>(); // Si no se establece la cámara principal, se establece en la cámara que tiene este script
    }

    void Update()
    {
        // Seguir al jugador en el eje X
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
