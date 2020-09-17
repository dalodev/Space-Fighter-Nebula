using UnityEngine;
using System.Collections;

public class BoltMoveAndRotate : MonoBehaviour {

    public float speed = 0.0f;
    public float turnSpeed = 0.0f;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
}
