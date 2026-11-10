using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class stapleSlot : MonoBehaviour
{
    public Image image;
    public int index;
    public stapleSlot prevSlot;
    public GameObject bridgeMenuUI;

    public bool clicked = false;

    string currentSelected;
    logicManager logic;
    bridgeMenuManager bridgeMenu;
    hcManager helixContManager;

    List<Sprite> nucleotideSprites;

    // Start is called before the first frame update
    void Start()
    {
        bridgeMenu = GameObject.FindGameObjectWithTag("bridgeMenu").GetComponent<bridgeMenuManager>();
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicManager>();
        helixContManager = GameObject.FindGameObjectWithTag("hcManager").GetComponent<hcManager>();

        if (index == 0) {prevSlot = null;}
        nucleotideSprites = new List<Sprite>
        {
            helixContManager.nucAv2,
            helixContManager.nucCRev,
            helixContManager.nucGv2,
            helixContManager.nucT
        };
    }

    // Update is called once per frame
    void Update()
    {
        currentSelected = bridgeMenu.getSelectedNuc();
    }

    //called when a slot in the staple area is clicked
    public void slotClicked()
    {
        string eventInfo = "[CLICK] " + "staple slot [" + transform.gameObject.name + "] clicked.";
        AllLoggers.getFirstLogger().addLine("STPL_CL", eventInfo);

        if (bridgeMenu.getDeleteClicked())
        {
            //if delete is clicked, then just delete stuff from each slot that it clicked, redraw  
            char toDel = bridgeMenu.userGivenStapleString[index];
            Debug.Log(bridgeMenu.userGivenStapleString);
            bridgeMenu.userGivenStapleString = bridgeMenu.userGivenStapleString.Remove(index, 1);
            Debug.Log(bridgeMenu.userGivenStapleString);

            //reimbursing the nucs that was deleted
            if (toDel == 'A') { logic.addNucleotideA(1); }
            else if (toDel == 'G') { logic.addNucleotideG(1); }
            else if (toDel == 'T') { logic.addNucleotideT(1); }
            else if (toDel == 'C') { logic.addNucleotideC(1); }

            bridgeMenu.SetCurrentStaple(index); 

            bridgeMenu.cursors[index].SetActive(true);
            for (int i = 0; i < bridgeMenu.cursors.Count; i++) {
                if(i != index) { bridgeMenu.cursors[i].SetActive(false); }
                 
            }

            string deletionInfo = toDel + " deleted from staple [" + transform.gameObject.name + "].";
            AllLoggers.getFirstLogger().addLine("STPL_DEL", deletionInfo);
            bridgeMenu.redrawStapleSlots();

        }
        //check if prevSlot is filled or not. If not filled, this one cannot be filled as staple strands need to be formed from 5' to 3' in one go
        if(index != 0 && !prevSlot.clicked){
            //do nothing for now. 
        }

        else //either we are in the first slot (index == 0) or we are in the xth slot where its predecessors are filled with some colour. 
        {

            if(currentSelected == null || currentSelected == "") { return; } //if nothing is selected right now

            //handle checking of inventory here 
            // if out of the currently selected one, don't do anything.     
            if ((currentSelected == "A" && logic.getNucleotideA() == 0) ||
                (currentSelected == "T" && logic.getNucleotideT() == 0) ||
                (currentSelected == "G" && logic.getNucleotideG() == 0 ) ||
                (currentSelected == "C" && logic.getNucleotideC() == 0)
                ) { currentSelected = null; }

            else
            {
                bridgeMenu.userGivenStapleString = "" + bridgeMenu.userGivenStapleString + "" + currentSelected; //adding current selected to the end 
                Debug.Log("added " + bridgeMenu.userGivenStapleString);

                if (clicked) //previously, this slot already had a different assignment 
                {
                    char toChange = bridgeMenu.userGivenStapleString[index];
                    char[] tempStapleString = bridgeMenu.userGivenStapleString.ToCharArray();

                    //reimbursing the nucs that is being changed from
                    if (toChange == 'A') { logic.addNucleotideA(1); }
                    else if (toChange == 'G') { logic.addNucleotideG(1); }
                    else if (toChange == 'T') { logic.addNucleotideT(1); }
                    else if (toChange == 'C') { logic.addNucleotideC(1); }

                    tempStapleString[index] = currentSelected[0];
                    bridgeMenu.userGivenStapleString = new string(tempStapleString);
                    bridgeMenu.userGivenStapleString = bridgeMenu.userGivenStapleString.Remove(bridgeMenu.userGivenStapleString.Length - 1); //removing the last character from staple string
                }

                //the currently selected slot is white, and therefore is a blank slot 
                //image.color = Color.white;
                image.color = new Color32(55, 95, 142, 150);
                 
                if (currentSelected == "A")
                {
                    logic.deleteABy1();
                    image.sprite = nucleotideSprites[0];
                }
                else if (currentSelected == "G")
                {
                    logic.deleteGBy1();
                    image.sprite = nucleotideSprites[2];
                }
                else if (currentSelected == "T")
                {
                    logic.deleteTBy1();
                    image.sprite = nucleotideSprites[3];
                }
                else if (currentSelected == "C")
                {
                    logic.deleteCBy1();
                    image.sprite = nucleotideSprites[1];
                }

                currentSelected = null;
            }
        }

        Debug.Log(bridgeMenu.userGivenStapleString);
        clicked = true;
    }
}
