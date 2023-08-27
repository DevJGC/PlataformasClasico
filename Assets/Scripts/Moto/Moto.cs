using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moto : MonoBehaviour
{
    [SerializeField] public float speed = 10f; // velocidad
    [SerializeField] public float forceAmount = 50f; // fuerza
    [SerializeField] private WheelJoint2D ruedaTraseraJoint; // referencia rueda trasera
    [SerializeField] private WheelJoint2D ruedaDelanteraJoint; // referencia rueda delantera

    [SerializeField] private Rigidbody2D ruedaTraseraRb; // referencia rigidbody rueda trasera
    [SerializeField] private Rigidbody2D ruedaDelanteraRb; // referencia rigidbody rueda delantera

    [SerializeField] private PhysicsMaterial2D ruedaTraseraMaterial; // referencia material rueda trasera
    [SerializeField] private PhysicsMaterial2D ruedaDelanteraMaterial; // referencia material rueda delantera

    [SerializeField] private float pressedFrictionValue = 0.5f;  // Valor de fricción cuando se presiona Space

    private float originalFrictionTrasera; // Valor de fricción original de la rueda trasera
    private float originalFrictionDelantera; // Valor de fricción original de la rueda delantera

    private JointMotor2D motorTrasero; // motor rueda trasera
    private JointMotor2D motorDelantero; // motor rueda delantera 
    private float horizontal; // horizontal

    // bool meta
    [SerializeField] public bool meta = false;

    [SerializeField] public bool dead = false;

    void Start()
    {
        motorTrasero = ruedaTraseraJoint.motor; // referencia motor rueda trasera
        motorDelantero = ruedaDelanteraJoint.motor; // referencia motor rueda delantera

        // Guardar los valores originales de fricción
        originalFrictionTrasera = ruedaTraseraMaterial.friction; // referencia fricción rueda trasera
        originalFrictionDelantera = ruedaDelanteraMaterial.friction; // referencia fricción rueda delantera
    }

    void Update()
    {
        // if meta == true return
        if (meta || dead)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal"); // acelerar y frenar

        if (Input.GetKey(KeyCode.Space))
        {
            ChangeWheelFriction(pressedFrictionValue); // cambiar fricción (caballito)

        }
        else
        {
            RestoreOriginalFriction(); // restaurar fricción
        }

    }

    //  FixedUpdate se llama una vez por fotograma
    void FixedUpdate()
    {
        // if meta == true return
        if (meta || dead)
        {
            return;
        }

        //  si se acelera o frena
        if (horizontal != 0)
        {
            // Ajustar velocidad del motor
            motorTrasero.motorSpeed = horizontal * speed;
            motorDelantero.motorSpeed = horizontal * speed;

            // Asignar el motor a las ruedas
            ruedaTraseraJoint.motor = motorTrasero;
            ruedaDelanteraJoint.motor = motorDelantero;

            // Añadir fuerza a las ruedas
            ruedaTraseraRb.AddForce(Vector2.right * horizontal * forceAmount);
            ruedaDelanteraRb.AddForce(Vector2.right * horizontal * forceAmount);
        }

        // esto activa y desactiva el RB en tiempo de ejecución para tomar los nuevos valores de los materiales físicos (fricción)
        Collider2D collider = ruedaTraseraRb.GetComponent<Collider2D>(); 
        collider.enabled = false;
        collider.enabled = true;

    }

    // Cambiar la fricción de las ruedas
    void ChangeWheelFriction(float friction)
    {
        ruedaTraseraMaterial.friction = friction;
        ruedaDelanteraMaterial.friction = friction;
    }

    // Restaurar la fricción original de las ruedas
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

    // public function to return bool dead
    public bool GetDead()
    {
        return dead;
    }

    // al salir de la escena, restaura los valores de fricción o materiales físicos para evitar errores en la siguiente escena
    void OnDestroy()
    {
        // Restaurar la fricción original
        RestoreOriginalFriction();
    }

}

