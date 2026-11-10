using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bankUIScript : MonoBehaviour
{
    public playerController player;
    public logicManager logic;
    public mouseLook cam;
    public TMP_Text nucAText;
    public TMP_Text nucTText;
    public TMP_Text nucCText;
    public TMP_Text nucGText;

    int nucA;
    int nucT;
    int nucG;
    int nucC;

    //used by retrieve button in [UI] bank inventory

    private void Update()
    {
        nucA = logic.getScatteredA();
        nucT = logic.getScatteredT();
        nucG = logic.getScatteredG();
        nucC = logic.getScatteredC();

        nucAText.text = "x" + nucA.ToString();
        nucTText.text = "x" + nucT.ToString();
        nucGText.text = "x" + nucG.ToString();
        nucCText.text = "x" + nucC.ToString();
    }

    public void retrieveNucs()
    {
        string eventInfo = "Excess nucs retrieved.";
        AllLoggers.getFirstLogger().addLine("RTRVE", eventInfo);

        //add nucs to inventory
        logic.addNucleotideA(nucA);
        logic.addNucleotideT(nucT);
        logic.addNucleotideG(nucG);
        logic.addNucleotideC(nucC);

        //reset UI inv text
        nucAText.text = "x0";
        nucTText.text = "x0";
        nucGText.text = "x0";
        nucCText.text = "x0";

        //then reset all to zero in logicManager and this 
        logic.resetScatteredNucs();
    }

    public void closeBankInv()
    {
        string eventInfo = "Bank inventory closed.";
        AllLoggers.getFirstLogger().addLine("BNK_CLOSE", eventInfo);

        this.gameObject.SetActive(false);
        player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        cam.turnMovementOn();
    }
}
