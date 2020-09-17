using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    
    public AudioSource btnAudio;
    public Button[] buttons;
    public Button continueButton;
    public float velocidadFade = 0.1f;
    public string scene;
    public FadeOffImage[] fadeOffImages;
    private float alpha = 255f;
    AsyncOperation async;
    private Music music;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        
    }

    void Start()
    {
        Time.timeScale = 1;
        music = GameObject.FindObjectOfType(typeof(Music)) as Music;
        
        //Se ha pausado = 1, no = 0
        //PlayerPrefs.SetInt("GamePaused", 0);
        if (continueButton != null)
        {
            if (PlayerPrefs.GetInt("GamePaused") == 1)
            {
                continueButton.interactable = true;
            }
            else
            {
                continueButton.interactable = false;
            }
        }
        if(PlayerPrefs.GetInt("MuteSound") == 1)
        {
            MuteSound();
        }
        if (!music.audio.isPlaying)
        {
            music.audio.Play();
        }
        /*quitButton.onClick.AddListener(() => {
            Application.Quit();
        });*/
    }

    void Update()
    {
        

        if (scene == "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(scene);
            }
        }
        

        

    }
    private void MuteSound()
    {
        foreach (AudioSource a in FindObjectsOfType(typeof(AudioSource)))
        {
            //a.Pause();
            a.enabled = false;
        }
    }

    public void StartGame()
    {
        btnAudio.Play();
        foreach (Button btn in buttons)
        {
            btn.enabled = false;
        }
        PlayerPrefs.SetInt("GamePaused", 0);
        //Application.LoadLevel(1);
        //SceneManager.LoadScene(1);
        //StartCoroutine(FadeOff());
        StartCoroutine("LoadScene");
        SwitchScene();
    }

    public void ContunueGame()
    {
        btnAudio.Play();
        foreach (Button btn in buttons)
        {
            btn.enabled = false;
        }
        //Application.LoadLevel(1);
        //SceneManager.LoadScene(1);
        //StartCoroutine(FadeOff());
        StartCoroutine("LoadScene");
        SwitchScene();
    }


    public void QuitGame()
    {
        btnAudio.Play();
        Application.Quit();
    }

    public void navigateToPrivacyPolicy()
    {
        Application.OpenURL("https://davidalonsosantos1.wixsite.com/spacefighternebula");
    }

    IEnumerator FadeOff(int scene)
    {
        /*foreach (MonoBehaviour mono in gameObject.GetComponentsInChildren<MonoBehaviour>())
        {
            mono.enabled = true;
            //yield return null;
        }*/
        foreach(FadeOffImage f in fadeOffImages)
        {
            f.enabled = true;
        }
        
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(scene);
    }
    IEnumerator LoadScene()
    {

        async = SceneManager.LoadSceneAsync(1);
        //async = Application.LoadLevelAsync(1);
        async.allowSceneActivation = false;

        Debug.Log("start loading");
        foreach (MonoBehaviour mono in gameObject.GetComponentsInChildren<MonoBehaviour>())
        {
            mono.enabled = true;
        }
        yield return async;
    }

    
    private void SwitchScene()
    {
        Debug.Log("switching");

        if (async != null)
        {
            Debug.Log("screenactivation");
            async.allowSceneActivation = true;
        }
    }
    private bool fading = false;


    public void HighScores()
    {
        btnAudio.Play();
        StartCoroutine(FadeOff(2));
        //SceneManager.LoadScene(2);
    }

    

}
