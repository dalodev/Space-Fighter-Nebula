using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighScoreDreamlo : MonoBehaviour {

    const string privateCode = "dWTI9fo4d0i6SFthao4NxQqk-PNPotI0Gwzg7Mkf1uag";
    const string publicCode = "56afc06b6e51b618080e91d3";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highScoresList;
    DisplayHighScoresOnline highscoresDisplay;
    static HighScoreDreamlo instance;


    void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighScoresOnline>();
        //AddNewHighScore("GM", 1000);
        //AddNewHighScore("Pepe", 100);
        //AddNewHighScore("Pepito", 200);
        DownloadHighScores();
    }


    public static void AddNewHighScore(string username, int score)
    {
        instance.StartCoroutine(instance.UpdloadNewHighScore(username, score));
    }

    public void DownloadHighScores()
    {
        instance.StartCoroutine("DownloadHighScoresFromDatabase");
    }

    IEnumerator UpdloadNewHighScore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Upload Succesful");
            DownloadHighScores();
        }
        else
        {
            Debug.Log("Error uploading" + www.error);
        }

    }
    IEnumerator  DownloadHighScoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode +"/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
            FormatHighScores(www.text);
            //highscoresDisplay.OnHighScoresDownloaded(highScoresList);
            
        }
        else
        {
            Debug.Log("Error downloading" + www.error);
        }
    }


    void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        highScoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highScoresList[i] = new Highscore(username, score);
            Debug.Log(highScoresList[i].username + ": " + highScoresList[i].score);
        }

    }
    

}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }

}
