using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwn10_11PuzData : MonoBehaviour
{
    public OrigamiBridgeSpawnScript oriSpawn;

    ArrayNuc[,] nucs = new ArrayNuc[4,6];
    ArrayNuc[,] ans = new ArrayNuc[3,3];
    public List<GameObject> forageStrands;

    //populate the array
    private void Start()
    {
        //nucs [X, Y]:
        //if X even,    5-3
        //else          3-5
        //strand colour determined by int strand of each ArrayNuc

        //first strand 5-3
        nucs[0, 0] = new ArrayNuc('C', 0);
        nucs[0, 1] = new ArrayNuc('G', 0);
        nucs[0, 2] = new ArrayNuc('A', 0);
        nucs[0, 3] = new ArrayNuc('T', 0);
        nucs[0, 4] = new ArrayNuc('C', 0);
        nucs[0, 5] = new ArrayNuc('A', 0);

        //second strand 3-5
        nucs[1, 3] = new ArrayNuc('A', 1);
        nucs[1, 4] = new ArrayNuc('G', 1);
        nucs[1, 5] = new ArrayNuc('T', 1);
        
        //second strand 5-3
        nucs[2, 3] = new ArrayNuc('A', 1);
        nucs[2, 4] = new ArrayNuc('G', 1);
        nucs[2, 5] = new ArrayNuc('C', 1);

        //third strand 3-5
        nucs[3, 0] = new ArrayNuc('A', 2);
        nucs[3, 1] = new ArrayNuc('G', 2);
        nucs[3, 2] = new ArrayNuc('C', 2);
        nucs[3, 3] = new ArrayNuc('T', 2);
        nucs[3, 4] = new ArrayNuc('C', 2);
        nucs[3, 5] = new ArrayNuc('G', 2);

        //send to the respective spawn point
        oriSpawn.puzzleNucs = nucs;

        //solution strand(s)
        ans[1, 0] = new ArrayNuc('G', 0);
        ans[1, 1] = new ArrayNuc('C', 0);
        ans[1, 2] = new ArrayNuc('T', 0);

        ans[2, 0] = new ArrayNuc('T', 0);
        ans[2, 1] = new ArrayNuc('C', 0);
        ans[2, 2] = new ArrayNuc('G', 0);

        oriSpawn.solutionNucs = ans;
        oriSpawn.forages = forageStrands;
    }

}
