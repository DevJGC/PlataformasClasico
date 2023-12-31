using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMoto : MonoBehaviour
{
    [SerializeField] private Transform target; //   referencia al jugador
    [SerializeField] private Camera mainCamera; //  referencia a la c�mara principal


    void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>(); // Si no se establece la c�mara principal, se establece en la c�mara que tiene este script
    }

    void Update()
    {
        // Seguir al jugador en el eje X
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
