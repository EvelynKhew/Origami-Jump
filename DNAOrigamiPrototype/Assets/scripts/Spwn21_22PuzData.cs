using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwn21_22PuzData : MonoBehaviour
{
    OrigamiBridgeSpawnScript oriSpawn;

    ArrayNuc[,] nucs = new ArrayNuc[4, 6];
    ArrayNuc[,] ans = new ArrayNuc[3, 6];

    // Start is called before the first frame update
    void Start()
    {
        oriSpawn = transform.GetComponent<OrigamiBridgeSpawnScript>();

        //nucs [X, Y]:
        //if X even,    5-3
        //else          3-5
        //strand colour determined by int strand of each ArrayNuc

        nucs[0, 0] = new ArrayNuc('T', 0);
        nucs[0, 1] = new ArrayNuc('T', 0);
        nucs[0, 2] = new ArrayNuc('A', 0);
        nucs[0, 3] = new ArrayNuc('G', 0);
        nucs[0, 4] = new ArrayNuc('C', 0);
        nucs[0, 5] = new ArrayNuc('T', 0);

        nucs[3, 0] = new ArrayNuc('A', 0);
        nucs[3, 1] = new ArrayNuc('C', 0);
        nucs[3, 2] = new ArrayNuc('G', 0);
        nucs[3, 3] = new ArrayNuc('G', 0);
        nucs[3, 4] = new ArrayNuc('C', 0);
        nucs[3, 5] = new ArrayNuc('C', 0);

        //send to the respective spawn point
        oriSpawn.puzzleNucs = nucs;

        //solution strand(s)
        ans[1, 0] = new ArrayNuc('A', 0);
        ans[1, 1] = new ArrayNuc('A', 0);
        ans[1, 2] = new ArrayNuc('T', 0);
        ans[1, 3] = new ArrayNuc('C', 1);
        ans[1, 4] = new ArrayNuc('G', 1);
        ans[1, 5] = new ArrayNuc('A', 1);

        ans[2, 0] = new ArrayNuc('T', 0);
        ans[2, 1] = new ArrayNuc('G', 0);
        ans[2, 2] = new ArrayNuc('C', 0);
        ans[2, 3] = new ArrayNuc('G', 1);
        ans[2, 4] = new ArrayNuc('C', 1);
        ans[2, 5] = new ArrayNuc('C', 1);

        oriSpawn.solutionNucs = ans;
    }
}
