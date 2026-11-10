using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreValueScript : MonoBehaviour
{
    public GameObject scoreText;
    public logicManager logic;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = logic.getFixedBridgesCount().ToString() + " / 25";
    }

    private void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = logic.getFixedBridgesCount().ToString() + " / 25"; 
    }

}
