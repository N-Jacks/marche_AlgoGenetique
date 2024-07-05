using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class numeroGeneration : MonoBehaviour
{
    public AlgoGenetique algoGenetique;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (algoGenetique != null)
        {
            textMeshPro.text = "Numéro de génération : " + (algoGenetique.nbGeneration - 1).ToString();
        }
    }
}