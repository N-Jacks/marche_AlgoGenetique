using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mvtLaser : MonoBehaviour
{
    private float speed = 4f;
    public BoxCollider lasercol;

    //Moves this GameObject 2 units a second in the forward direction
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
