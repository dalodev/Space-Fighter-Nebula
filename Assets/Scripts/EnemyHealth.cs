using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float initialHealth = 10.0f;
    public GameObject hitEffect;
    public GameObject deathEffect;
    public GameObject playerExplosion;
    public int scoreValue;

    private float currentHealth;
    private GameController gameController;
    private GCSurvival survivalController;

    private bool immunity = false;

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
    void Start()
    {
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

    public void TakeDamage(float damage, GameObject other)
    {
        if (other.CompareTag("Shield"))
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            return;
        }
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("EnemyBolt") || other.CompareTag("PowerUp") )
        {
            return;
        }

        if ((other.CompareTag("PlayerBolt") || other.CompareTag("LaserBeam")) && gameObject.CompareTag("EnemyBolt"))
        {
            return;
        }
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            if (!other.gameObject.CompareTag("LaserBeam"))
            {
                Destroy(other.gameObject);

            }
        }

        

        //Instantiate(hitEffect, other.transform.position, other.transform.rotation);

        if (!Immunity)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Instantiate(deathEffect, transform.position, other.transform.rotation);
                if (gameController != null)
                {
                    gameController.SendMessage("Dead", transform.position, SendMessageOptions.DontRequireReceiver);
                    gameController.AddScore(scoreValue);
                }
                if (survivalController != null)
                {
                    survivalController.SendMessage("Dead", transform.position, SendMessageOptions.DontRequireReceiver);
                    survivalController.AddScore(scoreValue);
                }

                Destroy(gameObject);
            }
        }
        if (gameController != null)
        {
            gameController.SendMessage("Dead", transform.position, SendMessageOptions.DontRequireReceiver);
        }
        if (survivalController != null)
        {
            survivalController.SendMessage("Dead", transform.position, SendMessageOptions.DontRequireReceiver);
        }
        if (!other.gameObject.CompareTag("LaserBeam"))
        {
            Destroy(other.gameObject);

        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);

            if (gameController != null)
            {
                //gameController.StartCoroutine(gameController.GameOver());

                gameController.GameOver();
            }
            if (survivalController != null)
            {
                //survivalController.StartCoroutine(survivalController.GameOver());
                survivalController.GameOver();
            }
        }
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LaserBeam") && initialHealth > 1)
        {
            yield return new WaitForSeconds(0.8f);
            TakeDamage(5.0f, other.gameObject);
        }
        else if(other.gameObject.CompareTag("LaserBeam") && initialHealth == 1)
        {
            yield return new WaitForSeconds(0.8f);
            TakeDamage(1.0f, other.gameObject);
        }
        else
        {
            TakeDamage(1.0f, other.gameObject);
        }

    }

   

}
