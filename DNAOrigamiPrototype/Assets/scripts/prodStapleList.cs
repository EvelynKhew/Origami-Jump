using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class prodStapleList : MonoBehaviour
{
    public bridgeMenuManager bmm;
    public GameObject stapleCont;
    public List<string> staples;
    public List<string> selectedStaples;

    public Image flash;
    Coroutine curFlashRoutine = null;

    List<string> bufferedStaples;
    List<GameObject> allStaples; //a list of all current staples. Updated in newCont, destroyed in annealClicked
    List<GameObject> clickedStaples; //a list of all clicked staples. Only gets updated in annealClicked(), and gets destroyed after 

    bool initiated = false;
    float width; 
    float height = 100; //need to update height everytime a new container is added in order for scrolling to work

    private void ensureInitiated()
    {
        if (initiated) return;

        staples = new List<string>();

        allStaples = new List<GameObject>();
        clickedStaples = new List<GameObject>();
        bufferedStaples = new List<string> ();
        initiated = true;
    }

    public void newCont(string list)
    {
        GameObject newContainer = Instantiate(stapleCont, transform);
        allStaples.Add(newContainer);

        //checking if BMM is active 
        if (bmm.isActiveAndEnabled) { newContainer.GetComponent<prodSContainer>().rePaint(list); }
        else { newContainer.GetComponent<prodSContainer>().addToBuffer(list); }
        
        updateHeight(); 
    }

    public bool checkBuffer() { 
        if(!initiated) ensureInitiated();
        return (bufferedStaples.Count > 0);
    }

    public void addBufferContents() { 
        //checking if there are left behind buffers
        if (bufferedStaples.Count > 0) {
            for (int i = 0; i < bufferedStaples.Count; i++)
            {
                staples.Add(bufferedStaples[i]);
                newCont(bufferedStaples[i]);
                Debug.Log(bufferedStaples[i]);
            }
        }

        bufferedStaples.Clear();

    }

    public void addToList(string list)
    {
        if (!initiated) { ensureInitiated(); };

        if (list == "" || list == null) return;

        staples.Add(list);
        newCont(list);
        Debug.Log(list);
    }

    public void addToBuffer(string list)
    {
        bufferedStaples.Add(list);
        Debug.Log("staple added");
    }

    public void deleteStapleStr(string list)
    {
        //find if string is in this list
        //if exists, delete,
        //      then redraw the entire staple list area. 
        //         redraw = destroy all containers, make all new containers
        //else, send error message

        for (int i = 0; i < staples.Count; i++)
        {
            if (staples[i] == list)
            {
                staples.RemoveAt(i);
                Debug.Log("Staple removed");
                return;
            }
        }

        //if it reaches here, then it means that the string doesn't exist
        Debug.Log("string <" + list + "> doesn't exist in list");
    }

    public void deleteAllStaples()
    {
        staples.Clear();
    }

    public void updateHeight()
    {
        height += 75;
        transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    //mwthod to redraw all un-deleted staples
    public void redraw()
    {
        deleteContainers(); //first delete all existing staple containers
        height = 100; //resetting height for scrollbar
        transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        for (int i = 0; i < staples.Count; i++) { newCont(staples[i]); }
    }

    public void deleteContainers()
    {
        int childCount = transform.childCount;
        foreach (Transform child in transform) { Destroy(child.gameObject); }

    }

    public void annealClicked()
    {
        string eventInfo = "[CLICK] BMM Production Tab Anneal clicked. ";
        AllLoggers.getFirstLogger().addLine("[ANNL_CL]", eventInfo);

        //figure out which containers were clicked. 
        for (int i = 0; i < allStaples.Count; i++)
        {
            //if this container is clicked, add to clicked staples
            if (allStaples[i].GetComponent<prodSContainer>().getClicked()) { 
                clickedStaples.Add(allStaples[i]); 
            }
        }

        if (!clickedStaples.Any()) { return; } //if no staples chosen, do nothing

        List<string> clickedStaplesSTR = new List<string>();

        //delete() each clickedStaple[i] string from staples and add the strings to clickedStaplesSTR
        for (int i = 0; i < clickedStaples.Count; i++)
        {
            string temp = clickedStaples[i].GetComponent<prodSContainer>().getStapleString();
            if(bmm.getBridgeType() < 2) deleteStapleStr(temp);
            clickedStaplesSTR.Add(temp);
        }   


        if (bmm.prodAnnealCheck(clickedStaplesSTR))
        { 
            if(bmm.getBridgeType() == 2)
            {
                for(int i = 0; i < clickedStaplesSTR.Count; i++) { deleteStapleStr(clickedStaplesSTR[i]); }
            }
        }
        else //WRONG ANS
        {
            if (bmm.getBridgeType() == 2) {  curFlashRoutine = StartCoroutine(flashImage(1)); }
        }

        clickedStaplesSTR.Clear();
        allStaples.Clear();
        clickedStaples.Clear();
        redraw();
    } 

    IEnumerator flashImage(int count)
    {
        for(int i = 0; i < count; i++)
        {
            flash.color = new Color32(255, 0, 0, 30);
            yield return new WaitForSeconds(0.1f);
            flash.color = new Color32(255, 0, 0, 0);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
