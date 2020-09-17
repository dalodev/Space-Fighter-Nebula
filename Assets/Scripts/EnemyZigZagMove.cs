using UnityEngine;
using System.Collections;

public class EnemyZigZagMove : MonoBehaviour {

    public float speed = 1.0f;
    public float sinAmplitude = 1.0f;
    public float sinFrequency = 1.0f;

    private Rigidbody rb;
    private float horizontalOffset = 0.0f;
    private float time;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        //remove offset from enemy
        transform.position -= horizontalOffset * transform.right;

        //Moves enemy down
        transform.position += -transform.forward * speed * Time.deltaTime;

        //Adjust horizontal position by sine wave
        horizontalOffset = Mathf.Sin(time * sinFrequency * 2 * Mathf.PI) * sinAmplitude;

        transform.position += horizontalOffset * transform.right;
    }
}
