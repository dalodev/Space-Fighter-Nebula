using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    public AudioSource audio;


    public static Music Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                
                GameObject gameManager = new GameObject("MenuManager");
                _instance = gameManager.AddComponent<Music>();
                return _instance;
            }
        }
    }

    
    private static Music _instance;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

    }
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("MuteSound") == 1)
        {
            //btnMute.image.sprite = muteSprites[1];
            MuteSound(); ;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MuteSound()
    {
        foreach (AudioSource a in FindObjectsOfType(typeof(AudioSource)))
        {
            //a.Pause();
            a.enabled = false;
        }

        /*foreach (AudioSource a in backgroundMusic)
        {
            a.Pause();
        }*/
    }
}
