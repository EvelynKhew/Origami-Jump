using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class invFlush : MonoBehaviour
{

    public logicManager logic;
    public GameObject notice;
    public GameObject noticePERSISTENT;
    private void OnTriggerEnter(Collider other)
    {
        string aClick = "All nucleotides deleted.";
        AllLoggers.getFirstLogger().addLine("INV_FLUSH", aClick); //Inventory A clicked

        string aCount = logic.getNucleotideA().ToString() + "A nucleotide flushed.";
        string gCount = logic.getNucleotideG().ToString() + "G nucleotide flushed.";
        string tCount = logic.getNucleotideT().ToString() + "T nucleotide flushed.";
        string cCount = logic.getNucleotideC().ToString() + "C nucleotide flushed.";

        AllLoggers.getFirstLogger().addLine("INV_FLUSH_A", aCount);
        AllLoggers.getFirstLogger().addLine("INV_FLUSH_G", gCount);
        AllLoggers.getFirstLogger().addLine("INV_FLUSH_C", cCount);
        AllLoggers.getFirstLogger().addLine("INV_FLUSH_T", tCount);

        logic.deleteNucleotideA(logic.getNucleotideA());
        logic.deleteNucleotideG(logic.getNucleotideG());
        logic.deleteNucleotideT(logic.getNucleotideT());
        logic.deleteNucleotideC(logic.getNucleotideC());

        Debug.Log("nucleotides flushed!");
        notice.GetComponent<tutorialTimer>().initialiseTut();
        notice.GetComponent<tutorialTimer>().opened = true;
        notice.SetActive(true);
        this.GetComponent<BoxCollider>().isTrigger = false;
        
        noticePERSISTENT.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
