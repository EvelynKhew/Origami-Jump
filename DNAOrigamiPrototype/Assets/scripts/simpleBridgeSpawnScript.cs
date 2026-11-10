using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//script to pass information from the bridge associated with this spawn point to the UI, information gets sent to UI 
public class simpleBridgeSpawnScript : MonoBehaviour
{
    public bool topBottom; //0 if given piece is attached to player island, 1 otherwise
    public string nuc;

    string respPtName;

    public GameObject givenBridge;
    public GameObject missingBridge;
    public MeshCollider islandMesh;

    public GameObject tutorialVideo;
    public bool tutWatched = false;
    public tutorialManager tutorialManager;
    public VideoReplayManager videoReplayManager;
    public GameObject indicator;

    public float vectX;
    public float vectY;
    public float vectZ;
    public float rotateZ;

    public bridgeMenuManager bridgeMenu;
    mapManager mapMngr;
    scoreTracker score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("scoreTracker").GetComponent<scoreTracker>();
        mapMngr = GameObject.FindGameObjectWithTag("mapCamera").GetComponent<mapManager>();
        GameObject meshCont = givenBridge.transform.GetChild(0).gameObject;
        meshCont.GetComponent<MeshCollider>().enabled = false;
        respPtName = gameObject.name;
    }

    /* called when the respawn point is collided with 
     */
    private void OnTriggerEnter(Collider other) {
        string eventLogENTER = respPtName + " entered.";

        AllLoggers.getFirstLogger().addLine("RESPPT_ENT", eventLogENTER); //respawn point entered

        if (missingBridge.activeSelf) {  
            this.GetComponent<Collider>().isTrigger = false;
            this.GetComponent<Collider>().enabled = false;

            string eventLogDISABLE = respPtName + " disabled.";
            AllLoggers.getFirstLogger().addLine("RESPPT_DIS", eventLogDISABLE); //respawn point disabled

            bridgeMenu.resetInvImages();  
            return;
        }

        if(tutorialVideo != null && !tutWatched && tutorialManager != null) { 
            tutorialManager.hintOpenMenu();
            tutorialManager.setLabViewed(true);
        }

        Vector3 relocateCamera = new Vector3(vectX, vectY, vectZ);
        mapMngr.relocateMap(relocateCamera);
        mapMngr.orientateMap(rotateZ);

        bridgeMenu.simpleStrandSpawn = null;
        bridgeMenu.strandSpawn = null;
        bridgeMenu.origamiSpawn = null;

        bridgeMenu.simpleStrandSpawn = this;
        bridgeMenu.setBridgeType(0);
        bridgeMenu.bridgePieceLock = givenBridge;
        bridgeMenu.bridgePieceKey = missingBridge;
        bridgeMenu.setTestOrBridge(topBottom);
        
        bridgeMenu.setRequiredNuc(nuc); 
        bridgeMenu.reqStapleString = nuc;

        bridgeMenu.setHCImageColours();
        score.setRequiredNuc(nuc);

    }

    public void playVid()
    {
        if(tutorialVideo!= null && !tutWatched)
        {
            string eventInfo = "Base pair tutorial start.";
            AllLoggers.getFirstLogger().addLine("TUT_ST_BP", eventInfo);
            tutorialVideo.SetActive(true);
            tutorialVideo.GetComponent<videoScreenController>().offMinimapAndBMM();
            tutWatched = !tutWatched;
            if(videoReplayManager != null) { videoReplayManager.setVid1PlayedOn(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string eventLogEXIT = respPtName + " left.";
        AllLoggers.getFirstLogger().addLine("RESPPT_EXIT", eventLogEXIT); //respawn point exited

        bridgeMenu.resetInvImages();
        bridgeMenu.simpleStrandSpawn = null;
        bridgeMenu.reqStapleString = null;
    }

    public void fixBridge() {
        string eventLogFIX = "[SIMPLE] " + respPtName + " fixed.";
        AllLoggers.getFirstLogger().addLine("S_BR_FIXED", eventLogFIX); //simple bridge fixed

        if(missingBridge.activeSelf) { return; }
        missingBridge.SetActive(true);
        islandMesh.enabled = true;
        givenBridge.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true;
        indicator.SetActive(false);
    }
}
