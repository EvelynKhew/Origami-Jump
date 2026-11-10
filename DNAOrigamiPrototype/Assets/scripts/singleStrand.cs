using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used by the 5'-3' and 3' to 5' prefab GameObject
 */

public class singleStrand : MonoBehaviour
{
    public GameObject helixSlot;
    public bool topBottom; //0 = top, 1 = bottom

    //width and height of this strand
    float width = 0;
    public float height = 50;

    List<helixContainerSlot> helixSlots;
    bool listCreated = false;


    //if i = 0, add 10 slots 
    public void addHelixSlots(int amt)
    {
       if (amt == 0) amt = 10;

       Vector3 top = new Vector3(0, 0, -90);
       Vector3 bottom = new Vector3(0, 0, 90);

       for(int i = 0; i < amt; i++)
        {
            GameObject temp = Instantiate(helixSlot, transform);
            if(topBottom)
            {
                temp.GetComponent<RectTransform>().Rotate(top);
            }
            else {
                temp.GetComponent<RectTransform>().Rotate(bottom);
            }
            helixSlots.Add(temp.GetComponent<helixContainerSlot>());

        }

        width = width + 50 * amt;
        transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }


    public List<helixContainerSlot> returnList() {
        ensureListCreated();
        return helixSlots;
    }

    public void ensureListCreated()
    {
        if (!listCreated)
        {
            helixSlots = new List<helixContainerSlot>();    
            addHelixSlots(20);
            listCreated = true;
        }
    }

    public float getWidth()
    {
        ensureListCreated();
        return width;
    }
}
