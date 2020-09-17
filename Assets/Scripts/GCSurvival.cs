using UnityEngine;
using System.Collections;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;

public class GCSurvival : MonoBehaviour
{
    public GameObject[] hazardsLv1;
    public GameObject[] hazardsLv2;
    public GameObject[] hazardsLv3;
    public GameObject[] hazardsLv4;
    public GameObject[] hazardsLv5;

    public GameObject boss;

    public Vector3 spawnValues;
    public GameObject powerUp;//cambiar por un array de power ups
    //public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text gameOverText;
    public Animator textAnimator;
    public AudioSource[] backgroundMusic;
    public AudioSource btnAudio;
    public GameObject highScoreController;
    private bool gamePaused;
    public Button btnPaused;
    public GameObject startPanel;
    public bool isStartGame;
    public GameObject usernameInput;
    public Text usernameText;
    public Button btnMute;
    public Sprite[] muteSprites;

    private int hazardCount;
    private int score;
    private bool gameOver;
    private bool startGame = false;
    private Vector3 lastEnemyPosition;
    private bool powerUpGenerated = false;
    GameObject hazard;
    Vector3 spawnPosition;
    private UnityAds unityAds;
    private bool isScreenDisable;
    private int scorePaused;
    HighScoreDreamlo highscoreManager;
    private string username;
    private Music music;
    private bool isOnlineHighScore;
    private AudioSource actualMusic;
    private bool isEnemyShip_a_bSpawn;

    public bool StartGame
    {
        get
        {
            return startGame;
        }

        set
        {
            startGame = value;
        }
    }

