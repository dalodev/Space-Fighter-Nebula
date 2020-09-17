using UnityEngine;
using System.Collections;

public class Rotate360 : MonoBehaviour {

    public float rotation;
    public float rotationTime = 1.0f;
    public float pos;
    public float angle;
    Rigidbody rb;

    
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(pos, 0, rb.position.z);
        StartCoroutine("RotateForth");
    }

    IEnumerator RotateForth()
    {
        float t = 0.0f;
        while (t < rotationTime * 0.5f)
        {
            transform.RotateAround(transform.position, transform.up, Time.deltaTime / (rotationTime * 0.5f) * rotation);
            t += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("RotateBack");
    }
    IEnumerator RotateBack()
    {
        float t = 0.0f;
        while (t < rotationTime * 0.5f)
        {

            transform.RotateAround(transform.position, transform.up, -Time.deltaTime / (rotationTime * 0.5f) * rotation);
            t += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("RotateForth");
    }
    // Update is called once per frame
    void Update () {
        rb.position = new Vector3(pos, 0, rb.position.z);
        rb.rotation = Quaternion.Euler(0, angle, 0);

    }
}
