using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moto : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private WheelJoint2D ruedaTraseraJoint;
    [SerializeField] private WheelJoint2D ruedaDelanteraJoint;

    private JointMotor2D motorTrasero;
    private JointMotor2D motorDelantero;
    private float horizontal;


    void Start()
    {
        motorTrasero = ruedaTraseraJoint.motor;
        motorDelantero = ruedaDelanteraJoint.motor;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if (horizontal != 0)
        {
            motorTrasero.motorSpeed = horizontal * speed;
            motorDelantero.motorSpeed = horizontal * speed;

            ruedaTraseraJoint.motor = motorTrasero;
            ruedaDelanteraJoint.motor = motorDelantero;
        }
    }

}
