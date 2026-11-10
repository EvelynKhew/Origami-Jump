using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

/*
 * Script attached to the staple container game object in the Production tab. 
 * Used for generating staple slots. 
 * stapleContainer acts as a button, which will create a white highlight around it. [add button onclick method]
 */
public class prodSContainer : MonoBehaviour
{
    public GameObject stapleSlot;
    public Image containerBG;

    List<Sprite> nucleotideSprites;
    string stapleString; //string to hold info of <this> staple
    bool startup = false; //only used once to ensure that the nucleotideSprites list has been initiated
    bool clicked = false;
    public Sprite A;
    public Sprite G;
    public Sprite C;
    public Sprite T;

    Vector3 top = new Vector3(0, 0, 90);

    /*
    * Method used to repaint the staples to depict their respective nucs
    * List contains the AGCT data of each nuc
    */

    public void addToBuffer(string list)
    {
        stapleString = list;
    }

    public void rePaint(string list)
    {
        if(!startup)
        {

            nucleotideSprites = new List<Sprite>
            {
                A, G, C, T
            };
            startup = true;
        }

        stapleString = list;

        for(int  i = 0; i < list.Length; i++)
        {
            //generate new staple slot
            GameObject temp = Instantiate(stapleSlot, transform);
            temp.GetComponent<prodStapleSlot>().img.color = Color.white;
            temp.GetComponent<RectTransform>().Rotate(top);

            //assign the nuc of the staple to the slot
            if (list[i] == 'A') { temp.GetComponent<prodStapleSlot>().img.sprite = nucleotideSprites[0]; }
            else if (list[i] == 'G') { temp.GetComponent<prodStapleSlot>().img.sprite = nucleotideSprites[1]; }
            else if (list[i] == 'C') { temp.GetComponent<prodStapleSlot>().img.sprite = nucleotideSprites[2]; }
            else if (list[i] == 'T') { temp.GetComponent<prodStapleSlot>().img.sprite = nucleotideSprites[3]; }
            else { Debug.Log("Wrong nuc type. i = " + i + "input = " + list); }

        }
    }

    //To highlight that this staple strand is the currently selected one
    public void onClick()
    {
        string eventInfo = "[CLICK] BMM Production Tab staple clicked. " + stapleString;
        AllLoggers.getFirstLogger().addLine("[STPL_CL]", eventInfo);

        if (!clicked) //set as clicked
            containerBG.color = new Color32(255, 255, 255, 50);
        else //set as not clicked
            containerBG.color = new Color32(255, 255, 255, 0);

        clicked = !clicked;
    }

    public bool getClicked()
    {
        return clicked;
    }

    public string getStapleString()
    {
        return stapleString;
    }
}
