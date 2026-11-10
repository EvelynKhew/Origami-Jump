using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseLook : MonoBehaviour
{
    public Transform playerBody;
    public Slider slider;
    float xRotation = 0f;
    public float mouseSensitivity = 270f;
    float adjustedSensitivity = 270f;
    bool movement = true;
    bool initiated = false;

    // Start is called before the first frame update
    public void initialiseMouse()
    {
        if (!initiated)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //prevent player from looking 360 degress, so they can only look max up to the sky (90) and down to the ground (-90) and nothing beyond that

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
 
    }
    
    public void toggleMovement()
    {
        Debug.Log("movement toggled. " +  movement);
        if(movement){mouseSensitivity = 0f;}
        else{ 
            mouseSensitivity = adjustedSensitivity;
            Cursor.lockState = CursorLockMode.Locked;
        }

        movement = !movement;
    }

    public void turnMovementOff()
    {
        movement = false;
        mouseSensitivity = 0f;
    }

    public void turnMovementOn()
    {
        movement = true;
        mouseSensitivity = adjustedSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool getMovement()
    {
        return movement;
    }

    public void adjustSensitivity()
    {
        adjustedSensitivity = 270f * slider.GetComponent<Slider>().value;

        string eventLog = "mouse sensitivity adjusted to " + adjustedSensitivity;
        AllLoggers.getFirstLogger().addLine("MOUSE_SENS", eventLog); //respawn point entered

    }

    public float getxRotation()
    {
        return xRotation;
    }
}
