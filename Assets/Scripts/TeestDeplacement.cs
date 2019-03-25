using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeestDeplacement : MonoBehaviour
{
    Rigidbody rb;

    public int intensity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        inputMovement *= intensity;

        rb.AddForce(inputMovement);
    }
}