    public bool GamePaused
    {
        get
        {
            return gamePaused;
        }

        set
        {
            gamePaused = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public bool GameOver1
    {
        get
        {
            return gameOver;
        }

        set
        {
            gameOver = value;
        }
    }

    public bool IsScreenDisable
    {
        get
        {
            return isScreenDisable;
        }

        set
        {
            isScreenDisable = value;
        }
    }

    void Awake()
    {
        Time.timeScale = 1;
        actualMusic = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        actualMusic.Play();
        music = GameObject.FindObjectOfType(typeof(Music)) as Music;

    }

    // Use this for initialization
    void Start()
    {
        //gameOver = false;
        
        hazardCount = 10;
        Score = 0;
        isStartGame = false;
        IsScreenDisable = true;
        gameOverText.gameObject.gameObject.SetActive(false);
        highscoreManager = GameObject.FindObjectOfType(typeof(HighScoreDreamlo)) as HighScoreDreamlo;
        

        if (PlayerPrefs.GetInt("MuteSound") == 1)
        {
            btnMute.image.sprite = muteSprites[1];
            MuteSound();
        }

        if (PlayerPrefs.GetInt("GamePaused") == 1)
        {
            Score = PlayerPrefs.GetInt("ScorePaused");
            scoreText.text = "Score: "+ PlayerPrefs.GetInt("ScorePaused");
        }
        else
        {
            UpdateScore();
        }

        if (!StartGame)
        {
            gameOverText.text = "";

        }
        else
        {
            StartCoroutine(SpawnWaves());

        }
        if (music.audio.isPlaying)
        {
            music.audio.Stop();
        }

    }

    void Update()
    {

        if (!StartGame)
        {
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            if(isStartGame)
            {
                btnPaused.interactable = true;
                StartGame = true;
                Debug.Log("Start game");
                startPanel.SetActive(false);
                gameOverText.gameObject.gameObject.SetActive(true);
                //backgroundMusic[0].Stop();
                StartCoroutine(SpawnWaves());
            }
            else
            {
                btnPaused.interactable = false;
            }
        }

        if (IsScreenDisable)
        {
            return;

        }else
        {
            if (GameOver1)
            {
                btnPaused.interactable = false;
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log("HighScore false");
                    Debug.Log("Pulsado pantalla...");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                
            }
            else
            {
                btnPaused.interactable = true;
            }
        }

        
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene(0);
        }*/

    }

    void Dead(Vector3 position)
    {
        lastEnemyPosition = position;
    }

    IEnumerator SpawnWaves()
    {
        isEnemyShip_a_bSpawn = false;
        //Score = 5000; // for tests, delete or coment when finish the tests :)
        textAnimator.SetBool("textAnimation", true);
        gameOverText.text = "" + 3;
        //yield return new WaitForSeconds(startWait);
        yield return new WaitForSeconds(1.0f);
        gameOverText.text = "" + 2;
        yield return new WaitForSeconds(1.0f);
        gameOverText.text = "" + 1;
        yield return new WaitForSeconds(1.0f);
        gameOverText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        textAnimator.SetBool("textAnimation", false);

        gameOverText.text = "";

        if (StartGame)
        {
            while (true)
            {

                for (int i = 0; i < hazardCount; i++)
                {
                    //cambiar oleadas según tiempo???? o por oleadas
                    if (Score <= 400)
                    {
                        hazard = hazardsLv1[Random.Range(0, hazardsLv1.Length)];
                    }
                    else if (Score <= 900)
                    {
                        hazard = hazardsLv2[Random.Range(0, hazardsLv2.Length)];

                    }
                    else if (Score <=1800)
                    {
                        hazard = hazardsLv3[Random.Range(0, hazardsLv3.Length)];

                    }
                    else if (Score <= 3800)
                    {
                        hazard = hazardsLv4[Random.Range(0, hazardsLv4.Length)];
                    }
                    else if(Score <= 4500)
                    {
                        hazardCount = 12;
                        hazard = hazardsLv4[Random.Range(0, hazardsLv4.Length)];
                    }
                    else
                    {
                        if (isEnemyShip_a_bSpawn)
                        {
                            hazard = hazardsLv5[Random.Range(0, hazardsLv5.Length-1)];
                        }
                        else
                        {
                            hazard = hazardsLv5[Random.Range(0, hazardsLv5.Length)];
                        }
                        //spawn new Enemy "Enemy Ship a_b" togheter and wait x seconds to spawn another
                        if (hazard.name == "Enemy Ship 3 a_b")
                        {
                            isEnemyShip_a_bSpawn = true;
                        }
                    }

                    //GameObject hazard = hazardsLv1[Random.Range(0, hazardsLv1.Length)];
                    if (hazard.name == "Soul of Phillon Defender" || hazard.name == "Enemy Ship 3 a_b")
                    {
                        spawnPosition = new Vector3(0, 0, 16);
                    }else{
                        spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    }
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
               
                yield return new WaitForSeconds(waveWait);
                isEnemyShip_a_bSpawn = false;

                if (!GameOver1)
                {
                    Instantiate(powerUp, lastEnemyPosition, powerUp.transform.rotation);

                }

                if (GameOver1)
                {
                    gameOverText.text = gameOverText.text + "";
                    break;
                }
                //instanciar boss de alguna manera y condición???
            }
            

        }

    }



    public void AddScore(int newScoreValue)
    {
        Score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scorePaused = Score;
        if(Score > 0)
        {
            PlayerPrefs.SetInt("ScorePaused", scorePaused);
        }

        scoreText.text = "Score: " + Score;
    }

    public void GameOver()
    {
        foreach(GameObject powerUp in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(powerUp);
        }
        int randomAd = Random.Range(0, 3);
        if(randomAd == 1)
        {
            StartCoroutine(ShowAd());
        }
        bool var = SaveHighScore();
        if (!var)
        {
            gameOverText.text = "Game Over \nPress Sceen to Restart";
        }
        GameOver1 = true;
        GamePaused = false;
        PlayerPrefs.SetInt("GamePaused", 0);
        PlayerPrefs.SetInt("ScorePaused", 0);
        //stageText.enabled = false;
    }



    void PowerUpGenerator()
    {
        Instantiate(powerUp, spawnValues, powerUp.transform.rotation);
    }


    bool SaveHighScore()
    {
        bool isPersonalScore = false;
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            isOnlineHighScore = false;
            isPersonalScore = true;
            IsScreenDisable = true;
            btnPaused.interactable = false;
            highScoreController.SetActive(true);
            highScoreController.GetComponentInChildren<Text>().text = "New Personal HighScore!";
            PlayerPrefs.SetInt("HighScore", Score);
        }
        else
        {
            IsScreenDisable = false;
        }
        if(highscoreManager.highScoresList.Length > 0)
        {
            for (int i = 0; i < highscoreManager.highScoresList.Length; i++)
            {
                if (Score > highscoreManager.highScoresList[0].score)
                {
                    btnPaused.interactable = false;
                    IsScreenDisable = true;
                    isOnlineHighScore = true;
                    highScoreController.SetActive(true);
                    highScoreController.GetComponentInChildren<Text>().text = "New Online HighScore!";
                    if(Score > PlayerPrefs.GetInt("HighScore"))
                    {
                        PlayerPrefs.SetInt("HighScore", Score);
                    }
                }
                else
                {

                    if (isPersonalScore)
                    {
                        IsScreenDisable = true;
                    }
                    else
                    {
                        IsScreenDisable = false;
                    }
                }
            }

        }
        else
        {
            btnPaused.interactable = false;
            IsScreenDisable = true;
            isOnlineHighScore = true;
            highScoreController.SetActive(true);
            //PlayerPrefs.SetInt("HighScore", Score);
        }

        return IsScreenDisable;
        
    }
    public IEnumerator ShowAd()
    {
        yield return new WaitForSeconds(1.5f);
        #if UNITY_ADS

        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        #endif
    }

    public void OkButton()
    {
        btnAudio.Play();
        highScoreController.SetActive(false);
        if (isOnlineHighScore)
        {
            usernameInput.SetActive(true);
        }
        else{
            gameOverText.text = "Game Over \nPress Sceen to Restart";
            IsScreenDisable = false;
        }

        //gameOverText.text = "Game Over \nPress Sceen to Restart";
        //isScreenDisable = false;
    }

    public void SubmitButton()
    {
        btnAudio.Play();
        username = usernameText.text;

        if (CheckConnection("http://www.google.com"))
        {
            if (string.IsNullOrEmpty(username))
            {
                Debug.Log("Please enter username");
                usernameInput.GetComponentInChildren<Text>().text = "Please enter username";
            }
            else
            {
                //username.ToLower();
                HighScoreDreamlo.AddNewHighScore(username, Score);
                usernameInput.SetActive(false);
                gameOverText.text = "Game Over \nPress Screen to Restart";
                IsScreenDisable = false;
            }
        }
        else
        {
            usernameInput.GetComponentInChildren<Text>().text = "No internet connection";
            usernameInput.SetActive(false);
            gameOverText.text = "Game Over \nPress Screen to Restart";
            IsScreenDisable = false;
        }


    }
    public void QuitButton()
    {
        //Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void StartButton()
    {
        btnAudio.Play();
        isStartGame = true;
    }

    public void MuteButton()
    {
        int mute = PlayerPrefs.GetInt("MuteSound");

        /*if (btnMute.image.sprite == muteSprites[0])
        {
            mute = 1;
            btnMute.image.sprite = muteSprites[1];
            MuteSound();
        }
        else
        {
            mute = 0;
            btnMute.image.sprite = muteSprites[0];
            SoundOn();
        }*/
        if (mute == 0)
        {
            mute = 1;
            btnMute.image.sprite = muteSprites[1];
            MuteSound();
        }
        else
        {
            mute = 0;
            btnMute.image.sprite = muteSprites[0];
            SoundOn();
        }

        PlayerPrefs.SetInt("MuteSound", mute);
    }

    public void MuteSound()
    {
        foreach (AudioSource a in FindObjectsOfType(typeof(AudioSource)))
        {
            //a.Pause();
            a.enabled = false;
        }
        actualMusic.Pause();
       
    }

    //cuando hago mute, unmute se activan todas las canciones
    public void SoundOn()
    {
        foreach(AudioSource a in FindObjectsOfType(typeof(AudioSource)))
        {
            //a.Play();
            a.enabled = true;
        }
        if (music.audio.isPlaying)
        {
            music.audio.Stop();
        }

        actualMusic.Play();
    }

    private bool CheckConnection(System.String URL)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK) return true;
            else return false;
        }
        catch
        {
            return false;
        }
    }

}
