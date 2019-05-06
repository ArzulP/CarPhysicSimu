using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle

    private float maxMotorTorque = 4000; // maximum torque the motor can apply to wheel
    private float minMotorTorque = 20; // minimum torque applied when no pedal are pressed but motor is still rotating
    private float maxBreakTorque = 10000;
    private float minMotorBreak = -300;

    private float maxSteeringAngle = 30; // maximum steer angle the wheel can have

    private Rigidbody leftGround;
    private Rigidbody rightGround;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float accelerate = maxMotorTorque * Input.GetAxis("Accelerate");
        float decelerate = maxBreakTorque * Input.GetAxis("Decelerate");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        float velocity = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z); // current velocity of car

        if (accelerate == 0 && decelerate == 0 && velocity>1.5)
            accelerate = minMotorBreak;
        else if (decelerate == 0 && accelerate < minMotorTorque)
            accelerate = minMotorTorque;
        

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
