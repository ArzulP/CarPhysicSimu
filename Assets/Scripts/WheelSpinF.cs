using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinF : MonoBehaviour
{

    public WheelCollider wheelCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, wheelCollider.steerAngle - transform.localEulerAngles.z, transform.localEulerAngles.z);
        transform.Rotate(wheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}
