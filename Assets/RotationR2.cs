using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationR2 : MonoBehaviour
{
    [SerializeField] ScriptPhilibert Philibert;
    public GameObject targetR2;
    // Start is called before the first frame update
    void Update()
    {
        transform.RotateAround(targetR2.transform.position, Vector3.forward, Time.deltaTime * Philibert.angularSpeedR2);
    }
}
