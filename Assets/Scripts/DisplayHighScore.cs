using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayHighScore : MonoBehaviour {

    //public int scoreSlot = 0;
    public string label;
    Text texto;
    // Use this for initialization
    void Start()
    {
        texto = GetComponent<Text>();
        //texto.text = label + PlayerPrefs.GetInt("HighScore" + scoreSlot.ToString());
        //texto.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        texto.text = label + PlayerPrefs.GetInt("HighScore").ToString();
    }
    void Update()
    {
        texto.text = label + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
