using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public GameObject textBox;
    public List<GameObject> texts;
    public GameObject hintArrow;
    public GameObject firstSpawn;

    public GameObject nucleotideHintUI;
    public GameObject synthesisePrompt;

    bool labViewed = false;
    bool prodCloseTut = false;

    public void hintForPickingUpNucs()
    {
        textBox.SetActive(true);
        hintArrow.SetActive(true);
        texts[0].SetActive(true);
    }

    public void hintForRespPt()
    {
        firstSpawn.GetComponent<CapsuleCollider>().enabled = true;
        texts[1].SetActive(true);
        hintArrow.transform.position = new Vector3(0, 10f, 18.5f);
    }

    public void hintOpenMenu()
    {
        texts[2].SetActive(true);
        hintArrow.SetActive(false);
    }

    public void clearTuts()
    {
        textBox.SetActive(false);
        synthesisePrompt.SetActive(false);
    }

    //highlights either nucleotide T or nucleotide A with a red circle
    public void hintAtNucleotide()
    {
        //TODO
        nucleotideHintUI.SetActive(true);
        prodCloseTut = true;
    }

    //shows hint at the synthesize box in Design Lab tab
    public void hintAtSynthesize()
    {
        nucleotideHintUI.SetActive(false);
        synthesisePrompt.SetActive(true);   
    }


    public bool getLabViewed()
    {
        return labViewed;
    }

    public void setLabViewed(bool x)
    {
        labViewed = x;
    }
    
    public bool getProdCloseTut()
    {
        return prodCloseTut;
    }
}
