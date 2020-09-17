using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Boundary boundary;
    public Animator leftShotAnim;
    public Animator rightShotAnim;
    public Animator shotPatterns;
    public GameObject bullet;
    public Transform[] mainShotSpawn;
    public Transform[] shotSpawns;
    public GameObject shield;
    public float shootDelay = 0.2f;
    public Text healtBoss;
    public float immunityTime;

    private float targetManeuver;
    private Rigidbody rb;
    private float currentSpeed;
    private int phase = 0;
    private EnemyHealth health;
    private bool readyToShoot = true;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        health = FindObjectOfType(typeof(EnemyHealth)) as EnemyHealth;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(BossIA());
    }

    IEnumerator BossIA()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (phase == 0)
        {

            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
            Invoke("ResetReadyToShoot", shootDelay);
        }
       
        shield.SetActive(true);
        health.Immunity = true;
        shootDelay = 0.1f;
        shotPatterns.SetBool("rotate360", true);
        //inmune durante x segundos
        yield return new WaitForSeconds(immunityTime);
        shield.SetActive(false);
        health.Immunity = false;
        
        

    }

    


    // Update is called once per frame
    void FixedUpdate()
    {
        if (readyToShoot)
        {
            if(phase == 0)
            {
                foreach (Transform shotSpawn in mainShotSpawn)
                {
                    Vector3 firePosition = new Vector3(shotSpawn.position.x, 0, shotSpawn.position.z);
                    Instantiate(bullet, firePosition, shotSpawn.rotation);
                }
            }
           
            readyToShoot = false;
            if(phase != 0)
            {
                
                foreach (Transform shotSpawn in shotSpawns)
                {
                    Vector3 firePosition = new Vector3(shotSpawn.position.x, 0, shotSpawn.position.z);
                    Instantiate(bullet, firePosition, shotSpawn.rotation);
                }
                Invoke("ResetReadyToShoot", shootDelay);
            }
            audioSource.Play();
        }
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)

        );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        

        //a mitad de vida se coloca en el centro
        if (health.CurrentHealth <= 100)
        { 
            phase = 1;
            rb.position = new Vector3(
               Mathf.Lerp(rb.position.x, 0, Time.deltaTime),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
               );
        }
    }

    IEnumerator WaitForPositioning()
    {
        yield return new WaitForSeconds(1.0f);
    }
    void ResetReadyToShoot()
    {
        readyToShoot = true;
    }


}
