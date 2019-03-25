using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
    public GameObject focusOnObject;

    private Vector3 offset;
    
    public float smoothSpeed = 0.5f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        offset = transform.position - focusOnObject.transform.position;
    }

    void LateUpdate()
    {

        //transform.position = Vector3.SmoothDamp(transform.position, focusOnObject.transform.position + offset, ref velocity, smoothSpeed * Time.deltaTime);
        //transform.position = focusOnObject.transform.position + offset;
    }
}
