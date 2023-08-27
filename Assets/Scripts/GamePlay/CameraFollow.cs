using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // El objeto que la cámara seguirá
    [SerializeField] private Camera mainCamera; // La cámara principal
    [SerializeField] private float normalSize = 5.0f; // El tamaño "normal" de la cámara (por ejemplo, cuando el jugador camina)
    [SerializeField] private float runningSize = 7.0f; // El tamaño de la cámara cuando el jugador corre
    [SerializeField] private float transitionSpeed = 2.0f; // Velocidad a la que se cambiará el tamaño de la cámara

    private Player playerScript; // Suponiendo que tu script del jugador se llama "Player"

    // Si no se establece la cámara principal, se establece en la cámara que tiene este script
    void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>();

        playerScript = target.GetComponent<Player>();
    }

    // Seguir al jugador en el eje X y cambiar el tamaño de la cámara en función de si el jugador está corriendo
    void Update()
    {
        // Seguir al jugador en el eje X
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        // Cambiar el tamaño de la cámara en función de si el jugador está corriendo
        float targetSize = playerScript.IsRunning() ? runningSize : normalSize; // Si el jugador está corriendo, el tamaño de la cámara es el tamaño de funcionamiento, de lo contrario, es el tamaño normal
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime); // Cambiar el tamaño de la cámara suavemente
    }
}
