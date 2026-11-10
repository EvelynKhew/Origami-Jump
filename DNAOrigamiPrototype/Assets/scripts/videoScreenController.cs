using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;

public class videoScreenController : MonoBehaviour
{
    private VideoPlayer player;
    public Button playPause;
    public Button Close;
    public Sprite play;
    public Sprite pause;
    public string tutIdentifier;

    public GameObject mMap;
    public GameObject bmm;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
        Debug.Log("Video Player Found");
        player.Play();
        player.Pause();
    }
    public void closeClicked()
    {
        string eventInfo = "[CLICK] Tutorial video " + tutIdentifier + " closed. ";
        AllLoggers.getFirstLogger().addLine("[TUT_CLOS_CL]", eventInfo);
        onMinimapAndBMM();

        player.Stop();
        playPause.image.sprite = play;

        this.gameObject.SetActive(false);
    }

    public void playPauseVideo()
    {
        if (player.isPlaying == false)
        {
            string eventInfo = "[CLICK] Tutorial video " + tutIdentifier + " played. ";
            AllLoggers.getFirstLogger().addLine("[TUT_PLAY_CL]", eventInfo);

            player.Play();
            Close.gameObject.SetActive(true);
            playPause.image.sprite = pause;
        }
        else
        {
            string eventInfo = "[CLICK] Tutorial video " + tutIdentifier + " paused. ";
            AllLoggers.getFirstLogger().addLine("[TUT_PAUSE_CL]", eventInfo);

            player.Pause();
            playPause.image.sprite = play;
        }
    }

    public void onMinimapAndBMM()
    {
        mMap.SetActive(true);
        bmm.SetActive(true);
    }

    public void offMinimapAndBMM()
    {
        mMap.SetActive(false);
        bmm.SetActive(false);
    }
}
