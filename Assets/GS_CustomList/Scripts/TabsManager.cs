using UnityEngine;
using System.Collections;


public enum TabState
{
    Active,
    Inactive
}

public class TabsManager : MonoBehaviour {

    TabButton prevActiveTab;

    void Start()
    {
    }

    public void OnClickTabButton(TabButton tabBtn){
        prevActiveTab.tabPanel.ChangeTabState(false);
        prevActiveTab = tabBtn;
        tabBtn.tabPanel.ChangeTabState(true);
    }
}