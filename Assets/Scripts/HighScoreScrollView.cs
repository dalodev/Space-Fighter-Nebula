using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreScrollView : MonoBehaviour {

    public GameObject textPrefab;
    private List<GameObject> items;

	// Use this for initialization
	void Start () {


        AddItem();
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void AddItem()
    {
        if(textPrefab != null)
        {
            items.Add(textPrefab);

        }
        else
        {
            Debug.Log("Text Prefab null");
        }

    }
}
