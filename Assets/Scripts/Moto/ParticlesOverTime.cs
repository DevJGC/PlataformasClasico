using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOverTime : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleHumo;
    [SerializeField] private Rigidbody2D ruedaRb; // Referencia al Rigidbody2D de la rueda

    [SerializeField] private float emissionMultiplier = 0.001f; // Multiplier to control how emission changes

    void Start()
    {
        
    }

    void Update()
    {
        // Cambia la emisión basado en la velocidad angular. 
        // Usa Mathf.Abs para asegurarte de que la velocidad sea positiva. 
        // Puedes ajustar emissionMultiplier para controlar cuánto afecta la velocidad a la emisión.
        var emission = particleHumo.emission;
        emission.rateOverTime = Mathf.Abs(ruedaRb.angularVelocity) * emissionMultiplier;


    }
}
