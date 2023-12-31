using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCanvasImage : MonoBehaviour
{
    public Vector2 scrollSpeed = new Vector2(0.5f, 0); // Velocidad de desplazamiento. Puedes cambiar los valores para conseguir la direcci�n y velocidad que deseas.
    private RawImage rawImage; // Referencia al componente RawImage.

    private void Start()
    {
        rawImage = GetComponent<RawImage>(); // Obtener el componente RawImage.
    }

    private void Update()
    {
        if (rawImage == null) return;

        // Actualizamos la posici�n de uvRect basada en el tiempo y la velocidad de desplazamiento.
        Rect currentUVRect = rawImage.uvRect;
        currentUVRect.position += scrollSpeed * Time.deltaTime;

        // Asignamos el nuevo uvRect al RawImage
        rawImage.uvRect = currentUVRect;
    }
}
