using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoReplayManager : MonoBehaviour
{

    public GameObject BMM;
    public GameObject vid1;
    public GameObject vid2;
    public GameObject vid3;

    public GameObject vid1Button;
    public GameObject vid2Button;
    public GameObject vid3Button;


    int vid1count;
    int vid2count;
    int vid3count;

    // Start is called before the first frame update
    public void replayVid1() {
        vid1count++;
        string eventLog = "Vid 1 replayed. Replay amount: " + vid1count;
        AllLoggers.getFirstLogger().addLine("REPLAY_VID1", eventLog);
        vid1.SetActive(true); 
        vid1Button.SetActive(false);
    }

    public void replayVid2() {
        vid2count++;
        string eventLog = "Vid 2 replayed. Replay amount: " + vid2count;
        AllLoggers.getFirstLogger().addLine("REPLAY_VID2", eventLog);
        vid2.SetActive(true); 
        vid2Button.SetActive(false);
    }

    public void replayVid3() {
        vid3count++;
        string eventLog = "Vid 3 replayed. Replay amount: " + vid3count;
        AllLoggers.getFirstLogger().addLine("REPLAY_VID3", eventLog);
        vid3.SetActive(true); 
        vid3Button.SetActive(false);    
    }  

    public void setVid1PlayedOn() {vid1Button.SetActive(true);}

    public void setVid2PlayedOn() {vid2Button.SetActive(true);}

    public void setVid3PlayedOn() {vid3Button.SetActive(true);}

}
