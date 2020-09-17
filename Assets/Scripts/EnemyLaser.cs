using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

    public GameObject deathEffect;
    private LineRenderer lineRenderer;
    private GameController gameController;
    private GCSurvival survivalController;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(.2f, .2f);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            survivalController = gameControllerObject.GetComponent<GCSurvival>();
        }

        if (gameController == null && survivalController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        lineRenderer.SetPosition(0, new Vector3(0, 0, -11));

        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                if (gameController != null)
                {
                    gameController.GameOver();
                }
                if (survivalController != null)
                {
                    survivalController.GameOver();
                }
                Destroy(hit.collider.gameObject);
                Instantiate(deathEffect, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
            }
        }
	}
}
