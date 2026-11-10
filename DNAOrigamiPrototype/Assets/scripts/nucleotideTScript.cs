using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nucleotideTScript : MonoBehaviour
{
    public logicManager logic;
    [SerializeField] private AudioClip picked;
    scoreTracker score;

    // Start is called before the first frame update
    void Start() {
        //as soon as a new foragable nucleotide spawns, it will look through the hierarchy to find the logic script, then find its script "logicManager"
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<logicManager>();
        score = GameObject.FindGameObjectWithTag("scoreTracker").GetComponent<scoreTracker>();
    }

    private void OnTriggerEnter(Collider other) {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        string eventInfo = "user picked up Nuc T at [" + xPos + " | " + yPos + " | " + zPos + "]";
        AllLoggers.getFirstLogger().addLine("NUC_T_PICKED", eventInfo);

        AudioSource.PlayClipAtPoint(picked, transform.position, 1f);

        logic.addNucleotideT(1);
        score.nucPicked("T");
        Destroy(gameObject);
    }
}
