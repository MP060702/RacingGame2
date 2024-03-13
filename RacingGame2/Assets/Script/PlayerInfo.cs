using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    Rigidbody rb;
    CarController carController;

    public Transform Center;
    [HideInInspector] public Transform TargetPoint;
    public Transform WayPoints;
    public int WayIndex;

    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float BreakForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Center.localPosition;

        carController = GetComponent<CarController>();

        TargetPoint = WayPoints.GetChild(WayIndex);
    }

    private void FixedUpdate()
    {
        float motor = 1000;
        float steering = 0;
        float Break = 0;

        motor = maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        Break = Input.GetKey(KeyCode.Space) ? BreakForce : 0;

        carController.CarControl(motor, steering, Break);

        if (Vector3.Distance(TargetPoint.position, transform.position) <= 30)
        {
            if (WayPoints.childCount > WayIndex)
                WayIndex++;

            if (WayIndex == WayPoints.childCount)
            {
                WayIndex = 0;
            }
            TargetPoint = WayPoints.GetChild(WayIndex);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            rb.AddForce(transform.forward * 20000, ForceMode.Impulse);
        }
    }
}
