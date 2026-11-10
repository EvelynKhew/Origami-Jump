using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwn16_21PuzData : MonoBehaviour
{
    public OrigamiBridgeSpawnScript oriSpawn;

    ArrayNuc[,] nucs = new ArrayNuc[4, 6];
    ArrayNuc[,] ans = new ArrayNuc[4, 6];

    //populate the array
    private void Start()
    {
        //nucs [X, Y]:
        //if X even,    5-3
        //else          3-5
        //strand colour determined by int strand of each ArrayNuc

        //first strand 3-5
        nucs[1, 0] = new ArrayNuc('A', 0);
        nucs[1, 1] = new ArrayNuc('C', 0);
        nucs[1, 2] = new ArrayNuc('T', 0);

        //first strand 5-3
        nucs[2, 0] = new ArrayNuc('T', 0);
        nucs[2, 1] = new ArrayNuc('A', 0);
        nucs[2, 2] = new ArrayNuc('C', 0);

        //second strand 3-5
        nucs[1, 3] = new ArrayNuc('A', 1);
        nucs[1, 4] = new ArrayNuc('C', 1);
        nucs[1, 5] = new ArrayNuc('C', 1);

        //second strand 5-3
        nucs[2, 3] = new ArrayNuc('A', 1);
        nucs[2, 4] = new ArrayNuc('C', 1);
        nucs[2, 5] = new ArrayNuc('T', 1);        

        //send to the respective spawn point
        oriSpawn.puzzleNucs = nucs;

        //solution strand(s)
        ans[0, 0] = new ArrayNuc('T', 0);
        ans[0, 1] = new ArrayNuc('G', 0);
        ans[0, 2] = new ArrayNuc('A', 0);
        ans[0, 3] = new ArrayNuc('T', 0);
        ans[0, 4] = new ArrayNuc('G', 0);
        ans[0, 5] = new ArrayNuc('G', 0);

        ans[3, 0] = new ArrayNuc('A', 0);
        ans[3, 1] = new ArrayNuc('T', 0);
        ans[3, 2] = new ArrayNuc('G', 0);
        ans[3, 3] = new ArrayNuc('T', 0);
        ans[3, 4] = new ArrayNuc('G', 0);
        ans[3, 5] = new ArrayNuc('A', 0);

        oriSpawn.solutionNucs = ans;
    } 
}
