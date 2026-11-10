using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class worldMapManager : MonoBehaviour
{

    public GameObject mapCamera;
    Vector3 position;
    float yRotation = 0f;

    void Start()
    {
        position = mapCamera.transform.position;
        transform.gameObject.SetActive(false);
    }

    public void upClicked()
    {
        string eventInfo = "[CLICK] World Map up clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_UP_CL]", eventInfo);

        if (yRotation == 0f) position.z += 20;
        else if(yRotation == 180f || yRotation == -180f) position.z -= 20;
        else if(yRotation == -90f || yRotation == 270f) position.x -= 20;
        else position.x += 20;

        mapCamera.transform.position = position;
    }

    public void downClicked()
    {
        string eventInfo = "[CLICK] World Map down clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_DOWN_CL]", eventInfo);
        if (yRotation == 0f) position.z -= 20;
        else if (yRotation == 180f || yRotation == -180f) position.z += 20;
        else if (yRotation == -90f || yRotation == 270f) position.x += 20;
        else position.x -= 20;

        mapCamera.transform.position = position;
    }

    public void leftClicked()
    {
        string eventInfo = "[CLICK] World Map left clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_LEFT_CL]", eventInfo);
        if (yRotation == 0f) position.x -= 20;
        else if (yRotation == 180f || yRotation == -180f) position.x += 20;
        else if (yRotation == -90f || yRotation == 270f) position.z -= 20;
        else position.z += 20;

        mapCamera.transform.position = position;
    }

    public void rightClicked()
    {
        string eventInfo = "[CLICK] World Map right clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_RIGHT_CL]", eventInfo);
        if (yRotation == 0f) position.x += 20;
        else if (yRotation == 180f || yRotation == -180f) position.x -= 20;
        else if (yRotation == -90f || yRotation == 270f) position.z += 20;
        else position.z -= 20;

        mapCamera.transform.position = position;
    }
    
    public void zoomInClicked()
    {
        string eventInfo = "[CLICK] World Map zoom in clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_ZI_CL]", eventInfo);
        if (position.y >= 80) position.y -= 20;

        mapCamera.transform.position = position;
    }

    public void zoomOutClicked()
    {
        string eventInfo = "[CLICK] World Map zoom out clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_ZI_CL]", eventInfo);
        if (position.y <= 200) position.y += 20;

        mapCamera.transform.position = position;
    }

    public void rotateClockwiseClicked()
    {
        string eventInfo = "[CLICK] World Map rotate clockwise clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_RC_CL]", eventInfo);
        yRotation = (yRotation + 90f) % 360; 
        mapCamera.transform.localRotation = Quaternion.Euler(90f, yRotation, 0f);
    }

    public void rotateAntiClockwiseClicked()
    {
        string eventInfo = "[CLICK] World Map rotate anti-clockwise clicked. ";
        AllLoggers.getFirstLogger().addLine("[MAP_RA_CL]", eventInfo);
        yRotation = (yRotation - 90f) % 360; 
        mapCamera.transform.localRotation = Quaternion.Euler(90f, yRotation, 0f);
    }
}
