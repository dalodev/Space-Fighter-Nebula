using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public GameObject[] bosses;
    public GameObject powerUp;//cambiar por un array de power ups

    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text gameOverText;
    public Text stageText;

    private int score;
    private bool gameOver;
    private int level = 1;
    private Vector3 lastEnemyPosition;
    private bool powerUpGenerated = false;

    void Start()
    {
        gameOver = false;
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(Level1());
    }
    void Dead(Vector3 position)
    {
        lastEnemyPosition = position;
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        
        
        

    }
    

    /*IEnumerator SpawnWaves()
    {
        DisplayLevels(level);
        yield return new WaitForSeconds(startWait);
        middleText.text = "";

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {

                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                middleText.text = middleText.text + "";
                break;
            }
        }   
    }*/

    IEnumerator Level1()
    {
        DisplayLevels("Stage", level);
        yield return new WaitForSeconds(startWait);
        stageText.text = "";
        int waves = 0;
        while (waves<2)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, 3)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                //middleText.text = middleText.text + "";
                break;
            }
            waves++;
            Instantiate(powerUp, lastEnemyPosition, powerUp.transform.rotation);
        }
        level++;
        StartCoroutine(Level2());
    }
    IEnumerator Level2()
    {
        DisplayLevels("Stage", level);
        yield return new WaitForSeconds(startWait);
        stageText.text = "";
        int waves = 0;
        while (waves < 3)
        {
            for (int i = 0; i < hazardCount; i++)
            {

                GameObject hazard = hazards[Random.Range(0, 4)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                //middleText.text = middleText.text + "";
                break;
            }
            waves++;
            Instantiate(powerUp, lastEnemyPosition, powerUp.transform.rotation);
        }
        level++;
        StartCoroutine(Boss1());
    }

    IEnumerator Boss1()
    {
        DisplayLevels("Boss", 0);
        yield return new WaitForSeconds(startWait);
        stageText.text = "";
        GameObject boss = bosses[0];
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
        InvokeRepeating("PowerUpGenerator", 1, 8);
        if (gameOver)
        {
            CancelInvoke("PowerUpGenerator");
            //middleText.text = middleText.text + "";
        }        
        //level++;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over \nPress Enter to Restart";
        gameOver = true;
        stageText.enabled = false;
    }

    public void DisplayLevels(string name, int level)
    {
        if(level == 0)
        {
            stageText.text = name ;

        }
        else
        {
            stageText.text = name + ": " + level;
        }

    }

    void PowerUpGenerator()
    {
        Instantiate(powerUp, spawnValues, powerUp.transform.rotation);
    }
}
