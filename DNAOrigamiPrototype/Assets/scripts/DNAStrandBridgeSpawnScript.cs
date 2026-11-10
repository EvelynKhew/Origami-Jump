using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAStrandBridgeSpawnScript : MonoBehaviour
{
    public bridgeMenuManager bridgeMenuScript;
    public GameObject bridgeBuildingMenu;
    public GameObject indicator;
    public List<GameObject> givenBridgePieces = new List<GameObject>();
    public List<GameObject> missingBridgePieces = new List<GameObject>(); //holds all the misssing bridge pieces in the scene
    public bool FTorTF; //5' -> 3' == 0, 3' -> 5' == 1 [basically, top or bottom with nt pair] Possibly could reuse topBottom from BMM
    public string staple;
    public string scaffold;

    string respPtName;

    public Material givenMat;
    public Material missingMat;

    public Color32 givenColour;
    public Color32 missingColour;

    public GameObject tutorialVideo;
    public bool tutWatched = false;

    //For relocating map cam
    public float vectX;
    public float vectY;
    public float vectZ;
    public float rotateZ;

    public GameObject stapleSection;
    scoreTracker score;

    mapManager mapMngr; //map cam
    public VideoReplayManager replayManager;

    // Start is called before the first frame update
    void Start()
    {
        respPtName = gameObject.name;
        score = GameObject.FindGameObjectWithTag("scoreTracker").GetComponent<scoreTracker>();
        mapMngr = GameObject.FindGameObjectWithTag("mapCamera").GetComponent<mapManager>();

        for( int i = 0; i < givenBridgePieces.Count; i++)
        {
            givenBridgePieces[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = givenMat;
            missingBridgePieces[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = missingMat;

            givenBridgePieces[i].transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string eventLogENTER = respPtName + " entered.";
        AllLoggers.getFirstLogger().addLine("RESPPT_ENT", eventLogENTER); //respawn point entered

        Vector3 relocateCamera = new Vector3(vectX, vectY, vectZ);
        mapMngr.relocateMap(relocateCamera);
        mapMngr.orientateMap(rotateZ);
        bridgeMenuScript.simpleStrandSpawn = null;
        bridgeMenuScript.strandSpawn = null;
        bridgeMenuScript.origamiSpawn = null;
        bridgeMenuScript.setBridgeType(1);
        stapleSection.SetActive(true); 
        bridgeMenuScript.reqStapleString = staple;
        bridgeMenuScript.scaffoldStrand = scaffold;
        bridgeMenuScript.strandSpawn = this;
        bridgeMenuScript.setTestOrBridge(FTorTF);
        bridgeMenuScript.setHCImageColours();

    }

    private void OnTriggerExit(Collider other)
    {
        string eventLogEXIT = respPtName + " left.";
        AllLoggers.getFirstLogger().addLine("RESPPT_EXIT", eventLogEXIT); //respawn point exited

        bridgeMenuScript.resetInvImages();
        bridgeMenuScript.strandSpawn = null;
        bridgeMenuScript.reqStapleString = null;

        if (missingBridgePieces[0].activeSelf)
        { 
            this.GetComponent<Collider>().isTrigger = false;
            this.GetComponent<Collider>().enabled = false;
            return;
        }
    }

    //this method gets called by BMM and will be responsible for fixing bridges
    public void fixBridge()
    {
        string eventLogFIX = "[DOUBLE STRAND] " + respPtName + " fixed.";
        AllLoggers.getFirstLogger().addLine("DS_BR_FIXED", eventLogFIX); //double strand bridge fixed

        for (int i = 0; i < missingBridgePieces.Count; i++) { missingBridgePieces[i].SetActive(true); }
        
        for (int i = 0; i < givenBridgePieces.Count; i++)
        { givenBridgePieces[i].transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true; }
        indicator.SetActive(false);
    }

    public void playVid()
    {
        if (tutorialVideo != null && !tutWatched)
        {
            string eventInfo = "DNA Strand tutorial start.";
            AllLoggers.getFirstLogger().addLine("TUT_ST_STR", eventInfo);
            tutorialVideo.SetActive(true);
            tutorialVideo.GetComponent<videoScreenController>().offMinimapAndBMM();
            tutWatched = !tutWatched;

            if(replayManager != null) { replayManager.setVid2PlayedOn(); }
        }
    }
}
