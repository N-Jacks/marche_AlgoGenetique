using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationL2 : MonoBehaviour
{
    [SerializeField] ScriptPhilibert Philibert;
    public GameObject targetL2;
    // Start is called before the first frame update
    void Update()
    {
        transform.RotateAround(targetL2.transform.position, Vector3.forward, Time.deltaTime * Philibert.angularSpeedL2);
    }
}
