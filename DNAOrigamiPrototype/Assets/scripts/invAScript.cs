using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class invAScript : MonoBehaviour
{
    [SerializeField]
    Image aImage;

    [SerializeField]
    TMP_Text quantity;

    bridgeMenuManager bridgeMenu;
    hcManager helixContManager;
    string invName = "A";

    Color32 lowNucCt = new Color32(255, 248, 80, 255);
    Color32 veryLowNucCt = new Color32(255, 0, 0, 255);

    Sprite nucSprite;

    bool firstClick = true;
    public tutorialManager tutorialManager;
    public GameObject nucHint;

    private void Start() {
        bridgeMenu = GameObject.FindGameObjectWithTag("bridgeMenu").GetComponent<bridgeMenuManager>();
        helixContManager = GameObject.FindGameObjectWithTag("hcManager").GetComponent<hcManager>();
        nucSprite = helixContManager.nucAv2;
    }

    public void setData(int amt) {
        this.quantity.text = "x" + amt + "";

        if (amt == 0) { quantity.color = veryLowNucCt; }
        else if (amt == 1) { quantity.color = lowNucCt; }
        else { quantity.color = Color.white; }
    }

    public void mouseClick() {
        string aClick = "[CLICK] A nucleotide in BMM inventory HUD Dupe.";
        AllLoggers.getFirstLogger().addLine("INV_A_CL", aClick); //Inventory A clicked
        bridgeMenu.setSelectedNuc(invName);

        if (firstClick && nucHint.activeSelf)
        {
            tutorialManager.hintAtSynthesize();
            firstClick = false;
        }
    }

}
