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

    [SerializeField] private float pressedFrictionValue = 0.5f;  // Valor de fricci�n cuando se presiona Space

    private float originalFrictionTrasera; // Valor de fricci�n original de la rueda trasera
    private float originalFrictionDelantera; // Valor de fricci�n original de la rueda delantera

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

        // Guardar los valores originales de fricci�n
        originalFrictionTrasera = ruedaTraseraMaterial.friction; // referencia fricci�n rueda trasera
        originalFrictionDelantera = ruedaDelanteraMaterial.friction; // referencia fricci�n rueda delantera
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
            ChangeWheelFriction(pressedFrictionValue); // cambiar fricci�n (caballito)

        }
        else
        {
            RestoreOriginalFriction(); // restaurar fricci�n
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

            // A�adir fuerza a las ruedas
            ruedaTraseraRb.AddForce(Vector2.right * horizontal * forceAmount);
            ruedaDelanteraRb.AddForce(Vector2.right * horizontal * forceAmount);
        }

        // esto activa y desactiva el RB en tiempo de ejecuci�n para tomar los nuevos valores de los materiales f�sicos (fricci�n)
        Collider2D collider = ruedaTraseraRb.GetComponent<Collider2D>(); 
        collider.enabled = false;
        collider.enabled = true;

    }

    // Cambiar la fricci�n de las ruedas
    void ChangeWheelFriction(float friction)
    {
        ruedaTraseraMaterial.friction = friction;
        ruedaDelanteraMaterial.friction = friction;
    }

    // Restaurar la fricci�n original de las ruedas
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

        // Aumentar la fricci�n para detener la moto
        //ChangeWheelFriction(1.0f); // puedes ajustar este valor seg�n lo r�pido que quieras que se detenga

        // Opcionalmente, puedes tambi�n ajustar la velocidad lineal de las ruedas a 0 para detenerlas inmediatamente:
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

    // al salir de la escena, restaura los valores de fricci�n o materiales f�sicos para evitar errores en la siguiente escena
    void OnDestroy()
    {
        // Restaurar la fricci�n original
        RestoreOriginalFriction();
    }

}

