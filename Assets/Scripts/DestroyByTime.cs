using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float lifeTime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeTime);
        if (PlayerPrefs.GetInt("MuteSound") == 1)
        {
            //btnMute.image.sprite = muteSprites[1];
            GCSurvival survival = FindObjectOfType(typeof(GCSurvival)) as GCSurvival;
            survival.MuteSound(); ;
        }
    }
	
	
}
