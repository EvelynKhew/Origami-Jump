using System.Collections.Generic;
using UnityEngine;

/*
 * Used by the AllHelices gameObject
 */

public class allHelicesManager : MonoBehaviour
{
    public GameObject allHelices; //this game object
    public GameObject dbHelix;
    
    List<Dictionary<string, List<helixContainerSlot>>> doubleHelices;
    int helixCounter; //how many double helices already generated 
    float width = 0;
    float height = 0;
    bool listCreated = false;

   public void addPairs(int amt)
    {

        //instantiate singleStrand, add to doubleHelices
        for(int i  = 0; i < amt; i++)
        {
            GameObject temp = Instantiate(dbHelix, transform);
            Dictionary<string, List<helixContainerSlot>> tempDict = temp.GetComponent<doubleHelixManager>().returnDict();
            doubleHelices.Add(tempDict);
            width = temp.GetComponent<doubleHelixManager>().getWidth();
        }

        width += 100;
        helixCounter += amt;

        updateWidth();
        updateHeight();
    }

    //public getter method for the list
    public List<Dictionary<string, List<helixContainerSlot>>> getDoubleHelices()
    {
        ensureListCreated();
        return doubleHelices;
    }

    public int getDoubleHelixCount()
    {
        return helixCounter;
    }

    public void ensureListCreated()
    {
        if(!listCreated)
        {
            helixCounter = 0; //first time init 
            doubleHelices = new List<Dictionary<string, List<helixContainerSlot>>>();
            addPairs(1);
            listCreated = true;
        }
    }

    public void updateWidth() 
    {
        allHelices.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void updateHeight()
    {
        height = 200 * helixCounter;
        allHelices.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
