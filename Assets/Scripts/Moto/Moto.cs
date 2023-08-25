using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moto : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    [SerializeField] public float forceAmount = 50f;
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

    // bool meta
    [SerializeField] public bool meta = false;

    [SerializeField] public bool dead = false;

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
        // if meta == true return
        if (meta || dead)
        {
            return;
        }

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
        // if meta == true return
        if (meta || dead)
        {
            return;
        }

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

    // meta
    public void LlegadaMeta()
    {
        meta = true;

        // Desactivar los motores
        motorTrasero.motorSpeed = 0;
        motorDelantero.motorSpeed = 0;
        ruedaTraseraJoint.motor = motorTrasero;
        ruedaDelanteraJoint.motor = motorDelantero;

        // Aumentar la fricción para detener la moto
        //ChangeWheelFriction(1.0f); // puedes ajustar este valor según lo rápido que quieras que se detenga

        // Opcionalmente, puedes también ajustar la velocidad lineal de las ruedas a 0 para detenerlas inmediatamente:
        ruedaTraseraRb.velocity = Vector2.zero;
        ruedaDelanteraRb.velocity = Vector2.zero;
    }

    // public function to return bool meta
    public bool GetMeta()
    {
        return meta;
    }

    public bool GetDead()
    {
        return dead;
    }

    void OnDestroy()
    {
        // Restaurar la fricción original
        RestoreOriginalFriction();
    }

}

