using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float normalSize = 5.0f; // El tamaño "normal" de la cámara (por ejemplo, cuando el jugador camina)
    [SerializeField] private float runningSize = 7.0f; // El tamaño de la cámara cuando el jugador corre
    [SerializeField] private float transitionSpeed = 2.0f; // Velocidad a la que se cambiará el tamaño de la cámara

    private Player playerScript; // Suponiendo que tu script del jugador se llama "Player"

    void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>();

        playerScript = target.GetComponent<Player>();
    }

    void Update()
    {
        // Seguir al jugador en el eje X
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        // Cambiar el tamaño de la cámara en función de si el jugador está corriendo
        float targetSize = playerScript.IsRunning() ? runningSize : normalSize;
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime);
    }
}
