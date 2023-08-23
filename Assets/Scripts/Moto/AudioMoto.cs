using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMoto : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody2D ruedaRb; // Referencia al Rigidbody2D de la rueda

    [SerializeField] private float pitchMultiplier = 0.001f; // Multiplier to control how pitch changes relative to angular velocity

    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // Cambia el pitch basado en la velocidad angular. 
        // Usa Mathf.Abs para asegurarte de que la velocidad sea positiva. 
        // Puedes ajustar pitchMultiplier para controlar cuánto afecta la velocidad al pitch.
        audioSource.pitch = 1.0f + Mathf.Abs(ruedaRb.angularVelocity) * pitchMultiplier;
    }
}
