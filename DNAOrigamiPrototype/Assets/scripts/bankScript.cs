using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bankScript : MonoBehaviour
{
    public playerController player;
    public mouseLook cam;
    public GameObject invUI;

    // Start is called before the first frame update
    void Start()
    {
        invUI.SetActive(false);
    }

    //Called when Player approaches Bank and triggers its collider
    private void OnTriggerEnter(Collider other)
    {
        invUI.SetActive(true);
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        cam.turnMovementOff();
    }

}