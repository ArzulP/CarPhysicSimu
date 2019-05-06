using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    private CarController carController;
    private Vector3 originalPosition;
    private Vector3 originalAngle;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        originalPosition = transform.position;
        originalAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs inputs = new Inputs();
        inputs.accelerateInput = Input.GetAxis("Accelerate");
        inputs.decelerateInput = Input.GetAxis("Decelerate");
        inputs.horizontalInput = Input.GetAxis("Horizontal");
        inputs.reverseInput = Input.GetAxis("Reverse");

        if (Input.GetButton("Reset"))
        {
            transform.position = originalPosition;
            transform.eulerAngles = originalAngle;
        }

        carController.updateInputs(inputs);
    }
}

[System.Serializable]
public class Inputs
{
    public float accelerateInput;
    public float decelerateInput;
    public float horizontalInput;
    public float reverseInput;
}