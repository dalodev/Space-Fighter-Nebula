using UnityEngine;
using System.Collections;

public class WeaponLookPlayer : MonoBehaviour {

    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;
    public float delay;
    public float turnSpeed;


    private AudioSource audioSource;
    private Transform player;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }
	
	// Update is called once per frame
	void Update () {
        if (player)
        {
            foreach (Transform shotSpawn in shotSpawns)
            {
                Vector3 _dir = player.position - rb.position;
                _dir.Normalize();
                shotSpawn.rotation = Quaternion.Slerp(shotSpawn.rotation, Quaternion.LookRotation(_dir), turnSpeed * Time.deltaTime);
            }
        }
       

    }

    void Fire()
    {
        foreach (Transform shotSpawn in shotSpawns)
        {
            Vector3 firePosition = new Vector3(shotSpawn.position.x, 0, shotSpawn.position.z);
            Instantiate(shot, firePosition, shotSpawn.rotation);
        }
        audioSource.Play();
    }
}
