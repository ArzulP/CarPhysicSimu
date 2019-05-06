using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make the wheel mesh position correspond to the collider position
/// </summary>
public class syncPositionFromCollider : MonoBehaviour
{
    public WheelCollider wheelCollider;

    private Vector3 center;
    private RaycastHit hit;
    private float rotationValue = 0;
    
    void Start()
    {
    }
    
    void Update()
    {
        center = wheelCollider.transform.TransformPoint(wheelCollider.center);

        // cast a ray bellow to see if the collider hits the ground
        if (Physics.Raycast(center, -wheelCollider.transform.up, out hit, wheelCollider.suspensionDistance + wheelCollider.radius))
        {
            transform.position = hit.point + (wheelCollider.transform.up * wheelCollider.radius);
        }
        else // the wheel is currently floating in the air, take the maximum distance
        {
            transform.position = center - (wheelCollider.transform.up * wheelCollider.suspensionDistance);
        }
    }
}
