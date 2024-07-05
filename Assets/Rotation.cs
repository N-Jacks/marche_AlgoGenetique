using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotation : MonoBehaviour
{
    public GameObject target;
    public float a = 0.0f;
    // Start is called before the first frame update
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, Time.deltaTime * 50 * MathF.Sin(5*a));
        a = a + 0.01f;
    }
}
