using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float initialHealth = 10.0f;
    public GameObject deathEffect;
    public GameObject shield;

    private float currentHealth;
    private GameController gameController;
    private GCSurvival survivalController;
    private bool immunity = true;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public bool Immunity
    {
        get
        {
            return immunity;
        }

        set
        {
            immunity = value;
        }
    }


    // Use this for initialization
    void Start () {
        CurrentHealth = initialHealth;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            survivalController = gameControllerObject.GetComponent<GCSurvival>();
            if (PlayerPrefs.GetInt("MuteSound") == 1)
            {
                //btnMute.image.sprite = muteSprites[1];
                survivalController.MuteSound(); ;
            }

        }
       
        if (gameController == null && survivalController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("PlayerBolt") || other.CompareTag("PowerUp") || other.CompareTag("LaserBeam"))
        {
            return;
        }
       

        if (!Immunity)
        {
            CurrentHealth--;
            if (CurrentHealth <= 0)
            {
                Instantiate(deathEffect, transform.position, other.transform.rotation);
                Destroy(gameObject);
            }
        }
        Destroy(other.gameObject);

    }


}
