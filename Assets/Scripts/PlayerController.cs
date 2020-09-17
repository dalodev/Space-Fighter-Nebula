using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {

    public float speed = 0.0f;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public GameObject shotSpawn;
    public GameObject[] shotSpawns;
    public GameObject[] missleSpawns;
    public GameObject[] laserShots;
    public GameObject shield;
    public GameObject laserBeam;
    public float fireRate;
    public AudioSource powerUpSound;
    public SimpleTouchPad touchPad;

    private float nextFire;
    private PlayerHealth health;
    private Quaternion calibrationQuaternion;
    private Vector3 fingerPos;
    private GCSurvival gcSurvival;

    void Start()
    {
        //CalibrateAccelerometer();
        health = FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
        gcSurvival = FindObjectOfType(typeof(GCSurvival)) as GCSurvival;

    }
    //Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    void FixedUpdate()
    {

        //Con Input del teclado
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Con acelerometro(mobile)
        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration (accelerationRaw);
        //Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);

        //Con touch(mobile)
        //Vector2 direction = touchPad.GetDirecion();
        //Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        //GetComponent<Rigidbody>().velocity = movement * speed;
        if (gcSurvival.isStartGame)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                fingerPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));

            }
            //transform.position = new Vector3(fingerPos.x , fingerPos.y, fingerPos.z  +2f);
            GetComponent<Rigidbody>().position = new Vector3(fingerPos.x, fingerPos.y, fingerPos.z + 2f);

            GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        }
        else
        {
            return;
        }
        
        
        
        
    }


    void PowerUp()
    {
        powerUpSound.Play();
        int randomPowerUp = Random.Range(0, 4);
        Debug.Log(randomPowerUp);
        switch (randomPowerUp)
        {
            case 0:
                StartCoroutine(TripleLaser());
                Debug.Log("Triple Laser");
                break;
            case 1:
                StartCoroutine(Shield());
                Debug.Log("Shield");
                break;
            case 2:
                StartCoroutine(SeekingMissile());
                Debug.Log("Seeking Missile");
                break;
            case 3:
                StartCoroutine(DoubleLaser());
                Debug.Log("Double Laser");
                break;
            case 4:
                StartCoroutine(LaserBeam());
                Debug.Log("Laser Beam");
                break;

           
        }
        
    }

    IEnumerator TripleLaser()
    {
        foreach (GameObject shotSpawn in shotSpawns)
        {
            shotSpawn.SetActive(true);
        }
        yield return new WaitForSeconds(8.0f);
        foreach (GameObject shotSpawn in shotSpawns)
        {
            shotSpawn.SetActive(false);
        }

    }

    IEnumerator Shield()
    {
        shield.SetActive(true);
        health.Immunity = true;
        yield return new WaitForSeconds(8.0f);
        health.Immunity = false;
        shield.SetActive(false);

    }

    IEnumerator SeekingMissile()
    {
        foreach (GameObject missile in missleSpawns)
        {
            missile.SetActive(true);
        }
        yield return new WaitForSeconds(8.0f);
        foreach (GameObject missile in missleSpawns)
        {
            missile.SetActive(false);
        }
    }

    //double laser
    IEnumerator DoubleLaser()
    {
        
        foreach(GameObject laser in laserShots)
        {
            laser.SetActive(true);
        }
        shotSpawn.SetActive(false);

        yield return new WaitForSeconds(8.0f);
        foreach (GameObject laser in laserShots)
        {
            laser.SetActive(false);
        }
        shotSpawn.SetActive(true);

    }

    IEnumerator LaserBeam()
    {
        laserBeam.SetActive(true);
        yield return new WaitForSeconds(8.0f);
        laserBeam.SetActive(false);
    }


}
