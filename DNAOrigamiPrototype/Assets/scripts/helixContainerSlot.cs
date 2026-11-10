using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helixContainerSlot : MonoBehaviour
{
    public Image image;
    public GameObject bridgeMenuUI;
    public Image childImg;
    public GameObject strBackbone;
    public GameObject strLeft;
    public GameObject strRight;

    Image backbone;
    Image left;
    Image right;

    logicManager logic;
    bridgeMenuManager bridgeMenu;

    Color32 AColour = new Color32(255, 229, 0, 255);
    Color32 GColour = new Color32(74, 124, 135, 255);
    Color32 TColour = new Color32(116, 101, 180, 255);
    Color32 CColour = new Color32(245, 112, 112, 255);

    // Start is called before the first frame update
    void Start()
    {
        bridgeMenu = GameObject.FindGameObjectWithTag("bridgeMenu").GetComponent<bridgeMenuManager>();
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicManager>();

        backbone = strBackbone.GetComponent<Image>();
        left = strLeft.GetComponent<Image>();
        right = strRight.GetComponent<Image>();

        showStrand(false);
    }

    public void clickedSlot() {
        logic.clickedHCSlot(this);
    }

    public void showStrand(bool show) {
        if (show)
        {
            strBackbone.SetActive(true);
            strLeft.SetActive(true);
            strRight.SetActive(true);
        }

        else { 
            strBackbone.SetActive(false);
            strLeft.SetActive(false);
            strRight.SetActive(false);
        }
    }

    //needed for crossovers
    public void leftOrRight(bool LR)
    {
        //LR true = left, LR false = right
        if(LR) {
            strLeft.SetActive(true);
            strRight.SetActive(false);
        }
        else
        {
            strLeft.SetActive(false);
            strRight.SetActive(true);
        }
    }
    public void strandColour(Color32 colour)
    {
        backbone.color = colour;
        left.color = colour;
        right.color = colour;
    }
}
