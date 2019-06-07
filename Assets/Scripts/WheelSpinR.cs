using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinR : MonoBehaviour
{
    public WheelCollider wheelCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(wheelCollider.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}
