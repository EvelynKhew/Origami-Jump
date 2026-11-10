using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicManager : MonoBehaviour
{
    public GameObject mouseCam;
    public GameObject initPage;
    public tutorialManager tutorialManager;

    int nucACount = 0;
    int nucTCount = 0;
    int nucGCount = 0;
    int nucCCount = 0;

    int scatteredA = 0;
    int scatteredT = 0;
    int scatteredG = 0;
    int scatteredC = 0;

    int fixedBridges = 0;

    helixContainerSlot selectedSlot = null;

    bool tutRunA = false;

    int curColour = 0; 
    List<Color32> palette = new List<Color32>
    {
        new Color32(255, 153, 200, 255), 
        
        new Color32(208, 244, 222, 255),
        new Color32(228, 193, 249, 255),
        new Color32(219, 127, 103, 255),
        new Color32(98, 84, 82, 255),
        new Color32(252, 246, 189, 255),
        new Color32(7, 44, 51, 255),
        new Color32(169, 222, 249, 255),
        new Color32(78, 53, 24, 255)
    };

    //start method to handle proper initialisation order
    private void Start()
    {
        mouseCam.GetComponent<mouseLook>().initialiseMouse();
        if(initPage != null) { initPage.GetComponent<tutorialTimer>().initialiseTut(); }
        
    }


    public void addNucleotideA(int amt) { 
        nucACount += amt;
        //for tutorials
        if (nucACount == 1 && !tutRunA)
        {
            tutorialManager.hintForRespPt();
            tutRunA = true;
        }
    
    }

    public void addNucleotideT(int amt) { nucTCount += amt; }

    public void addNucleotideG(int amt) { nucGCount += amt; }
    public void addNucleotideC(int amt) { nucCCount += amt; }

    public void deleteNucleotideA(int amt) { nucACount -= amt; }
    public void deleteNucleotideT(int amt) { nucTCount -= amt; }
    public void deleteNucleotideG(int amt) { nucGCount -= amt; }
    public void deleteNucleotideC(int amt) { nucCCount -= amt; }

    //getter method for nucleotideA
    public int getNucleotideA() { return nucACount; }

    //getter method for nucleotideT
    public int getNucleotideT() { return nucTCount; }

    //getter method for nucleotideG
    public int getNucleotideG() { return nucGCount; }

    //getter method for nucleotideC
    public int getNucleotideC() { return nucCCount; }

    public void deleteABy1() { nucACount -= 1; }
    public void deleteGBy1() { nucGCount -= 1; }
    public void deleteTBy1() { nucTCount -= 1; }
    public void deleteCBy1() { nucCCount -= 1; }

    public void addScatteredA(int amt) { scatteredA += amt; }
    public void addScatteredT(int amt) { scatteredT += amt; }
    public void addScatteredG(int amt) { scatteredG += amt; }
    public void addScatteredC(int amt) { scatteredC += amt; }

    public void resetScatteredNucs()
    {
        scatteredA = 0;
        scatteredT = 0;
        scatteredG = 0;
        scatteredC = 0;
    }

    public int getScatteredA() { return scatteredA; }
    public int getScatteredT() { return scatteredT; }
    public int getScatteredG() { return scatteredG; }
    public int getScatteredC() { return scatteredC; }

    public void clickedHCSlot(helixContainerSlot slot) { selectedSlot = slot; }

    public void addFixedBridgeCount() { fixedBridges++; }
    public int getFixedBridgesCount() { return fixedBridges; }

    //colour methods
    public Color32 currentColour() { return palette[curColour]; }

    public Color32 nextColour()
    {
        curColour = (curColour + 1) % palette.Count;
        return palette[curColour];
    }

}

