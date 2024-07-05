using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

public class AlgoGenetique : MonoBehaviour
{
    public int n = 5;
    public int nbGeneration = 0;
    public int nbPhilibert = 0;
    public int nbTot = 30;
    public int p;

    public GameObject PhilibertPrefab;
    public GameObject[] PhilibertTab;
    public GameObject[] PhilibertTabCopy;
    public float[][][] PhilibertsComponents;

    public GameObject laser;

    public GameObject philibertWinner;
    public float xWin;
    public float[] fourrierCosR1Win;
    public float[] fourrierSinR1Win;

    public float[] fourrierCosR2Win;
    public float[] fourrierSinR2Win;

    public float[] fourrierCosL1Win;
    public float[] fourrierSinL1Win;

    public float[] fourrierCosL2Win;
    public float[] fourrierSinL2Win;

    public int iWin = 0;

    public List<float> xWinList = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        fourrierCosR1Win = new float[n];
        fourrierSinR1Win = new float[n];

        fourrierCosR2Win = new float[n];
        fourrierSinR2Win = new float[n];

        fourrierCosL1Win = new float[n];
        fourrierSinL1Win = new float[n];

        fourrierCosL2Win = new float[n];
        fourrierSinL2Win = new float[n];
    }

    // Update is called once per frame
    void Update()
    {
        nbPhilibert = 0;
        PhilibertTab = GameObject.FindGameObjectsWithTag("Philibert");
        foreach (GameObject philib in PhilibertTab)
        {
                nbPhilibert += 1;
        }

        if (nbGeneration == 0) {
            generation();
        }

        if (nbGeneration >= 1 && PhilibertTab.Length == 0) {
            philibertWinner = PhilibertTabCopy[0];
            ScriptPhilibert philibertScript = philibertWinner.GetComponent<ScriptPhilibert>();
            xWin = philibertScript.xCore;

            ScriptPhilibert philibvar = PhilibertTabCopy[0].GetComponent<ScriptPhilibert>();

            //Sélectionne le philibert vainqueur
            foreach (GameObject philibert in PhilibertTabCopy) {
                philibvar = philibert.GetComponent<ScriptPhilibert>();
                if (philibvar.xCore > xWin) {
                    philibertWinner = philibert;
                    xWin = philibvar.xCore;
                }
            }
            xWinList.Add(xWin);

            if (xWin > 300f) {
                reussite(xWinList);
                EditorApplication.ExitPlaymode();
            } else {
                Selection();
                croisement_et_mutation();
                generation();
            }
        }
    }


    void generation()
    {
        nbPhilibert = 0;
        laser.transform.position = new Vector3(-25,5,0); //Remet le laser à sa position de base

        if (nbGeneration == 0) {
            PhilibertsComponents = new float[nbTot][][];

            for (int p = 0; p < nbTot; p++)
            {
                PhilibertsComponents[p] = new float[8][];
                for (int i = 0; i < 8; i++)
                {
                    PhilibertsComponents[p][i] = new float[n];
                    for (int j = 0; j < n; j++)
                    {
                        PhilibertsComponents[p][i][j] = UnityEngine.Random.Range(-200f, 200f);
                    }
                }
                // PhilibertsComponents[p][0][0] = 60f;
                // PhilibertsComponents[p][0][1] = 192f;
                // PhilibertsComponents[p][0][2] = -17f;
                // PhilibertsComponents[p][0][3] = 24f;
                // PhilibertsComponents[p][0][4] = -72f;

                // PhilibertsComponents[p][1][0] = -10f;
                // PhilibertsComponents[p][1][1] = -257f;
                // PhilibertsComponents[p][1][2] = 96f;
                // PhilibertsComponents[p][1][3] = -47f;
                // PhilibertsComponents[p][1][4] = -92f;

                // PhilibertsComponents[p][2][0] = 59f;
                // PhilibertsComponents[p][2][1] = 64f;
                // PhilibertsComponents[p][2][2] = -34f;
                // PhilibertsComponents[p][2][3] = 5f;
                // PhilibertsComponents[p][2][4] = 246f;

                // PhilibertsComponents[p][3][0] = -40f;
                // PhilibertsComponents[p][3][1] = -46f;
                // PhilibertsComponents[p][3][2] = -28f;
                // PhilibertsComponents[p][3][3] = 36f;
                // PhilibertsComponents[p][3][4] = 50f;

                // PhilibertsComponents[p][4][0] = 79f;
                // PhilibertsComponents[p][4][1] = 84f;
                // PhilibertsComponents[p][4][2] = 102f;
                // PhilibertsComponents[p][4][3] = 94f;
                // PhilibertsComponents[p][4][4] = -133f;

                // PhilibertsComponents[p][5][0] = 104f;
                // PhilibertsComponents[p][5][1] = -49f;
                // PhilibertsComponents[p][5][2] = 153f;
                // PhilibertsComponents[p][5][3] = -21f;
                // PhilibertsComponents[p][5][4] = 86f;

                // PhilibertsComponents[p][6][0] = 73f;
                // PhilibertsComponents[p][6][1] = -45f;
                // PhilibertsComponents[p][6][2] = 62f;
                // PhilibertsComponents[p][6][3] = 62f;
                // PhilibertsComponents[p][6][4] = 133f;

                // PhilibertsComponents[p][7][0] = -97f;
                // PhilibertsComponents[p][7][1] = 27f;
                // PhilibertsComponents[p][7][2] = 235f;
                // PhilibertsComponents[p][7][3] = 17f;
                // PhilibertsComponents[p][7][4] = -71f;
            }
        }

        //Generer les 30 nouveaux prefabs Philibert
        for (int i = 0; i < nbTot; i++) {
            //Instantiate(PhilibertPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            GameObject philibertInstance = Instantiate(PhilibertPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            ScriptPhilibert philibertFourrier = philibertInstance.GetComponent<ScriptPhilibert>();

            philibertFourrier.fourrierCosR1 = DeepCopyArray(PhilibertsComponents[i][0]);
            philibertFourrier.fourrierSinR1 = DeepCopyArray(PhilibertsComponents[i][1]);

            philibertFourrier.fourrierCosR2 = DeepCopyArray(PhilibertsComponents[i][2]);
            philibertFourrier.fourrierSinR2 = DeepCopyArray(PhilibertsComponents[i][3]);

            philibertFourrier.fourrierCosL1 = DeepCopyArray(PhilibertsComponents[i][4]);
            philibertFourrier.fourrierSinL1 = DeepCopyArray(PhilibertsComponents[i][5]);

            philibertFourrier.fourrierCosL2 = DeepCopyArray(PhilibertsComponents[i][6]);
            philibertFourrier.fourrierSinL2 = DeepCopyArray(PhilibertsComponents[i][7]);

            if (nbGeneration >= 1 && i == iWin) {
                SpriteRenderer[] childSpriteRenderers = philibertInstance.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer childRenderer in childSpriteRenderers) {
                    childRenderer.color = new Color(1f, 0.5f, 0f, 100f/255f);
                }
                Vector3 prefabPosition = philibertInstance.transform.position;
                prefabPosition.z = -0.01f;
                philibertInstance.transform.position = prefabPosition;
            }
        }

        PhilibertTab = GameObject.FindGameObjectsWithTag("Philibert"); //Fait la liste de tous les Philiberts vivants
        PhilibertTabCopy = new GameObject[PhilibertTab.Length];
        Array.Copy(PhilibertTab, PhilibertTabCopy, PhilibertTab.Length);

        foreach (GameObject philib in PhilibertTab)
        {
            nbPhilibert += 1; //compte le nombre de Philiberts vivants
        }

        nbGeneration += 1;
    }


    void Selection() 
    {
        ScriptPhilibert philibertScript = philibertWinner.GetComponent<ScriptPhilibert>();
        ScriptPhilibert philibvar = PhilibertTabCopy[0].GetComponent<ScriptPhilibert>();

        philibertScript = philibertWinner.GetComponent<ScriptPhilibert>();

        fourrierCosR1Win = DeepCopyArray(philibertScript.fourrierCosR1);
        fourrierSinR1Win = DeepCopyArray(philibertScript.fourrierSinR1);

        fourrierCosR2Win = DeepCopyArray(philibertScript.fourrierCosR2);
        fourrierSinR2Win = DeepCopyArray(philibertScript.fourrierSinR2);

        fourrierCosL1Win = DeepCopyArray(philibertScript.fourrierCosL1);
        fourrierSinL1Win = DeepCopyArray(philibertScript.fourrierSinL1);

        fourrierCosL2Win = DeepCopyArray(philibertScript.fourrierCosL2);
        fourrierSinL2Win = DeepCopyArray(philibertScript.fourrierSinL2);

        for (int i = 0; i < PhilibertTabCopy.Length; i++) {
            PhilibertsComponents[i] = new float[8][];
            philibvar = PhilibertTabCopy[i].GetComponent<ScriptPhilibert>();

            PhilibertsComponents[i][0] = DeepCopyArray(philibvar.fourrierCosR1);
            PhilibertsComponents[i][1] = DeepCopyArray(philibvar.fourrierSinR1);
            PhilibertsComponents[i][2] = DeepCopyArray(philibvar.fourrierCosR2);
            PhilibertsComponents[i][3] = DeepCopyArray(philibvar.fourrierSinR2);
            PhilibertsComponents[i][4] = DeepCopyArray(philibvar.fourrierCosL1);
            PhilibertsComponents[i][5] = DeepCopyArray(philibvar.fourrierSinL1);
            PhilibertsComponents[i][6] = DeepCopyArray(philibvar.fourrierCosL2);
            PhilibertsComponents[i][7] = DeepCopyArray(philibvar.fourrierSinL2);
        }
    }


    void croisement_et_mutation()
    {
        for (int i = 0; i < PhilibertTabCopy.Length; i++) {
            if (PhilibertsComponents[i][0][0] != fourrierCosR1Win[0]) {
                for (int j = 0; j < n; j++) {
                    p = UnityEngine.Random.Range(0, 39);
                    if (p == 0) {
                        PhilibertsComponents[i][0][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierCosR1Win[j] + PhilibertsComponents[i][0][j])/2;
                        PhilibertsComponents[i][1][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierSinR1Win[j] + PhilibertsComponents[i][1][j])/2;
                        PhilibertsComponents[i][2][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierCosR2Win[j] + PhilibertsComponents[i][2][j])/2;
                        PhilibertsComponents[i][3][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierSinR2Win[j] + PhilibertsComponents[i][3][j])/2;
                        PhilibertsComponents[i][4][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierCosL1Win[j] + PhilibertsComponents[i][4][j])/2;
                        PhilibertsComponents[i][5][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierSinL1Win[j] + PhilibertsComponents[i][5][j])/2;
                        PhilibertsComponents[i][6][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierCosL2Win[j] + PhilibertsComponents[i][6][j])/2;
                        PhilibertsComponents[i][7][j] = GenerateRandomGaussian(0.5f, 1.5f) * (fourrierSinL2Win[j] + PhilibertsComponents[i][7][j])/2;
                    }
                }
            } else {
                iWin = i;
            }
        }

        foreach (GameObject philib in PhilibertTabCopy) {
            Destroy(philib);
        }
    }



    // Fonction pour effectuer une copie profonde d'un tableau de floats
    float[] DeepCopyArray(float[] array)
    {
        float[] newArray = new float[array.Length];
        Array.Copy(array, newArray, array.Length);
        return newArray;
    }

    //faire un nombre entre 0.5 et 1.5 avec distribution selon une loi normale
    private static System.Random random = new System.Random();

    public static void Main()
    {
        float minValue = 0.5f;
        float maxValue = 1.5f;

        float randomNumber = GenerateRandomGaussian(minValue, maxValue);
        // Console.WriteLine(randomNumber);
    }

    private static float GenerateRandomGaussian(float minValue, float maxValue)
    {
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();
        double normalRandomValue = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

        float mean = 1.0f;
        float standardDeviation = 0.5f;

        float randomGaussian = mean + (float)(standardDeviation * normalRandomValue);

        // Clamp the value within the specified range
        randomGaussian = Math.Clamp(randomGaussian, minValue, maxValue);

        return randomGaussian;
    }


    private void reussite(List<float> xWinList)
    {
        // Déterminez le nom de fichier que vous souhaitez utiliser pour votre fichier CSV
        string fileName = "xWinData.csv";

        // Utilisez Application.persistentDataPath pour obtenir le chemin complet du dossier persistant
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Créez le contenu du fichier CSV
        StringBuilder csvContent = new StringBuilder();
        for (int i = 0; i < xWinList.Count; i++)
        {
            csvContent.AppendLine("Generation " + i + "," + xWinList[i]);
        }
        csvContent.AppendLine("CosR1 " + "," + fourrierCosR1Win[0] + "," + fourrierCosR1Win[1] + "," + fourrierCosR1Win[2] + "," + fourrierCosR1Win[3] + "," + fourrierCosR1Win[4]);
        csvContent.AppendLine("SinR1 " + "," + fourrierSinR1Win[0] + "," + fourrierSinR1Win[1] + "," + fourrierSinR1Win[2] + "," + fourrierSinR1Win[3] + "," + fourrierSinR1Win[4]);

        csvContent.AppendLine("CosR2 " + "," + fourrierCosR2Win[0] + "," + fourrierCosR2Win[1] + "," + fourrierCosR2Win[2] + "," + fourrierCosR2Win[3] + "," + fourrierCosR2Win[4]);
        csvContent.AppendLine("SinR2 " + "," + fourrierSinR2Win[0] + "," + fourrierSinR2Win[1] + "," + fourrierSinR2Win[2] + "," + fourrierSinR2Win[3] + "," + fourrierSinR2Win[4]);

        csvContent.AppendLine("CosL1 " + "," + fourrierCosL1Win[0] + "," + fourrierCosL1Win[1] + "," + fourrierCosL1Win[2] + "," + fourrierCosL1Win[3] + "," + fourrierCosL1Win[4]);
        csvContent.AppendLine("SinL1 " + "," + fourrierSinL1Win[0] + "," + fourrierSinL1Win[1] + "," + fourrierSinL1Win[2] + "," + fourrierSinL1Win[3] + "," + fourrierSinL1Win[4]);

        csvContent.AppendLine("CosL2 " + "," + fourrierCosL2Win[0] + "," + fourrierCosL2Win[1] + "," + fourrierCosL2Win[2] + "," + fourrierCosL2Win[3] + "," + fourrierCosL2Win[4]);
        csvContent.AppendLine("SinL2 " + "," + fourrierSinL2Win[0] + "," + fourrierSinL2Win[1] + "," + fourrierSinL2Win[2] + "," + fourrierSinL2Win[3] + "," + fourrierSinL2Win[4]);

        // Utilisez File.WriteAllText pour écrire le contenu du fichier CSV
        File.WriteAllText(filePath, csvContent.ToString());

        Debug.Log("Fichier CSV créé : " + filePath);
    }
}