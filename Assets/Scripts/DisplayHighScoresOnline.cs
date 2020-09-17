using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameSlyce;
using System.Collections.Generic;

public class DisplayHighScoresOnline : MonoBehaviour {

    public Text[] highScoreText;
    public GSItem gsItemPrefab;
    public GameObject GS_ListContainer;

    List<GSItem> gsList = new List<GSItem>();
    HighScoreDreamlo highscoreManager;

	// Use this for initialization
	void Start () {


        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". Fetching...";
        }
        highscoreManager = GetComponent<HighScoreDreamlo>();
        StartCoroutine("RefreshHighScores");
	}

    public void OnHighScoresDownloaded(Highscore[] highscoreList)
    {
        for(int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". ";
            if(highscoreList.Length > i)
            {
                highScoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }

    }
	
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighScores();
            //HighScoreDreamlo.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }


    
	// Update is called once per frame
	void Update () {
	
	}
}
