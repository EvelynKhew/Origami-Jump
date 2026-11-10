using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public TabGroup tabs;
    public TMP_Text label;
    public string tabName;

    public tutorialManager tutorialManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        tabs.OnTabSelected(this);
        if(tabName == "Production Tab" && tutorialManager.getProdCloseTut()) { tutorialManager.clearTuts(); }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabs.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabs.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        tabs.subscribe(this);
    }
}
