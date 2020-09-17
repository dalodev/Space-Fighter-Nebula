using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gameController;
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("EnemyBolt") || other.CompareTag("PowerUp"))
        {
            return;
        }
        if (other.CompareTag("PlayerBolt") && gameObject.CompareTag("EnemyBolt"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            gameController.GameOver();
        }

        gameController.SendMessage("Dead", transform.position, SendMessageOptions.DontRequireReceiver);

        gameController.AddScore(scoreValue);
        //Destroy(other.gameObject);
        Destroy(gameObject);
        

       
    }
}