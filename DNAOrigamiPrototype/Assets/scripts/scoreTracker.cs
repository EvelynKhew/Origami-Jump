using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreTracker : MonoBehaviour
{
    public TMP_Text scoreText;
    int score = 0;
    string reqNuc = "A"; 


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void nucPicked(string nuc) {
        if(nuc == reqNuc) { //correct nucleotide foraged
            reqNuc = "";
            addPlayerScore(1);
        }
        else { deductPlayerScore(1); } //incorrect nucleotide foraged
    }

    public void bridgeFormed() { addPlayerScore(5); }

    public void addPlayerScore(int num) { score += num; }

    public void deductPlayerScore(int num) {
        score -= num;
        if (score < 0) { score = 0; }
    }

    //not used, maybe unneccesary? 
    public int getPlayerScore() { return score; }

    public void setRequiredNuc(string nuc) { reqNuc = nuc; }
}
