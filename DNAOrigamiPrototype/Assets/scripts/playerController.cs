using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public mouseLook mouseCam;
    public logicManager logic;
    public tutorialManager tutorialManager;

    public bridgeMenuManager bridgeMenu;
    public settingsMenuManager settingsMenu;
    public worldMapManager worldMap;

    public GameObject minimap;

    public GameObject bridgePiece;
    public GameObject bridgeBuildingUI;
    public GameObject videoHolder;
    public GameObject settingsUI;
    public GameObject worldMapUI;

    public GameObject HUD;

    public GameObject pidObj;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float fallDist = -50f;

    public bool canMove = true;
    bool hotkeyTriggers;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask bridgeMask;
    public LayerMask spawnPointMask;

    Vector3 velocity;
    Vector3 respawnPos = new Vector3(0f, 3f, 0f);
    bool isGrounded;
    bool onBridge;
    bool onSpawnPoint;
    bool tutsClosed = false;

    bool bridgeUIOpen = false;
    bool settingsUIOpen = false;
    bool worldMapUIOpen = false; 

    public int nucA;
    public int nucT;
    public int nucG;
    public int nucC;

    float x = 0;
    float z = 0;

    // Update is called once per frame
    void Update() {

        minimap.transform.transform.position = new Vector3(transform.position.x, minimap.transform.transform.position.y, transform.position.z);

        //to check if player is free falling
        if (velocity.y < fallDist) {
            float xPos = transform.position.x;
            float yPos = transform.position.y;
            float zPos = transform.position.z;
            string eventLogFALL = "Player falling at [" + xPos + " | " + yPos + " | " + zPos + "]";

            AllLoggers.getFirstLogger().addLine("PL_FALL", eventLogFALL);
            repositionPlayer(-2f, respawnPos); 
        }

        //both to check the player's location
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        onBridge = Physics.CheckSphere(groundCheck.position, groundDistance, bridgeMask);
        onSpawnPoint = Physics.CheckSphere(groundCheck.position, groundDistance, spawnPointMask);

        //check if player is on the bridge puzzle piece or not
        if (onBridge) { repositionPlayer(0, respawnPos); }
        if (isGrounded && velocity.y < 0) { velocity.y = -2f; }

        //if player reaches a new spawn point, update respawn pos to be the position that the player is at currently. 
        if (onSpawnPoint) { respawnPos = transform.position; }

        if(Input.GetKeyDown(KeyCode.W) && canMove && hotkeyTriggers) { z = 1; }
        if(Input.GetKeyUp(KeyCode.W)) { z = 0; }

        if(Input.GetKeyDown(KeyCode.A) && canMove && hotkeyTriggers) { x = -1; }
        if (Input.GetKeyUp(KeyCode.A)) { x = 0; }

        if(Input.GetKeyDown(KeyCode.S) && canMove && hotkeyTriggers) { z = -1; }
        if (Input.GetKeyUp(KeyCode.S)) { z = 0; }
        
        if(Input.GetKeyDown(KeyCode.D) && canMove && hotkeyTriggers) { x = 1; }
        if (Input.GetKeyUp(KeyCode.D)) { x = 0; }

        //CHEATS FOR DEMO PURPOSE
        if (Input.GetKeyDown(KeyCode.G) && hotkeyTriggers) {

            Vector3 cheat = new Vector3(68.5f, 3f, 249.5f);
            repositionPlayer(0, cheat);
        }

        Vector3 move = transform.right * x + transform.forward * z;
        if (controller.enabled)
            controller.Move(move*speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && hotkeyTriggers) { velocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity); }

        velocity.y += gravity * Time.deltaTime;

        if(controller.enabled)
            controller.Move(velocity * Time.deltaTime);

        //open close Inventory here
        if (Input.GetKeyDown(KeyCode.E) && settingsUIOpen == false && worldMapUIOpen == false && hotkeyTriggers) {
            if (!bridgeUIOpen) {
                if (tutorialManager.getLabViewed() && !tutsClosed)
                { 
                    tutorialManager.clearTuts(); 
                    tutsClosed = true;
                }

                AllLoggers.getFirstLogger().addLine("INV_OPEN", "Inventory UI opened.");
                mouseCam.turnMovementOff();
                openInventory(); 
                videoHolder.SetActive(true);
                HUD.SetActive(false);
            }
            else {
                AllLoggers.getFirstLogger().addLine("INV_CLOSE", "Inventory UI closed.");
                mouseCam.turnMovementOn();
                bridgeMenu.bridgeClosed();
                closeInventory();
                videoHolder.SetActive(false );
                bridgePiece = bridgeMenu.bridgePieceKey;
                HUD.SetActive(true);
            }
        }

        //open close Settings here
        else if (Input.GetKeyDown(KeyCode.Escape) && bridgeUIOpen == false && worldMapUIOpen == false && hotkeyTriggers)
        {
            controller.enabled = !controller.enabled;
            if (!settingsUIOpen) {
                AllLoggers.getFirstLogger().addLine("SETT_OPEN", "Settings UI opened.");
                mouseCam.turnMovementOff();
                settingsUIOpen = true;
                controller.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                settingsUI.SetActive(true);
            }
            else
            {
                AllLoggers.getFirstLogger().addLine("SETT_CLOSE", "Settings UI closed.");
                mouseCam.turnMovementOn();
                Cursor.lockState = CursorLockMode.Locked;
                settingsUIOpen = false;
                controller.enabled = true;
                settingsUI.SetActive(false);
            }
        }

        //open close Map here
        else if (Input.GetKeyDown(KeyCode.M) && bridgeUIOpen == false && settingsUIOpen == false && hotkeyTriggers)
        {
            if (!worldMapUIOpen)
            {
                AllLoggers.getFirstLogger().addLine("MAP_OPEN", "Map UI opened.");
                mouseCam.turnMovementOff();
                worldMapUIOpen = true;
                controller.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                worldMapUI.SetActive(true);
            }
            else
            {
                AllLoggers.getFirstLogger().addLine("MAP_CLOSE", "Map UI closed.");
                mouseCam.turnMovementOn();
                worldMapUIOpen = false;
                controller.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                worldMapUI.SetActive(false);
            }
        }
    }

    //dealing with respawn points
    public void repositionPlayer(float speed, Vector3 pos) {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        string eventLogPL_REPO = "Player repositioned to [" + xPos + " | " + yPos + " | " + zPos + "]";

        AllLoggers.getFirstLogger().addLine("PL_REPOS", eventLogPL_REPO);

        controller.enabled = false;
        transform.position = pos;
        velocity.y = speed;

        controller.enabled = true;
    }

    void openInventory() { 
        //for debugging
        nucA = logic.getNucleotideA();
        nucT = logic.getNucleotideT();
        nucG = logic.getNucleotideG();
        nucC = logic.getNucleotideC();

        bridgeUIOpen = true;
        controller.enabled = false;
        bridgeBuildingUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        if (bridgeMenu.simpleStrandSpawn != null) { bridgeMenu.simpleStrandSpawn.playVid(); }
        else if (bridgeMenu.strandSpawn != null) { bridgeMenu.strandSpawn.playVid(); }
        else if (bridgeMenu.origamiSpawn != null) { bridgeMenu.origamiSpawn.playVid(); }
    }

    void closeInventory() {
        bridgeUIOpen = false;
        controller.enabled = true;
        bridgeBuildingUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void turnHotKeysOff()
    {
        hotkeyTriggers = false;
    }

    public void turnHotKeysOn()
    {
        hotkeyTriggers = true;
    }

    //awake methods are called before all start methods
    private void Awake()
    {
        AllLoggers.registerPlayer(this);
        
    }

    public bool getOnSpawnPoint()
    {
        return onSpawnPoint;
    }
}
