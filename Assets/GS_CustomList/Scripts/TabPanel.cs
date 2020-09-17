using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TabPanel : Image {

    Image img;
    protected override void Start()
    {
        base.Start();
    }

    public void ChangeActiveState(TabState state){

    }

    public void ChangeTabState(bool state)
    {
        gameObject.GetComponentInChildren<GameObject>().SetActive(false) ;
    }


}