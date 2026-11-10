using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class invTScript : MonoBehaviour
{
    [SerializeField]
    Image tImage;

    [SerializeField]
    TMP_Text quantity;

    bridgeMenuManager bridgeMenu;
    hcManager helixContManager;
    string invName = "T";

    Color32 lowNucCt = new Color32(255, 248, 80, 255);
    Color32 veryLowNucCt = new Color32(255, 0, 0, 255);

    Sprite nucSprite;

    //bool firstClick = true;
    public tutorialManager tutorialManager;
    public GameObject nucHint;

    private void Start() {
        bridgeMenu = GameObject.FindGameObjectWithTag("bridgeMenu").GetComponent<bridgeMenuManager>();
        helixContManager = GameObject.FindGameObjectWithTag("hcManager").GetComponent<hcManager>();
        nucSprite = helixContManager.nucT;
    }

    public void setData(int amt) { 
        quantity.text = "x" + amt + "";

        if (amt == 0) { quantity.color = veryLowNucCt; }
        else if (amt == 1) { quantity.color = lowNucCt; }
        else { quantity.color = Color.white; }
    }

    public void mouseClick() {
        string tClick = "[CLICK] T nucleotide in BMM inventory HUD Dupe.";
        AllLoggers.getFirstLogger().addLine("INV_T_CL", tClick); //Inventory T clicked

        bridgeMenu.setSelectedNuc(invName);
    }

    
}
