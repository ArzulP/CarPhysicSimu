using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle

    private float maxMotorTorque = 4000; // maximum torque the motor can apply to wheel
    private float minMotorTorque = 20; // minimum torque applied when no pedal are pressed but motor is still rotating
    private float maxBreakTorque = 10000;
    private float minBreakTorque = 1000;

    private float maxSteeringAngle = 30; // maximum steer angle the wheel can have

    public void FixedUpdate()
    {
        float accelerate = maxMotorTorque * Input.GetAxis("Accelerate");
        float decelerate = maxBreakTorque * Input.GetAxis("Decelerate");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        if (accelerate == 0 && decelerate == 0) // a revoir
            decelerate = minBreakTorque;

        if (decelerate == 0 && accelerate < minMotorTorque)
            accelerate = minMotorTorque;

        

        print("accelerate : " + accelerate);
        print("decelerate : " + decelerate);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            RaycastHit hit;
            float leftGroundDrag = 0;
            float rightGroundDrag = 0;


            if(Physics.Raycast(axleInfo.leftWheel.transform.position, Vector3.down, out hit, 2.0f))
                {
                leftGroundDrag = hit.transform.GetComponent<Rigidbody>().drag;
                }

            if (Physics.Raycast(axleInfo.rightWheel.transform.position, Vector3.down, out hit, 2.0f))
                {
                rightGroundDrag = hit.transform.GetComponent<Rigidbody>().drag;
                }
            setFrictions(axleInfo.leftWheel, leftGroundDrag);
            setFrictions(axleInfo.rightWheel, rightGroundDrag);

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

    private void setFrictions(WheelCollider collider, float friction)
    {
        WheelFrictionCurve forwardFrictionCurve = collider.forwardFriction;
        forwardFrictionCurve.stiffness = friction;
        collider.forwardFriction = forwardFrictionCurve;

        WheelFrictionCurve SideFrictionCurve = collider.sidewaysFriction;
        SideFrictionCurve.stiffness = friction;
        collider.sidewaysFriction = SideFrictionCurve;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
