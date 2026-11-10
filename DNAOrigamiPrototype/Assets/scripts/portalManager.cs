using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class portalManager : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public playerController pc;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 pos = new Vector3 (x, y, z);
        pc.repositionPlayer(0, pos);
    }
}
