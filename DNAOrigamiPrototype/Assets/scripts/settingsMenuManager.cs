using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class settingsMenuManager : MonoBehaviour
{
    private void Start()
    {
       transform.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        AllLoggers.getFirstLogger().addLine("QUIT_GAME", "player exit game.");
        AllLoggers.getFirstLogger().dumpLogFile();
        Application.Quit();
    }
}
