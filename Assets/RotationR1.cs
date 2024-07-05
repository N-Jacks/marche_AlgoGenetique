using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationR1 : MonoBehaviour
{
    [SerializeField] ScriptPhilibert Philibert;
    public GameObject targetR1;
    // Start is called before the first frame update
    void Update()
    {
        transform.RotateAround(targetR1.transform.position, Vector3.forward, Time.deltaTime * Philibert.angularSpeedR1);
    }
}
