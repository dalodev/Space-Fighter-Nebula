using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class SoundText : MonoBehaviour {

    public Text text;
    public AudioSource cta;
    public AudioSource ctb;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("MuteSound") == 1)
        {
            //btnMute.image.sprite = muteSprites[1];
            GCSurvival survival = FindObjectOfType(typeof(GCSurvival)) as GCSurvival;
            survival.MuteSound(); ;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
        if(text.text == "3" || text.text == "2"|| text.text == "1")
        {
            if (!cta.isPlaying)
            {
                cta.PlayOneShot(cta.clip);
                //cta.Stop();
                //cta.Play();
            }
        }
        else if(text.text == "GO!")
        {
            if (!ctb.isPlaying)
            {
                ctb.Stop();
                ctb.Play();
            }
        }
	}
}
