using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float normalSize = 5.0f; // El tama�o "normal" de la c�mara (por ejemplo, cuando el jugador camina)
    [SerializeField] private float runningSize = 7.0f; // El tama�o de la c�mara cuando el jugador corre
    [SerializeField] private float transitionSpeed = 2.0f; // Velocidad a la que se cambiar� el tama�o de la c�mara

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

        // Cambiar el tama�o de la c�mara en funci�n de si el jugador est� corriendo
        float targetSize = playerScript.IsRunning() ? runningSize : normalSize;
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime);
    }
}
