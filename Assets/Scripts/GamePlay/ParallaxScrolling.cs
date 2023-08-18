using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform[] backgrounds; // Lista de todos los fondos y sus pre-fondos.
    private float[] parallaxScales; // Proporción del movimiento del jugador al movimiento del fondo.
    public float smoothing = 1f;   // Debe ser mayor que 0.

    public Transform player;       // Referencia al jugador
    private Vector3 previousPlayerPos;

    void Start()
    {
        previousPlayerPos = player.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousPlayerPos.x - player.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousPlayerPos = player.position;
    }
}
