using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandHandling : MonoBehaviour
{
    public string islandName;
 
    private void OnTriggerEnter(Collider other)
    {
        string eventInfo = islandName + " entered.";
        AllLoggers.getFirstLogger().addLine("ISLD_ENT", eventInfo);
    }

    private void OnTriggerExit(Collider other)
    {
        string eventInfo = islandName + " exited.";
        AllLoggers.getFirstLogger().addLine("ISLD_EXIT", eventInfo);
    }
}
