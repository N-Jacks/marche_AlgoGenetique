using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScriptPhilibert : MonoBehaviour
{
    //Initialisation des listes de coefficients de Fourrier, qu'on appellera liste de Fourrier
    public float[] fourrierCosR1;
    public float[] fourrierSinR1;

    public float[] fourrierCosR2;
    public float[] fourrierSinR2;

    public float[] fourrierCosL1;
    public float[] fourrierSinL1;

    public float[] fourrierCosL2;
    public float[] fourrierSinL2;

    public float angularSpeedR1;
    public float angularSpeedR2;
    public float angularSpeedL1;
    public float angularSpeedL2;

    public GameObject core;
    public float xCore;

    private float t = 0.0f;

    // Start est appelee avant le premier appel de Update
    void Start()
    {

    }

    // Update est appelee une fois pas frame
    void Update()
    {
        FourrierR1(1f, t);
        FourrierR2(1f, t);
        FourrierL1(1f, t);
        FourrierL2(1f, t);
        xCore = core.transform.position.x;
        t = t + 0.01f;
    }

    //Calcul des 4 polynomes trigonométriques à chaque instant
    private void FourrierR1(float a, float t)
    {
        for (int i = 0; i < fourrierCosR1.Length; i++) {
            angularSpeedR1 = fourrierCosR1[i] * MathF.Cos(a * (i+1) * t) + fourrierSinR1[i] * MathF.Sin(a * (i+1) * t);
        }
    }

    private void FourrierR2(float a, float t)
    {
        for (int i = 0; i < fourrierCosR2.Length; i++) {
            angularSpeedR2 = fourrierCosR2[i] * MathF.Cos(a * (i+1) * t) + fourrierSinR2[i] * MathF.Sin(a * (i+1) * t);
        }
    }

    private void FourrierL1(float a, float t)
    {
        for (int i = 0; i < fourrierCosL1.Length; i++) {
            angularSpeedL1 = fourrierCosL1[i] * MathF.Cos(a * (i+1) * t) + fourrierSinL1[i] * MathF.Sin(a * (i+1) * t);
        }
    }

    private void FourrierL2(float a, float t)
    {
        for (int i = 0; i < fourrierCosL2.Length; i++) {
            angularSpeedL2 = fourrierCosL2[i] * MathF.Cos(a * (i+1) * t) + fourrierSinL2[i] * MathF.Sin(a * (i+1) * t);
        }
    }
}
