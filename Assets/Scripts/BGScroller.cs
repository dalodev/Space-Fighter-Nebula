using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

    public float verticalScrollSpeed = 1.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;

        newTextureOffset.y += verticalScrollSpeed * Time.deltaTime;

        GetComponent<Renderer>().material.mainTextureOffset = newTextureOffset;
    }
}
