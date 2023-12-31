using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOverTime : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleHumo; // Referencia al ParticleSystem de humo
    [SerializeField] private Rigidbody2D ruedaRb; // Referencia al Rigidbody2D de la rueda

    [SerializeField] private float emissionMultiplier = 0.001f; // Multiplier to control how emission changes

    void Start()
    {
        
    }

    void Update()
    {
        // Cambia la emisi�n basado en la velocidad angular. 
        // Usa Mathf.Abs para asegurarte de que la velocidad sea positiva. 
        // Puedes ajustar emissionMultiplier para controlar cu�nto afecta la velocidad a la emisi�n.
        var emission = particleHumo.emission;
        emission.rateOverTime = Mathf.Abs(ruedaRb.angularVelocity) * emissionMultiplier;


    }
}
