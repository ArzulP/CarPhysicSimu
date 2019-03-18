using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeestDeplacement : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputMovement *= 100;

        rb.AddForce(inputMovement);
    }
}
