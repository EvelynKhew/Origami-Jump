using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialTimer : MonoBehaviour
{
    public CharacterController characterController;
    public playerController playerController;

    public bool turnOffMouse; //0 = don't turn mouseLook off, 1 otherwise
    public mouseLook mouseCam;
    public GameObject button;

    public GameObject linkedPrevPage;
    public GameObject linkedNextPage;

    public List<GameObject> linkedPages;

    public tutorialManager tutManager;
    public bool isDesignTut;

    public bool opened = false;
    public bool invTuts = false;
    string pid;

    DataLogging logger = AllLoggers.getFirstLogger();

    public void initialiseTut()
    {
        if (turnOffMouse)
        { 
            playerController.turnHotKeysOff();
            mouseCam.turnMovementOff();
            Cursor.lockState = CursorLockMode.None;
            turnOffMouse = false;
            characterController.enabled = false;
        }

        if (this.gameObject.activeSelf) { opened = true; }
    }


     public void hideCanvas()
    {
        string eventInfo = "[CLICK] Starting tutorials close tutorials button on " + transform.gameObject.name + " clicked. ";
        AllLoggers.getFirstLogger().addLine("[ST_CLOS_CL]", eventInfo);
       
        if (!invTuts) { 
            mouseCam.turnMovementOn();
            characterController.enabled = true;
        }

        playerController.turnHotKeysOn();

        this.gameObject.SetActive(false);
        
        if (linkedPrevPage != null) { linkedPrevPage.SetActive(false); }
        if (linkedNextPage != null) { linkedNextPage.SetActive(false); }

        if (tutManager != null && !isDesignTut) tutManager.hintForPickingUpNucs();
        else if (tutManager != null && isDesignTut) tutManager.hintAtNucleotide();

    }

    public void openPrevLinked()
    {
        string eventInfo = "[CLICK] Starting tutorials previous page button on " + transform.gameObject.name + " clicked. ";
        AllLoggers.getFirstLogger().addLine("[ST_PREV_CL]", eventInfo);

        linkedPrevPage.GetComponent<tutorialTimer>().opened = true;
        
        int openedPages = 0;
        for(int i = 0; i < linkedPages.Count; i++)
        {
            if (linkedPages[i] != null && linkedPages[i].GetComponent<tutorialTimer>().opened) { openedPages++; } 
        }

        if(openedPages == linkedPages.Count) {
            for(int i = 0; i < linkedPages.Count; ++i)
            {
                linkedPages[i].GetComponent<tutorialTimer>().button.SetActive(true);
            }
        }

        if (linkedPrevPage != null)
        {
            linkedPrevPage.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void openNextLinked()
    {
        string eventInfo = "[CLICK] Starting tutorials next page button on " + transform.gameObject.name + " clicked. ";
        AllLoggers.getFirstLogger().addLine("[ST_NEXT_CL]", eventInfo);

        linkedNextPage.GetComponent<tutorialTimer>().opened = true;

        int openedPages = 0;
        Debug.Log("num linked pgs: " + linkedPages.Count);
        Debug.Log("cur op count: (pre loop) "+openedPages);

        for (int i = 0; i < linkedPages.Count; i++)
        {
            Debug.Log(i);
            if (linkedPages[i] != null && linkedPages[i].GetComponent<tutorialTimer>().opened) { openedPages++; }
        }

        Debug.Log("cur op count: (post loop) " + openedPages);

        if (openedPages == linkedPages.Count)
        {
            for(int i = 0; i < linkedPages.Count; i++)
            {
                linkedPages[i].GetComponent<tutorialTimer>().button.SetActive(true);
            }
        }

        linkedNextPage.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void pidEntry(string s)
    {
        AllLoggers.getFirstLogger().updatePID(s);
        if (pid == null)
        {
            //data logger
            //datalogging.addline( event type = pid change. event information = first update of pid )
            logger.addLine("PID_CHANGE", "first update of PID.");
            pid = s;
        }
        else
        {
            pid = s;
            logger.addLine("PID_CHANGE", "PID updated.");
        }
        
    }
}
