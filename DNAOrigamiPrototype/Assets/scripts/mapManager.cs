using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public void relocateMap(Vector3 pos)
    { transform.position = pos; }

    public void orientateMap(float zVal)
    { transform.eulerAngles = new Vector3(90, 0, zVal); }


    public void Reset() {
        transform.position = Vector3.zero; 
        transform.eulerAngles = Vector3.zero;
    }
}
