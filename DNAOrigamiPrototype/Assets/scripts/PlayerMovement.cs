using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rbPlayer;
    
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }
    
    void Update()
    { 
        //WASD
        if (Input.GetKeyDown(KeyCode.W)) {
            rbPlayer.velocity = new Vector3(0, 0, 5);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rbPlayer.velocity = new Vector3(-5, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rbPlayer.velocity = new Vector3(0, 0, -5);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rbPlayer.velocity = new Vector3(5, 0, 0);
        }

    }
}
