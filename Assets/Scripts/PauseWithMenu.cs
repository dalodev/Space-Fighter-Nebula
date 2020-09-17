using UnityEngine;
using System.Collections;

public class PauseWithMenu : MonoBehaviour {

    public string menuSceneName = "Menu";
    public Font menuFont;

    public GUITexture darkenScreen;
    public GUIText pausedText;
    public GUIText menuText;
    public GUIText resumeText;
    public GameObject resumeGraphics;
    public GameObject menuGraphics;

    private bool isPaused = false;


    // Use this for initialization
    void Start () {
        if (menuFont != null)
        {
            pausedText.font = menuFont;
            menuText.font = menuFont;
            resumeText.font = menuFont;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetKeyDown("escape"))
        {
            doPause();
        }

        if (isPaused == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mousePos.y > 0.53 && mousePos.y < 0.8)
                {
                    doPause();
                }
                if (mousePos.y < 0.53 && mousePos.y > 0.2)
                {
                    Application.LoadLevel(menuSceneName);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos.x > 0.8 && mousePos.y > 0.8)
            {
                doPause();
            }
        }
    }

    void doPause()
    {

        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;
            //darkenScreen.guiTexture.enabled = true;
            darkenScreen.GetComponent<GUITexture>().enabled = true;
            //pausedText.guiText.enabled = true;
            pausedText.GetComponent<GUIText>().enabled = true;
            resumeGraphics.SetActive(true);
            menuGraphics.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            //darkenScreen.guiTexture.enabled = false;
            darkenScreen.GetComponent<GUITexture>().enabled = false;
            //pausedText.guiText.enabled = false;
            pausedText.GetComponent<GUIText>().enabled = false;
            resumeGraphics.SetActive(false);
            menuGraphics.SetActive(false);
        }

    }
}
