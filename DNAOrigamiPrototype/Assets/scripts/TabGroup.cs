using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> buttons;
    public TabButton selectedTab;
    public TabButton initTab;
    public List<GameObject> swapObjs;

    public GameObject HelixConts;
    public GameObject dnaUnderline;

    private void Start()
    {
        OnTabSelected(initTab);

        swapObjs[1].SetActive(false);
        swapObjs[2].SetActive(false);
    }

    public void subscribe(TabButton button)
    {
        if(buttons == null)
            buttons = new List<TabButton>();

        buttons.Add(button);

    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.label.color = Color.white; //set the text to "HOVER" colour
        }
    }

    public void OnTabExit(TabButton button) { ResetTabs(); }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        button.label.color = Color.white;
        ResetTabs();
        //set the text to be "SELECTED" Colour


        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < swapObjs.Count; i++)
        {
            if (i == index) 
                swapObjs[i].SetActive(true);
            else
                swapObjs[i].SetActive(false);

            if(index != 2)
                HelixConts.SetActive(true);
            else 
                HelixConts.SetActive(false);
        }

        string eventInfo = "[CLICK] " + button.tabName + " clicked.";
        AllLoggers.getFirstLogger().addLine("BMM_TAB_SWP", eventInfo);
    }

    public void ResetTabs()
    {
        foreach (TabButton button in buttons)
        {
            if (selectedTab == button && selectedTab != null)
                continue; //skip over the currently selected tab so it doesn't get reset as well

            //set every tab to be IDLE [grayed out words]
            button.label.color = Color.gray;
        }
    }
}
