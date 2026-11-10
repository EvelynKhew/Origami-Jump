using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/*
 * Used by the Double Helix prefab GameObject
 */

public class doubleHelixManager : MonoBehaviour
{
    public Dictionary<string, List<helixContainerSlot>> doubleStrand;
    public GameObject topStrand;
    public GameObject bottomStrand;

    float width = 0;

    List<helixContainerSlot> helixTopSlots;
    List<helixContainerSlot> helixBottomSlots;

    bool dictCreated = false;

    public void setupDoubleStrand()
    {
        helixTopSlots = new List<helixContainerSlot>();
        helixBottomSlots = new List<helixContainerSlot>();

        helixTopSlots = topStrand.GetComponent<singleStrand>().returnList();
        helixBottomSlots = bottomStrand.GetComponent<singleStrand>().returnList();

        doubleStrand.Add("topSlot", helixTopSlots);
        doubleStrand.Add("bottomSlot", helixBottomSlots);

        width = topStrand.GetComponent<singleStrand>().getWidth();
        transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public Dictionary<string, List<helixContainerSlot>> returnDict()
    {
        ensureDictCreated();
        return doubleStrand;
    }

    public void ensureDictCreated()
    {
        if (!dictCreated)
        {
            doubleStrand = new Dictionary<string, List<helixContainerSlot>>();
            setupDoubleStrand();
            dictCreated = true;
        }
    }

    public float getWidth()
    {
        ensureDictCreated();
        return width;
    }

}
