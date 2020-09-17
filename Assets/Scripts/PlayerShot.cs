using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

    public GameObject bullet;
    public float shootDelay = 0.2f;

    private GCSurvival gcSurvival;
    private bool readyToShoot = true;

    void Start()
    {
        gcSurvival = FindObjectOfType(typeof(GCSurvival)) as GCSurvival;
    }

    // Update is called once per frame
    void Update()
    {
        if (gcSurvival.isStartGame)
        {
            if (Input.GetButton("Fire1") && readyToShoot)
            {
                Vector3 firePosition = new Vector3(transform.position.x, 0, transform.position.z);

                Instantiate(bullet, firePosition, transform.localRotation);
                readyToShoot = false;
                Invoke("ResetReadyToShoot", shootDelay);
                transform.parent.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            return;
        }
        
        
    }

    void ResetReadyToShoot()
    {
        readyToShoot = true;
    }
}
