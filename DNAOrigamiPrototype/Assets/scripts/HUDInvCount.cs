using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class HUDInvCount : MonoBehaviour
{
    public char nucType;
    public TMP_Text quantity;

    int count;
    logicManager logic;
    Color32 lowNucCt = new Color32(255, 248, 80, 255);
    Color32 veryLowNucCt = new Color32(255, 0, 0, 255);

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicManager>();
        updateCount();
    }

    void Update() { updateCount(); }

    void updateCount()
    {
        if (nucType == 'A')
        {
            count = logic.getNucleotideA();
            quantity.text = "x" + count.ToString();

        }
        else if (nucType == 'G')
        {
            count = logic.getNucleotideG();
            quantity.text = "x" + count.ToString();
        }
        else if (nucType == 'T')
        {
            count = logic.getNucleotideT();
            quantity.text = "x" + count.ToString();
        }
        else if (nucType == 'C')
        {
            count = logic.getNucleotideC();
            quantity.text = "x" + count.ToString();
        }

        if (count == 0) { quantity.color = veryLowNucCt; }
        else if (count == 1) { quantity.color = lowNucCt; }
        else { quantity.color = Color.white; }
    }
}
