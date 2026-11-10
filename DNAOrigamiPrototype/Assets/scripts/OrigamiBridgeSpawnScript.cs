using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrigamiBridgeSpawnScript : MonoBehaviour
{

    public bridgeMenuManager bridgeMenuScript;
    public GameObject indicator;
    string respPtName;
    
    public GameObject tutorialVideo;
    public GameObject tutPopUp;
    public GameObject brokenHCIndicator;
    public GameObject helpButton;
    public GameObject helpText;
    public GameObject helpStaple;

    public VideoReplayManager videoReplayManager;

    public bool tutWatched = false;
    public bool tutNeeded;
    public bool resetCamX = false;
    public bool brokenHC = false;
    public bool turnOnBrokenIndicator;

    //required amount of hc pairs
    public int neededhcPairs;

    mapManager mapMngr; //map cam
   
    //For relocating map cam
    public float vectX;
    public float vectY;
    public float vectZ;
    public float rotateZ;

    //Materials handling
    public List<Material> materials;

    //scaffold(s)
    public List<GameObject> scaffolds;

    //staple(s)
    public List<GameObject> staples;

    public GameObject extrasScaffolds;
    public GameObject extrasStaples;

    //for setHCImageColours use
    public ArrayNuc[,] puzzleNucs;
    public ArrayNuc[,] solutionNucs;

    //multiple answers separated by comma in each string
    public List<string> staplesContent; //each list entry is one set of possible answers
    public List<List<string>> staplesContentSeparated; //separated out into arrays 

    public List<GameObject> forages;

    public GameObject map;

    int matCount = 0; //for tracking material count as to not have materials be the same colour

    private void Start()
    {
        staplesContentSeparated = new List<List<string>>();
        mapMngr = GameObject.FindGameObjectWithTag("mapCamera").GetComponent<mapManager>();
        respPtName = gameObject.name;

        //disable mesh, change mats 
        for (int i = 0; i < scaffolds.Count; i++)
        {
            for (int j = 0; j < scaffolds[i].transform.childCount; j++)
            {
                //change mat, disable mesh
                scaffolds[i].transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[i%6]; 
                scaffolds[i].transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            matCount++;
        }  
        
        for(int i = 0; i < extrasScaffolds.transform.childCount; i++) { 
            extrasScaffolds.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = false; 
        }

        //separate staples content
        for(int i = 0; i < staplesContent.Count; i++)
        {
            string tempStr = staplesContent[i];
            string[] tempArr = tempStr.Split(',');

            //converting string[] to list<string> because Except only works with lists and not arrays
            List<string> temp = tempArr.ToList();

            staplesContentSeparated.Add(temp);
        } 

    }

    private void OnTriggerEnter(Collider other)
    {
        string eventLogENTER = respPtName + " entered.";
        AllLoggers.getFirstLogger().addLine("RESPPT_ENT", eventLogENTER); //respawn point entered

        Vector3 relocateCamera = new Vector3(vectX, vectY, vectZ);
        if (resetCamX) { mapMngr.Reset(); }
        mapMngr.relocateMap(relocateCamera);
        if(!resetCamX) mapMngr.orientateMap(rotateZ);

        if (bridgeMenuScript.hcPairs < neededhcPairs) { 
            bridgeMenuScript.allHelices.addPairs(neededhcPairs - bridgeMenuScript.hcPairs); 
            bridgeMenuScript.hcPairs = neededhcPairs;
        }

        bridgeMenuScript.setBridgeType(2); //origami type bridge

        bridgeMenuScript.simpleStrandSpawn = null;
        bridgeMenuScript.strandSpawn = null;
        bridgeMenuScript.origamiSpawn = null;

        bridgeMenuScript.origamiSpawn = this;
        if(!brokenHC) bridgeMenuScript.setHCImageColours();
                
        if(turnOnBrokenIndicator) { 
            bridgeMenuScript.brokenIndicator.SetActive(true); 
            map.SetActive(true);
        }
        if (tutNeeded)
        {
            tutPopUp.SetActive(true) ;
            tutNeeded = !tutNeeded ;
        }

        if(helpButton != null) { helpButton.SetActive(true); }
        else { bridgeMenuScript.helpButton.SetActive(false); }
        
    }

    private void OnTriggerExit(Collider other)
    {
        string eventLogEXIT = respPtName + " left.";
        AllLoggers.getFirstLogger().addLine("RESPPT_EXIT", eventLogEXIT); //respawn point exited

        bridgeMenuScript.resetInvImages();
        bridgeMenuScript.origamiSpawn = null;

        if (extrasStaples.activeSelf)
        {
            this.GetComponent<Collider>().isTrigger = false;
            this.GetComponent<Collider>().enabled = false;
            return;
        }
    }

    public void fixBridge()
    {
        string eventLogFIX = "[ORIGAMI] " + respPtName + " fixed.";
        AllLoggers.getFirstLogger().addLine("O_BR_FIXED", eventLogFIX); //origami bridge fixed
        if(helpButton != null) { helpButton.SetActive(false); }
        if(helpStaple != null && helpStaple.activeSelf) { helpStaple.SetActive(false); }

        //enable mesh
        for (int i = 0; i < staples.Count; i++)
        {
            for (int j = 0; j < staples[i].transform.childCount; j++)
            {
                staples[i].gameObject.SetActive(true);
                staples[i].transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = materials[(i + matCount) % 6]; 
                staples[i].transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true; }
        }

        for (int i = 0; i < scaffolds.Count; i++)
        {
            for (int j = 0; j < scaffolds[i].transform.childCount; j++)
            { 
                scaffolds[i].transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true;
            }
            matCount++;
        }
        for (int i = 0; i < extrasScaffolds.transform.childCount; i++) { extrasScaffolds.transform.GetChild(i).gameObject.GetComponent<BoxCollider>().enabled = true; }
       
        extrasStaples.SetActive(true);
        bridgeMenuScript.psl.deleteAllStaples();
        foreach(GameObject a in forages)
        {
            if (a.activeSelf) { a.SetActive(false); }
        }

        if(helpButton != null) { 
            helpButton.SetActive(false);
            closeHint();
        }
        indicator.SetActive(false);
    }

    public void playVid()
    {
        if (tutorialVideo != null && !tutWatched)
        {
            string eventInfo = "Origami tutorial start.";
            AllLoggers.getFirstLogger().addLine("TUT_ST_ORI", eventInfo);
            tutorialVideo.SetActive(true);
            tutorialVideo.GetComponent<videoScreenController>().offMinimapAndBMM();
            tutWatched = !tutWatched;

            if(videoReplayManager != null) { videoReplayManager.setVid3PlayedOn(); }
        }
    }

    public int returnMatCount() { return matCount; }

    public void openHint()
    {
        string eventInfo = "Help button opened at " + respPtName;
        AllLoggers.getFirstLogger().addLine("ORI_HELP_OP", eventInfo);
        helpButton.SetActive(false);
        helpText.SetActive(true);
        if (helpStaple != null) { helpStaple.SetActive(true);}
    }

    public void closeHint()
    {
        string eventInfo = "Help button closed at " + respPtName;
        AllLoggers.getFirstLogger().addLine("ORI_HELP_CL", eventInfo);
        helpButton.SetActive(true);
        helpText.SetActive(false);
        if (helpStaple != null) { helpStaple.SetActive(false); }
    }
}
