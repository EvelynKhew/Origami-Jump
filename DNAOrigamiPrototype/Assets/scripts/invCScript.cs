using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class invCScript : MonoBehaviour
{
    [SerializeField]
    Image cImage;

    [SerializeField]
    TMP_Text quantity;

    bridgeMenuManager bridgeMenu;
    hcManager helixContManager;
    string invName = "C";
    Color32 lowNucCt = new Color32(255, 248, 80, 255);
    Color32 veryLowNucCt = new Color32(255, 0, 0, 255);

    Sprite nucSprite;

    private void Start() {
        bridgeMenu = GameObject.FindGameObjectWithTag("bridgeMenu").GetComponent<bridgeMenuManager>();
        helixContManager = GameObject.FindGameObjectWithTag("hcManager").GetComponent<hcManager>();
        nucSprite = helixContManager.nucCRev;
    }
    public void setData(int amt) {
        this.quantity.text = "x" + amt + "";

        if (amt == 0) { quantity.color = veryLowNucCt; }
        else if (amt == 1) { quantity.color = lowNucCt; }
        else { quantity.color = Color.white; }
    }

    public void mouseClick() {
        string cClick = "[CLICK] C nucleotide in BMM inventory HUD Dupe.";
        AllLoggers.getFirstLogger().addLine("INV_C_CL", cClick); //Inventory C clicked
        bridgeMenu.setSelectedNuc(invName);
    }
}
