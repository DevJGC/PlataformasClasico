using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // El objeto que la c�mara seguir�
    [SerializeField] private Camera mainCamera; // La c�mara principal
    [SerializeField] private float normalSize = 5.0f; // El tama�o "normal" de la c�mara (por ejemplo, cuando el jugador camina)
    [SerializeField] private float runningSize = 7.0f; // El tama�o de la c�mara cuando el jugador corre
    [SerializeField] private float transitionSpeed = 2.0f; // Velocidad a la que se cambiar� el tama�o de la c�mara

    private Player playerScript; // Suponiendo que tu script del jugador se llama "Player"

    // Si no se establece la c�mara principal, se establece en la c�mara que tiene este script
    void Start()
    {
        if (!mainCamera)
            mainCamera = GetComponent<Camera>();

        playerScript = target.GetComponent<Player>();
    }

    // Seguir al jugador en el eje X y cambiar el tama�o de la c�mara en funci�n de si el jugador est� corriendo
    void Update()
    {
        // Seguir al jugador en el eje X
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        // Cambiar el tama�o de la c�mara en funci�n de si el jugador est� corriendo
        float targetSize = playerScript.IsRunning() ? runningSize : normalSize; // Si el jugador est� corriendo, el tama�o de la c�mara es el tama�o de funcionamiento, de lo contrario, es el tama�o normal
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime); // Cambiar el tama�o de la c�mara suavemente
    }
}
