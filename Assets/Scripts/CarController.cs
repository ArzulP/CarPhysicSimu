using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle

    public List<ParticleSystem> smokeSystems;

    private float maxMotorTorque = 4000; // maximum torque the motor can apply to wheel
    private float minMotorTorque = 20; // minimum torque applied when no pedal are pressed but motor is still rotating
    private float maxBreakTorque = 10000; // maximum break torque applied when breaking
    private float minMotorBreak = -300; // minimum motor torque applied when no acceleration is applied and the car is moving faster than the motor therefore letting the motor dictacte wheel rotation
    private float maxReverseTorque = -1000; // maximum torque applied when going in reverse

    private float maxSteeringAngle = 30; // maximum steer angle the wheel can have

    private Rigidbody leftGround;
    private Rigidbody rightGround;
    private Rigidbody rb;

    private Inputs inputs;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void updateInputs(Inputs newInputs)
    {
        inputs = newInputs;
    }

    public void FixedUpdate()
    {
        // value calculation code //

        float accelerate = maxMotorTorque * inputs.accelerateInput;
        float decelerate = maxBreakTorque * inputs.decelerateInput;
        float steering = maxSteeringAngle * inputs.horizontalInput;
        float reverse = maxReverseTorque * inputs.reverseInput;

        float velocity = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z); // current velocity of car

        float velocityAngle = (180 / (float)Math.PI) *  Mathf.Atan(rb.velocity.x / rb.velocity.z); // velocity in radiant

        // objective is to have it in degres between [0;360] but arctan function doesn't always give the right result between the 2 possibilities
        velocityAngle = velocityAngle < 0 ? velocityAngle + 360 : velocityAngle;
        velocityAngle = rb.velocity.z < 0 ? velocityAngle + 180 : velocityAngle;
        velocityAngle = velocityAngle > 360 ? velocityAngle - 360 : velocityAngle;

        float carAngle = rb.transform.eulerAngles.y; // expressed in degres

        float difference = Mathf.Abs(velocityAngle - carAngle);

        Boolean isGoingReverse = false;

        if (velocity < 1 && accelerate == 0) // when going reverse, must be close to neutral state
        {
            isGoingReverse = true;
            accelerate = reverse;
        }
        else if (accelerate == 0 && decelerate == 0 && velocity > 1.5) // simulate natural motor break
            accelerate = minMotorBreak;
        else if (decelerate == 0 && accelerate < minMotorTorque) // simulate natural motor rotation when turned on in a neutral state
            accelerate = minMotorTorque;
        
        updateParticles(difference, velocityAngle, isGoingReverse);

        // frictions code //

        foreach (AxleInfo axleInfo in axleInfos)
        {
            RaycastHit hit;


            if(Physics.Raycast(axleInfo.leftWheel.transform.position, Vector3.down, out hit, 2.0f))
                {
                leftGround = hit.transform.GetComponent<Rigidbody>();
                }

            if (Physics.Raycast(axleInfo.rightWheel.transform.position, Vector3.down, out hit, 2.0f))
                {
                rightGround = hit.transform.GetComponent<Rigidbody>();
                }
            setFrictions(axleInfo.leftWheel, leftGround);
            setFrictions(axleInfo.rightWheel, rightGround);

            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = accelerate;
                axleInfo.rightWheel.motorTorque = accelerate;
                axleInfo.leftWheel.brakeTorque = decelerate;
                axleInfo.rightWheel.brakeTorque = decelerate;
            }
        }
    }

    private void updateParticles(float difference, float velocityAngle, Boolean isGoingReverse)
    {
        foreach (ParticleSystem system in smokeSystems)
        {
            if(isGoingReverse)
                system.Stop();
            else if (difference > 15)
            {
                if (!system.isPlaying)
                    system.Play();
                else
                    system.GetComponent<followParent>().setRotation(velocityAngle);
            }
            else
                system.Stop();
        }
    }

    private void setFrictions(WheelCollider collider, Rigidbody groundBody)
    {
        WheelFrictionCurve forwardFrictionCurve = collider.forwardFriction;
        forwardFrictionCurve.stiffness = groundBody.drag;
        collider.forwardFriction = forwardFrictionCurve;

        WheelFrictionCurve SideFrictionCurve = collider.sidewaysFriction;
        SideFrictionCurve.stiffness = groundBody.angularDrag;
        collider.sidewaysFriction = SideFrictionCurve;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
