using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwn16_17PuzData : MonoBehaviour
{
    OrigamiBridgeSpawnScript oriSpawn;

    ArrayNuc[,] nucs = new ArrayNuc[6, 20];
    ArrayNuc[,] ans = new ArrayNuc[6, 16];

    // Start is called before the first frame update
    void Start()
    {
        oriSpawn = transform.GetComponent<OrigamiBridgeSpawnScript>();

        //nucs [X, Y]:
        //if X even,    5-3
        //else          3-5
        //strand colour determined by int strand of each ArrayNuc

        //first strand 5-3
        nucs[0, 0] = new ArrayNuc('G', 0);
        nucs[0, 1] = new ArrayNuc('T', 0);
        nucs[0, 2] = new ArrayNuc('C', 0);
        nucs[0, 3] = new ArrayNuc('T', 0);
        nucs[0, 4] = new ArrayNuc('C', 0);
        nucs[0, 5] = new ArrayNuc('T', 0);
        nucs[0, 6] = new ArrayNuc('A', 0);
        nucs[0, 7] = new ArrayNuc('C', 0);
        nucs[0, 8] = new ArrayNuc('G', 0);
        nucs[0, 9] = new ArrayNuc('A', 0);
        nucs[0, 10] = new ArrayNuc('C', 0);
        nucs[0, 11] = new ArrayNuc('C', 0);
        nucs[0, 12] = new ArrayNuc('G', 0);
        nucs[0, 13] = new ArrayNuc('G', 0);
        nucs[0, 14] = new ArrayNuc('T', 0);
        nucs[0, 15] = new ArrayNuc('G', 0);
        nucs[0, 16] = new ArrayNuc('C', 0);
        nucs[0, 17] = new ArrayNuc('T', 0);
        nucs[0, 18] = new ArrayNuc('C', 0);
        nucs[0, 19] = new ArrayNuc('G', 0);


        //second strand 3-5
        nucs[3, 0] = new ArrayNuc('A', 1);
        nucs[3, 1] = new ArrayNuc('G', 1);
        nucs[3, 2] = new ArrayNuc('T', 1);
        nucs[3, 3] = new ArrayNuc('G', 1);
        nucs[3, 4] = new ArrayNuc('C', 1);
        nucs[3, 5] = new ArrayNuc('A', 1);
        nucs[3, 6] = new ArrayNuc('G', 1);
        nucs[3, 7] = new ArrayNuc('C', 1);
        nucs[3, 8] = new ArrayNuc('C', 1);
        nucs[3, 9] = new ArrayNuc('G', 1);
        nucs[3, 10] = new ArrayNuc('C', 1);
        nucs[3, 11] = new ArrayNuc('T', 1);
        nucs[3, 12] = new ArrayNuc('T', 1);
        nucs[3, 13] = new ArrayNuc('T', 1);
        nucs[3, 14] = new ArrayNuc('A', 1);
        nucs[3, 15] = new ArrayNuc('A', 1);
        nucs[3, 16] = new ArrayNuc('T', 1);
        nucs[3, 17] = new ArrayNuc('T', 1);
        nucs[3, 18] = new ArrayNuc('T', 1);
        nucs[3, 19] = new ArrayNuc('A', 1);

        //third strand 5-3
        nucs[4, 0] = new ArrayNuc('T', 2);
        nucs[4, 1] = new ArrayNuc('C', 2);
        nucs[4, 2] = new ArrayNuc('C', 2);
        nucs[4, 3] = new ArrayNuc('G', 2);
        nucs[4, 4] = new ArrayNuc('T', 2);
        nucs[4, 5] = new ArrayNuc('G', 2);
        nucs[4, 6] = new ArrayNuc('A', 2);
        nucs[4, 7] = new ArrayNuc('C', 2);
        nucs[4, 8] = new ArrayNuc('G', 2);
        nucs[4, 9] = new ArrayNuc('G', 2);
        nucs[4, 10] = new ArrayNuc('A', 2);
        nucs[4, 11] = new ArrayNuc('C', 2);
        nucs[4, 12] = new ArrayNuc('C', 2);
        nucs[4, 13] = new ArrayNuc('T', 2);
        nucs[4, 14] = new ArrayNuc('T', 2);
        nucs[4, 15] = new ArrayNuc('A', 2);
        nucs[4, 16] = new ArrayNuc('C', 2);
        nucs[4, 17] = new ArrayNuc('A', 2);
        nucs[4, 18] = new ArrayNuc('G', 2);
        nucs[4, 19] = new ArrayNuc('T', 2);

        //fourth strand 
        nucs[1, 16] = new ArrayNuc('G', 3);
        nucs[1, 17] = new ArrayNuc('A', 3);
        nucs[1, 18] = new ArrayNuc('G', 3);
        nucs[1, 19] = new ArrayNuc('C', 3);

        nucs[2, 16] = new ArrayNuc('A', 3);
        nucs[2, 17] = new ArrayNuc('A', 3);
        nucs[2, 18] = new ArrayNuc('A', 3);
        nucs[2, 19] = new ArrayNuc('T', 3);

        nucs[5, 16] = new ArrayNuc('G', 3);
        nucs[5, 17] = new ArrayNuc('T', 3);
        nucs[5, 18] = new ArrayNuc('C', 3);
        nucs[5, 19] = new ArrayNuc('A', 3);


        //send to the respective spawn point
        oriSpawn.puzzleNucs = nucs;

        //solution strand(s)
        ans[1, 0] = new ArrayNuc('C', 0);
        ans[1, 1] = new ArrayNuc('A', 0);
        ans[1, 2] = new ArrayNuc('G', 0);
        ans[1, 3] = new ArrayNuc('A', 0);
        ans[1, 4] = new ArrayNuc('G', 0);
        ans[1, 5] = new ArrayNuc('A', 0);
        ans[1, 6] = new ArrayNuc('T', 0);
        ans[1, 7] = new ArrayNuc('G', 0);
        ans[1, 8] = new ArrayNuc('C', 0);
        ans[1, 9] = new ArrayNuc('T', 0);
        ans[1, 10] = new ArrayNuc('G', 0);
        ans[1, 11] = new ArrayNuc('G', 0);
        ans[1, 12] = new ArrayNuc('C', 0);
        ans[1, 13] = new ArrayNuc('C', 0);
        ans[1, 14] = new ArrayNuc('A', 0);
        ans[1, 15] = new ArrayNuc('C', 0);

        ans[2, 0] = new ArrayNuc('T', 0);
        ans[2, 1] = new ArrayNuc('C', 0);
        ans[2, 2] = new ArrayNuc('A', 0);
        ans[2, 3] = new ArrayNuc('C', 0);
        ans[2, 4] = new ArrayNuc('G', 0);
        ans[2, 5] = new ArrayNuc('T', 0);
        ans[2, 6] = new ArrayNuc('C', 0);
        ans[2, 7] = new ArrayNuc('G', 0);
        ans[2, 8] = new ArrayNuc('G', 0);
        ans[2, 9] = new ArrayNuc('C', 0);
        ans[2, 10] = new ArrayNuc('G', 0);
        ans[2, 11] = new ArrayNuc('A', 0);
        ans[2, 12] = new ArrayNuc('A', 0);
        ans[2, 13] = new ArrayNuc('A', 0);
        ans[2, 14] = new ArrayNuc('T', 0);
        ans[2, 15] = new ArrayNuc('T', 0);

        ans[5, 0] = new ArrayNuc('A', 0);
        ans[5, 1] = new ArrayNuc('G', 0);
        ans[5, 2] = new ArrayNuc('G', 0);
        ans[5, 3] = new ArrayNuc('C', 0);
        ans[5, 4] = new ArrayNuc('A', 0);
        ans[5, 5] = new ArrayNuc('C', 0);
        ans[5, 6] = new ArrayNuc('T', 0);
        ans[5, 7] = new ArrayNuc('G', 0);
        ans[5, 8] = new ArrayNuc('C', 0);
        ans[5, 9] = new ArrayNuc('C', 0);
        ans[5, 10] = new ArrayNuc('T', 0);
        ans[5, 11] = new ArrayNuc('G', 0);
        ans[5, 12] = new ArrayNuc('G', 0);
        ans[5, 13] = new ArrayNuc('A', 0);
        ans[5, 14] = new ArrayNuc('A', 0);
        ans[5, 15] = new ArrayNuc('T', 0);

        oriSpawn.solutionNucs = ans;
    }
}
