using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followParent : MonoBehaviour
{
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void setRotation(float angle)
    {
        Vector3 vector = new Vector3();
        vector.y = angle;
        vector.y += 180;
        vector.y = vector.y > 360 ? vector.y - 360 : vector.y;

        var shape = particleSystem.shape;
        shape.rotation = vector;
    }
}
