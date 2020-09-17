using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    void Fire()
    {
        foreach(Transform shotSpawn in shotSpawns)
        {
            Vector3 firePosition = new Vector3(shotSpawn.position.x, 0, shotSpawn.position.z);
            Instantiate(shot, firePosition, shotSpawn.rotation);
        }
        audioSource.Play();
    }

   
}
