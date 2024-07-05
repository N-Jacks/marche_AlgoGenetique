using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationL1 : MonoBehaviour
{
    [SerializeField] ScriptPhilibert Philibert;
    public GameObject targetL1;
    // Start is called before the first frame update
    void Update()
    {
        transform.RotateAround(targetL1.transform.position, Vector3.forward, Time.deltaTime * Philibert.angularSpeedL1);
    }
}