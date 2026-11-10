using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bridgeMenuManager : MonoBehaviour
{
    public GameObject self;
    public playerController player;
    public logicManager logic;
    public scoreTracker score;
    public invAScript invA;
    public invGScript invG;
    public invTScript invT;
    public invCScript invC;
    public hcManager helixContManager;

    public Image topImage;
    public Image bottomImage;

    public GameObject synthesizePrompt;

    //timer + score HUD
    public GameObject scoreTextDisplay;
    public GameObject scoreUpdateSignalImage;

    //to access the list-dict-list system
    public GameObject allHCs;
    public allHelicesManager allHelices;
    
    List<Dictionary<string, List<helixContainerSlot>>> allHCsList;

    bool bankPromptShown = false;
    public GameObject bankTutorial;

    //nucleotide base pair bridges usage
    public GameObject bridgePieceKey; //piece that will get activated once player puts in the correct piece
    public GameObject bridgePieceLock; //piece that is provided for the player as a hint
    public simpleBridgeSpawnScript simpleStrandSpawn;

    //strand pair bridges usage
    public string reqStapleString;
    public string userGivenStapleString; //for use when staple strands are involved
    public string scaffoldStrand;
    public List<stapleSlot> stapleSlots;
    public List<GameObject> cursors;
    public List<helixContainerSlot> helixSlots;
    public GameObject stapleSection;
    public DNAStrandBridgeSpawnScript strandSpawn;

    //origami bridges usage
    //list of lists -> list of lists containing possible answer sets
    public List<List<string>> staplesAnsBank;
    public OrigamiBridgeSpawnScript origamiSpawn;
    public GameObject brokenIndicator;
    public GameObject helpButton;

    //production tab usage
    public prodStapleList psl;
    List<string> staplesList;

    //production tab "notification"
    public GameObject prodPage;
    public Image notifCircle;
    public TMP_Text notifAmount;
    bool displayed = false;

    int bridgeType = -1; //0 = simple nt pair, 1 = double helix pair
    int currentStaple;

    public int hcPairs;

    string selectedNuc;
    string requiredNuc; //passed here through the respawn points 

    bool testOrBridge = true; //0 if given piece is attached to player island (3-5), 1 otherwise (5-3)
    Image reqImage;

    List<Sprite> nucleotideSprites;

    List<Color32> hcMatColours;

    //for Delete button
    bool deleteClicked;
    public GameObject deleteButton;
    public Sprite deleteSelected;
    public Sprite deleteUnselected;

    //for data logging
    int wrongAnswers = 0;
    int correctAnswers = 0;

    private void Start()
    {
        allHelices = allHCs.GetComponent<allHelicesManager>();
        allHCsList = allHelices.getDoubleHelices();
        hcPairs = 1;

        helixContManager = GameObject.FindGameObjectWithTag("hcManager").GetComponent<hcManager>();

        nucleotideSprites = new List<Sprite>
        {
            helixContManager.nucAv2,//0
            helixContManager.nucCRev, //1
            helixContManager.nucGv2,//2
            helixContManager.nucT, //3
            helixContManager.nucABase, //4
            helixContManager.nucCBase, //5 
            helixContManager.nucGBase, //6
            helixContManager.nucTBase, //7
        };

        hcMatColours = new List<Color32>
        {
            new Color32(0, 255, 243, 255),
            new Color32(0, 3, 255, 255),
            new Color32(181, 255, 0, 255),
            new Color32(198, 0, 255, 255),
            new Color32(255, 0, 7, 255),
            new Color32(255, 195, 0, 255)
        };

        //for rendering images of nucleotides in inventory/Bridge Menu Manager
        topImage = allHCsList[0]["topSlot"][0].childImg;
        bottomImage = allHCsList[0]["bottomSlot"][0].childImg;
        currentStaple = 0;

        userGivenStapleString = null;

        //setting initial active status of cursors for edit feature
        cursors[0].SetActive(true); 
        for(int i = 1; i < cursors.Count; i++) { cursors[i].SetActive(false); }

        //auto setting stapleSlots' info
        for(int i  = 0; i < stapleSlots.Count; i++) {
            stapleSlots[i].index = i; 
            if(i > 0) { stapleSlots[i].prevSlot = stapleSlots[i - 1]; }
        }

        self.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        setNucA();
        setNucG();
        setNucT();
        setNucC();
        setScoreDisplay();
        if (bridgeType == 0) { ntPairBridgeHandling(); } 

        if (psl.checkBuffer()) { psl.addBufferContents(); }

        if(psl.staples.Count == 0 || prodPage.activeInHierarchy) {
            notifCircle.transform.gameObject.SetActive(false);
            notifAmount.transform.gameObject.SetActive(false);
            displayed = false;
        }
        else if(!prodPage.activeInHierarchy && !displayed)
        {
            notifCircle.gameObject.SetActive(true);
            notifAmount.gameObject.SetActive(true);
            notifAmount.text = psl.staples.Count.ToString();
            displayed = true;            
        } 
        else{ notifAmount.text = psl.staples.Count.ToString();}
    }

    void setNucA() { invA.setData(logic.getNucleotideA()); }

    void setNucG() { invG.setData(logic.getNucleotideG()); }

    void setNucT() { invT.setData(logic.getNucleotideT()); }

    void setNucC() { invC.setData(logic.getNucleotideC()); }

    void setScoreDisplay() { scoreTextDisplay.GetComponent<TextMeshProUGUI>().text = logic.getFixedBridgesCount().ToString() + " / 25"; }

    void testTubeSolver()
    {

        // Set the sprite of the first available slot in the test tube to be the clicked nuc's sprite
        Sprite nucSprite;

        if(currentStaple == stapleSlots.Count) {
            Debug.Log("not enough staple slots left. [I need to implement it so that it can go up to 60 later and im not looking forward to it T.T]."); //it will be fine though :)
            return; 
        }

        if (selectedNuc == "A" && logic.getNucleotideA() != 0) { 
            nucSprite = nucleotideSprites[0];
            logic.deleteABy1();
        }
        else if (selectedNuc == "C" && logic.getNucleotideC() != 0) {
            nucSprite = nucleotideSprites[1]; 
            logic.deleteCBy1();
        }
        else if (selectedNuc == "G" && logic.getNucleotideG() != 0) {
            nucSprite = nucleotideSprites[2];
            logic.deleteGBy1();
        }
        else if (selectedNuc == "T" && logic.getNucleotideT() != 0) { 
            nucSprite = nucleotideSprites[3];
            logic.deleteTBy1();
        }
        else {
            Debug.Log("Selected Nuc not a valid type. OR. selected nuc not enough. Do nothing.");
            return; 
        }

        string temp = "" + selectedNuc;
        if (userGivenStapleString != null) {
            Debug.Log(currentStaple);
            if(currentStaple == userGivenStapleString.Length) { userGivenStapleString = userGivenStapleString + temp; }
            else { userGivenStapleString = userGivenStapleString.Insert(currentStaple, temp);
            }
        }
        else { userGivenStapleString = temp; }

        
        selectedNuc = null; //nuc was used
        currentStaple++; //incrementing for slots
        
        redrawStapleSlots();
        refreshCursorPosition();
    }

    void ntPairBridgeHandling() {

        //0 if given piece is attached to player island, 1 otherwise
        if (testOrBridge) { reqImage = allHCsList[0]["bottomSlot"][0].childImg; }
        else { reqImage = allHCsList[0]["topSlot"][0].childImg; }        
            
        if ((requiredNuc == "A" && selectedNuc == requiredNuc && logic.getNucleotideA() > 0) ||
                    (requiredNuc == "G" && selectedNuc == requiredNuc && logic.getNucleotideG() > 0) ||
                    (requiredNuc == "T" && selectedNuc == requiredNuc && logic.getNucleotideT() > 0) ||
                    (requiredNuc == "C" && selectedNuc == requiredNuc && logic.getNucleotideC() > 0))
        {
            reqImage.color = new Color32(255, 255, 255, 255);  
            if (requiredNuc == "A")
            {
                reqImage.sprite = nucleotideSprites[0];
                logic.deleteABy1();
            }
            else if (requiredNuc == "C")
            {
                reqImage.sprite = nucleotideSprites[1];
                logic.deleteCBy1();
            }
            else if (requiredNuc == "G")
            {
                reqImage.sprite = nucleotideSprites[2];
                logic.deleteGBy1();
            }
            else if (requiredNuc == "T")
            {
                reqImage.sprite = nucleotideSprites[3];
                logic.deleteTBy1();
            }
            else { Debug.Log("unknown requiredNuc string entered. YIKES!!"); }

            score.bridgeFormed();
            requiredNuc = null;
            bridgePieceKey.SetActive(true);
        }

        selectedNuc = null;
    }

    void dhBridgeHandling() {

    }

    public void setSelectedNuc(string clickedNuc) {
        selectedNuc = clickedNuc;
       testTubeSolver();
    }

    //getter method for selected nuc
    //to be used by stapleSlot
    public string getSelectedNuc() { return selectedNuc; }

    public void setTestOrBridge(bool flipflop)
    {
        //0 if given piece is attached to player island, 1 otherwise
        testOrBridge = flipflop; 

        //if 0, bridgeImgClicked needs to be true to set bridge
        //else, testImgClicked needs to be true
    }

    //different bridges have different required nucs  
    public void setRequiredNuc(string reqNuc) { requiredNuc = reqNuc; }

    //is called when player closes the inventory screen. 
    //Resets all helix container images to the default colour. 
    public void resetInvImages() 
    {
        if(bridgeType == 0)
        {
            topImage.color = new Color32(255, 255, 255, 0); //revert back to invisible
            topImage.sprite = null;

            bottomImage.color = new Color32(255, 255, 255, 0);
            bottomImage.sprite = null;
        }
        else if(bridgeType == 1)
        {
            int temp = scaffoldStrand.Length;
            for (int i = 0; i < temp; i++)
            {
                allHCsList[0]["topSlot"][i].childImg.color = new Color32(255, 255, 255, 0);
                allHCsList[0]["topSlot"][i].showStrand(false);
                allHCsList[0]["topSlot"][i].childImg.sprite = null;

                allHCsList[0]["bottomSlot"][i].childImg.color = new Color32(255, 255, 255, 0);
                allHCsList[0]["bottomSlot"][i].showStrand(false);
                allHCsList[0]["bottomSlot"][i].childImg.sprite = null;
            }
        }

        else if (bridgeType == 2)
        {
            for(int i = 0; i < allHCsList.Count; i++)
            {
                List<helixContainerSlot> topSlots = allHCsList[i]["topSlot"];
                List<helixContainerSlot> bottomSlots = allHCsList[i]["bottomSlot"];

                for(int j = 0; j < topSlots.Count; j++)
                {
                    topSlots[j].childImg.color = new Color32(255, 255, 255, 0);
                    bottomSlots[j].childImg.color = new Color32(255, 255, 255, 0);

                    topSlots[j].childImg.sprite = null;
                    bottomSlots[j].childImg.sprite = null;
                }
            }
        }
    }

    //used by respawn points to set the helix container's sprites
    public void setHCImageColours()
    {
        if (bridgeType == 0)
        {
            if (testOrBridge) //the given piece is on the opposite side
            {
                reqImage = bottomImage;
                topImage.color = new Color32(255, 255, 255, 255);
                bottomImage.color = new Color32(255, 255, 255, 90);
                
                if (requiredNuc == "A") { topImage.sprite = nucleotideSprites[3]; }
                else if (requiredNuc == "G") { topImage.sprite = nucleotideSprites[1]; }
                else if (requiredNuc == "C") { topImage.sprite = nucleotideSprites[2]; }
                else if (requiredNuc == "T") { topImage.sprite = nucleotideSprites[0]; }
                else { Debug.Log("unknown reqNuc, bridgeMenuManager line 248"); }
            }
            else
            {
                bottomImage.color = new Color32(255, 255, 255, 255);
                topImage.color = new Color32(255, 255, 255, 90);

                reqImage = topImage;
                if (requiredNuc == "A") { bottomImage.sprite = nucleotideSprites[3]; }
                else if (requiredNuc == "G") { bottomImage.sprite = nucleotideSprites[1]; }
                else if (requiredNuc == "C") { bottomImage.sprite = nucleotideSprites[2]; }
                else if (requiredNuc == "T") { bottomImage.sprite = nucleotideSprites[0]; }
                else { Debug.Log("unknown reqNuc, bridgeMenuManager line 258"); }
            }
        }

        else if(bridgeType == 1) //double helix
        {
            int A = 0;
            int G = 0;
            int T = 0;
            int C = 0;

            char[] nucs = scaffoldStrand.ToCharArray();
            List<helixContainerSlot> slots = new();
            List<helixContainerSlot> slotsOpp = new();

            Color32 strandCol = strandSpawn.givenColour;

            if (testOrBridge == true) { 
                slots = allHCsList[0]["topSlot"];
                slotsOpp = allHCsList[0]["bottomSlot"];
            }
            else { 
                slots = allHCsList[0]["bottomSlot"];
                slotsOpp = allHCsList[0]["topSlot"];
            }            

            for (int i = 0; i < nucs.Length; i++)
            {
                if (nucs[i] == 'A') { A++; }
                if (nucs[i] == 'G') { G++; }
                if (nucs[i] == 'T') { T++; }
                if (nucs[i] == 'C') { C++; }

                int j = nucs.Length - 1 - i;

                slots[j].childImg.color = strandCol;
                slotsOpp[j].childImg.color = new Color32(255, 255, 255, 90);


                if (!testOrBridge)
                {
                    if (nucs[i] == 'A') { slots[j].childImg.sprite = nucleotideSprites[4]; }
                    else if (nucs[i] == 'C') { slots[j].childImg.sprite = nucleotideSprites[5]; }
                    else if (nucs[i] == 'G') { slots[j].childImg.sprite = nucleotideSprites[6]; }
                    else if (nucs[i] == 'T') { slots[j].childImg.sprite = nucleotideSprites[7]; }
                }
                else
                {
                    if (nucs[i] == 'A') { slots[i].childImg.sprite = nucleotideSprites[4]; }
                    else if (nucs[i] == 'C') { slots[i].childImg.sprite = nucleotideSprites[5]; }
                    else if (nucs[i] == 'G') { slots[i].childImg.sprite = nucleotideSprites[6]; }
                    else if (nucs[i] == 'T') { slots[i].childImg.sprite = nucleotideSprites[7]; }
                }
            }   
        }

        else if(bridgeType == 2)
        {
            //should be only used during bridge fixing
            
            ArrayNuc[,] nuc2D = origamiSpawn.puzzleNucs; //2d array holding data for Helix Containers

            for(int i = 0; i < nuc2D.GetLength(0); i++)
            {
                List<helixContainerSlot> slots = new List<helixContainerSlot>();
                List<helixContainerSlot> slotsOpp = new List<helixContainerSlot>();

                if (i % 2 == 0)
                {
                    slots = allHCsList[i / 2]["topSlot"];
                    slotsOpp = allHCsList[i / 2]["bottomSlot"];
                }

                else
                {
                    slots = allHCsList[i / 2]["bottomSlot"];
                    slotsOpp = allHCsList[i / 2]["topSlot"];
                }
 
                for(int j = 0; j < nuc2D.GetLength(1); j++)
                {
                    if (nuc2D[i, j] != null)
                    {
                        //set strand colour here
                        slots[j].childImg.color = hcMatColours[nuc2D[i,j].strand % 6];

                        if (nuc2D[i, j].nuc == 'A') { slots[j].childImg.sprite = nucleotideSprites[4]; }
                        else if (nuc2D[i, j].nuc == 'C') { slots[j].childImg.sprite = nucleotideSprites[5]; }
                        else if (nuc2D[i, j].nuc == 'G') { slots[j].childImg.sprite = nucleotideSprites[6]; }
                        else if (nuc2D[i, j].nuc == 'T') { slots[j].childImg.sprite = nucleotideSprites[7]; }
                    }
                    else slots[j].childImg.color = new Color32(255, 255, 255, 90);
                }
            }
        }
    }

    //to be called by CharacterController when this inventory menu is closed
    public void bridgeClosed()
    {
        resetStapleStrand();
    }

    //called when the "Reset" button is clicked, or by bridgeClosed()
    public void resetStapleStrand()
    {
        string eventInfo = "[CLICK] BMM Design Tab Reset clicked. ";
        AllLoggers.getFirstLogger().addLine("[RES_CL]", eventInfo);
        if (stapleSection.activeSelf) {
            for (int i = 0; i < stapleSlots.Count; i++) {
                stapleSlots[i].image.color = new Color32(55, 95, 142, 150);
                stapleSlots[i].image.sprite = null;
                stapleSlots[i].clicked = false;
            }

            if(userGivenStapleString != null) {
                for (int i = 0; i < userGivenStapleString.Length; i++) {
                    char c = userGivenStapleString[i];
                    if (c == 'A') { logic.addNucleotideA(1); }
                    if (c == 'T') { logic.addNucleotideT(1); }
                    if (c == 'G') { logic.addNucleotideG(1); }
                    if (c == 'C') { logic.addNucleotideC(1); }
                }
            }
            userGivenStapleString = null;
            currentStaple = 0;
            refreshCursorPosition();
        }
    }

    //called when the "synthesize" button is clicked.  
    //- check if input exists [not null]
    //- send the staple to prod tab, regardless of valid or not 
    public void submitStaple()
    {
        string eventInfo = "[CLICK] BMM Design Tab Synthesise clicked. ";
        AllLoggers.getFirstLogger().addLine("[SYN_CL]", eventInfo);

        if (synthesizePrompt.activeSelf) synthesizePrompt.SetActive(false);

        if (userGivenStapleString == "" || userGivenStapleString == null) { return; }

        int A = 0;
        int G = 0;
        int T = 0;
        int C = 0;

        psl.addToList(userGivenStapleString);

        //Parse stapleString for nuc counts
        char[] nucs = userGivenStapleString.ToCharArray();
        for(int i = 0; i < nucs.Length; i++) {
            if (nucs[i] == 'A') { A++; }
            if (nucs[i] == 'G') { G++; }
            if (nucs[i] == 'T') { T++; }
            if (nucs[i] == 'C') { C++; }
        }
       
        //reset stapleString
        userGivenStapleString = null;

        resetStapleStrand();
    }

    public void setBridgeType(int num) { bridgeType = num; }

    //deletes a single nuc to be filled in by something else 
    public void deleteNuc()
    {
        string eventInfo = "[CLICK] Delete button in BMM clicked.";
        AllLoggers.getFirstLogger().addLine("[DEL_CL]", eventInfo);
        if (deleteClicked) { deleteButton.GetComponent<Image>().sprite = deleteUnselected; }
        else { deleteButton.GetComponent<Image>().sprite = deleteSelected; }

        deleteClicked = !deleteClicked;
    }

    public void redrawStapleSlots() //used when deleting staple slots
    {
        //delete everything
        for(int i = 0; i < stapleSlots.Count; i++) {
            stapleSlots[i].image.color = new Color32(55, 95, 142, 150);
            stapleSlots[i].image.sprite = null;
            stapleSlots[i].clicked = false;
        }

        for(int i = 0; i < userGivenStapleString.Length; i++)
        {

            stapleSlots[i].image.color = Color.white;
            stapleSlots[i].clicked = true;

            if (userGivenStapleString[i] == 'A') { stapleSlots[i].image.sprite = nucleotideSprites[0]; }
            else if (userGivenStapleString[i] == 'G') { stapleSlots[i].image.sprite = nucleotideSprites[2]; }
            else if (userGivenStapleString[i] == 'T') { stapleSlots[i].image.sprite = nucleotideSprites[3]; }
            else if (userGivenStapleString[i] == 'C') { stapleSlots[i].image.sprite = nucleotideSprites[1]; }
        }
    }

    public bool getDeleteClicked() { return deleteClicked; }

    //gets called when annealClicked from prodStapleList is called. 
    // == gets called when the anneal button in prod tab is clicked. 
    //ans key is provided by user
    public bool prodAnnealCheck(List<string> ansKey)
    {
        if (!player.getOnSpawnPoint())
        {
            string eventInfo = "Anneal clicked when not standing on spawn point.";
            AllLoggers.getFirstLogger().addLine("BMM_PRO_ANN_MIST", eventInfo);
            if(bridgeType < 2)
            {
                userGivenStapleString = ansKey[0];
                for (int i = 0; i < userGivenStapleString.Length; i++)
                {
                    if (userGivenStapleString[i] == 'A') { logic.addScatteredA(1); }
                    if (userGivenStapleString[i] == 'G') { logic.addScatteredG(1); }
                    if (userGivenStapleString[i] == 'T') { logic.addScatteredT(1); }
                    if (userGivenStapleString[i] == 'C') { logic.addScatteredC(1); }
                }
                userGivenStapleString = null;
            }

            if(!bankPromptShown) {  showBankPrompt(); }

            return false;
        }

        if(bridgeType < 2)
        {
            //if the bridge is either nuc pair or strand pair, then there should only be one answer
            userGivenStapleString = ansKey[0];

            if (ansKey.Count > 1) {
                wrongAnswers++;
                for (int i = 0; i < userGivenStapleString.Length; i++)
                {
                    if (userGivenStapleString[i] == 'A') { logic.addScatteredA(1); }
                    if (userGivenStapleString[i] == 'G') { logic.addScatteredG(1); }
                    if (userGivenStapleString[i] == 'T') { logic.addScatteredT(1); }
                    if (userGivenStapleString[i] == 'C') { logic.addScatteredC(1); }
                }

                userGivenStapleString = null;
                if (!bankPromptShown) { showBankPrompt(); }
                return false; 
            }

            if (userGivenStapleString != reqStapleString || userGivenStapleString.Length == 0)
            {
                
                Debug.Log("wrong input given.");
                Debug.Log(userGivenStapleString + " " + reqStapleString);
                Debug.Log(" fnkjsnsekjfnskejnf " + userGivenStapleString);
                wrongAnswers++;

                for (int i = 0; i < userGivenStapleString.Length; i++)
                {
                    if (userGivenStapleString[i] == 'A') { logic.addScatteredA(1); }
                    if (userGivenStapleString[i] == 'G') { logic.addScatteredG(1); }
                    if (userGivenStapleString[i] == 'T') { logic.addScatteredT(1); }
                    if (userGivenStapleString[i] == 'C') { logic.addScatteredC(1); }
                }

                userGivenStapleString = null;
                resetStapleStrand();
                if (!bankPromptShown) { showBankPrompt(); }
                return false;
            }

            //change respective helix container image colours
            List<helixContainerSlot> slots = new List<helixContainerSlot>();

            Color32 strandCol;

            if (bridgeType == 1) strandCol = strandSpawn.missingColour;
            else strandCol = Color.white;

            if (testOrBridge == false) { slots = allHCsList[0]["topSlot"]; }
            else { slots = allHCsList[0]["bottomSlot"]; }

            char[] nucs = userGivenStapleString.ToCharArray();

            for (int i = 0; i < nucs.Length; i++)
            {
                int j = nucs.Length - 1 - i;
                int x; 

                if(testOrBridge) x = j; 
                else x = i;

                slots[x].childImg.color = strandCol;

                if(bridgeType == 1)
                {
                    /*slots[j].showStrand(true);
                    slots[j].strandColour(strandCol);
                    slots[j].strLeft.SetActive(false);
                    slots[j].strRight.SetActive(false);*/
                }

                if (nucs[i] == 'A' ) { 
                    if (bridgeType == 0) slots[x].childImg.sprite = nucleotideSprites[0];
                    if (bridgeType == 1) slots[x].childImg.sprite = nucleotideSprites[4];
                }
                else if (nucs[i] == 'C') {
                    if (bridgeType == 0) slots[x].childImg.sprite = nucleotideSprites[1];
                    if (bridgeType == 1) slots[x].childImg.sprite = nucleotideSprites[5];
                }
                else if (nucs[i] == 'G') {
                    if (bridgeType == 0) slots[x].childImg.sprite = nucleotideSprites[2];
                    if (bridgeType == 1) slots[x].childImg.sprite = nucleotideSprites[6];
                }
                else if (nucs[i] == 'T') {
                    if (bridgeType == 0) slots[x].childImg.sprite = nucleotideSprites[3];
                    if (bridgeType == 1) slots[x].childImg.sprite = nucleotideSprites[7];
                }
            }   

            //Fix the bridge
            if (bridgeType == 0) { simpleStrandSpawn.fixBridge(); }
            else if (bridgeType == 1) { strandSpawn.fixBridge(); }
            
            userGivenStapleString = "";
        }

        else
        {
            //check answers
            for(int i = 0; i < origamiSpawn.staplesContentSeparated.Count; i++)
            {
                List<string> forwardCheck = ansKey.Except(origamiSpawn.staplesContentSeparated[i]).ToList();
                List<string> backwardCheck = (origamiSpawn.staplesContentSeparated[i]).Except(ansKey).ToList();

                if(forwardCheck.Count == 0 && backwardCheck.Count == 0) { break; }
                if(i == origamiSpawn.staplesContentSeparated.Count - 1) { return false; } //need to double check this
            }

            // set Helix Container image colours
            if (!origamiSpawn.brokenHC)
            {
                ArrayNuc[,] nuc2D = origamiSpawn.solutionNucs; //2d array holding data for Helix Containers
                int matCount = origamiSpawn.returnMatCount();

                for (int i = 0; i < nuc2D.GetLength(0); i++)
                {
                    List<helixContainerSlot> slots = new List<helixContainerSlot>();

                    if (i % 2 == 0) slots = allHCsList[i / 2]["topSlot"];
                    else slots = allHCsList[i / 2]["bottomSlot"];

                    for (int j = 0; j < nuc2D.GetLength(1); j++)
                    {
                        if (nuc2D[i, j] != null)
                        {
                            //set strand colour here
                            slots[j].childImg.color = hcMatColours[(nuc2D[i, j].strand + matCount) % 6];

                            if (nuc2D[i, j].nuc == 'A') { slots[j].childImg.sprite = nucleotideSprites[4]; }
                            else if (nuc2D[i, j].nuc == 'C') { slots[j].childImg.sprite = nucleotideSprites[5]; }
                            else if (nuc2D[i, j].nuc == 'G') { slots[j].childImg.sprite = nucleotideSprites[6]; }
                            else if (nuc2D[i, j].nuc == 'T') { slots[j].childImg.sprite = nucleotideSprites[7]; }
                        }
                    }
                }
            }

            origamiSpawn.fixBridge();
        }

        correctAnswers++;
        logic.addFixedBridgeCount();

        return true;
    }

    public void leftArrowClicked()
    {
        string eventInfo = "[CLICK] BMM Design tab left arrow clicked.";
        AllLoggers.getFirstLogger().addLine("BMM_DES_LEFT_CL", eventInfo);
        if(currentStaple != 0)
        {
            currentStaple--;
            refreshCursorPosition();
        }
    }

    public void rightArrowClicked()
    {
        string eventInfo = "[CLICK] BMM Design tab right arrow clicked.";
        AllLoggers.getFirstLogger().addLine("BMM_DES_RIGHT_CL", eventInfo);
        if (currentStaple != userGivenStapleString.Length)
        {
            currentStaple++;
            refreshCursorPosition();
        }
    }

    public void refreshCursorPosition()
    {
        for (int i = 0; i < cursors.Count; i++)
        {
            if (i == currentStaple) cursors[i].SetActive(true);
            else cursors[i].SetActive(false);
        }
    }

    public int getBridgeType()
    { return bridgeType; }

    public int GetCurrentStaple()
    { return currentStaple; }

    public void SetCurrentStaple(int x)
    { currentStaple = x; }

    IEnumerator showNotice()
    {
        scoreUpdateSignalImage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        scoreUpdateSignalImage.SetActive(false);
    }

    public void showBankPrompt()
    {
        bankTutorial.SetActive(true);
        bankPromptShown = true;
    }

    public void origamiHintsOpen()
    {
        if(origamiSpawn != null && origamiSpawn.helpButton != null) { origamiSpawn.openHint(); }
    }
    public void origamiHintsClose()
    {
        if (origamiSpawn != null && origamiSpawn.helpButton != null)
        { origamiSpawn.closeHint(); }
    }

}
