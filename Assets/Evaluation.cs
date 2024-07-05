using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    public GameObject individu;
    public SpriteRenderer spritetete;
    public float distanceY;

    void Update()
    {
        distanceY = transform.position.y;
        if (distanceY < -2f) {
            echec();
        }
    }

    private void OnTriggerEnter(Collider lasercol)
    {
        echec();
    }

    void echec()
    {
        individu.SetActive(false);
    }
}