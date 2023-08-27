using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform[] backgrounds; // Lista de todos los fondos y sus pre-fondos.
    private float[] parallaxScales; // Proporción del movimiento del jugador al movimiento del fondo.
    public float smoothing = 1f;   // Debe ser mayor que 0.

    public Transform player;       // Referencia al jugador
    private Vector3 previousPlayerPos; // Posición del jugador en el frame anterior.

    void Start()
    {
        previousPlayerPos = player.position; // La posición del frame anterior es la posición inicial del jugador.

        parallaxScales = new float[backgrounds.Length]; //  Asignar la proporción de paralaje correspondiente.

        for (int i = 0; i < backgrounds.Length; i++) // Recorrer todos los fondos...
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; //  ...establecer la proporción de paralaje correspondiente.
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++) // Recorrer todos los fondos...
        {
            float parallax = (previousPlayerPos.x - player.position.x) * parallaxScales[i]; //  ...el paralaje es el movimiento del jugador multiplicado por la escala de paralaje.

            float backgroundTargetPosX = backgrounds[i].position.x + parallax; //   ...el objetivo x es la posición actual más el paralaje.

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z); //   ...el objetivo es la posición actual con el objetivo x.

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime); //    ...suavemente mueve la posición actual hacia el objetivo.
        }

        previousPlayerPos = player.position; // La posición del frame anterior es la posición actual del jugador.
    }
}
