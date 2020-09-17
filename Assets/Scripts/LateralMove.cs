using UnityEngine;
using System.Collections;

public class LateralMove : MonoBehaviour {

    public float speed = 0.0f;

    private Rigidbody rb;
    private AudioSource audioSource;
    private int randomPos;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        randomPos = Random.Range(0, 2);
        Debug.Log("numero: " + randomPos);
        if(randomPos == 1)
        {
            rb.position = new Vector3(5.0f, 0, rb.position.z);
            rb.rotation = Quaternion.Euler(0, 90, 0);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else
        {
            rb.position = new Vector3(-5.0f, 0, rb.position.z);
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            rb.rotation = Quaternion.Euler(0, -90, 0);
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
