using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moto : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float forceAmount = 50f;
    [SerializeField] private WheelJoint2D ruedaTraseraJoint;
    [SerializeField] private WheelJoint2D ruedaDelanteraJoint;

    [SerializeField] private Rigidbody2D ruedaTraseraRb;
    [SerializeField] private Rigidbody2D ruedaDelanteraRb;

    [SerializeField] private PhysicsMaterial2D ruedaTraseraMaterial;
    [SerializeField] private PhysicsMaterial2D ruedaDelanteraMaterial;

    [SerializeField] private float pressedFrictionValue = 0.5f;  // Valor de fricción cuando se presiona Space

    private float originalFrictionTrasera;
    private float originalFrictionDelantera;

    private JointMotor2D motorTrasero;
    private JointMotor2D motorDelantero;
    private float horizontal;

    void Start()
    {
        motorTrasero = ruedaTraseraJoint.motor;
        motorDelantero = ruedaDelanteraJoint.motor;

        // Guardar los valores originales de fricción
        originalFrictionTrasera = ruedaTraseraMaterial.friction;
        originalFrictionDelantera = ruedaDelanteraMaterial.friction;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            ChangeWheelFriction(pressedFrictionValue);

        }
        else
        {
            RestoreOriginalFriction();
        }

    }

    void FixedUpdate()
    {
        if (horizontal != 0)
        {
            // Ajustar velocidad del motor
            motorTrasero.motorSpeed = horizontal * speed;
            motorDelantero.motorSpeed = horizontal * speed;

            ruedaTraseraJoint.motor = motorTrasero;
            ruedaDelanteraJoint.motor = motorDelantero;

            // Añadir fuerza a las ruedas
            ruedaTraseraRb.AddForce(Vector2.right * horizontal * forceAmount);
            ruedaDelanteraRb.AddForce(Vector2.right * horizontal * forceAmount);
        }


        Collider2D collider = ruedaTraseraRb.GetComponent<Collider2D>();
        collider.enabled = false;
        collider.enabled = true;

    }

    void ChangeWheelFriction(float friction)
    {
        ruedaTraseraMaterial.friction = friction;
        ruedaDelanteraMaterial.friction = friction;



    }

    void RestoreOriginalFriction()
    {
        ruedaTraseraMaterial.friction = originalFrictionTrasera;
        ruedaDelanteraMaterial.friction = originalFrictionDelantera;
    }
}

