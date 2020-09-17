using UnityEngine;
using System.Collections;

public class HorizontalMove : MonoBehaviour {

    public float speed = 0.0f;
    public float tilt;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
