using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TabButton : Button {

    public int tabID;

	// Use this for initialization
    public TabPanel tabPanel;
    TabState tabState;
	protected override void Start () {

        base.Start();
        int.TryParse(name.Substring(name.Length -1, 1), out tabID);
        print(tabID);
        tabPanel = GameObject.Find("TabPanel"+ tabID).GetComponent<TabPanel>();
	}

}