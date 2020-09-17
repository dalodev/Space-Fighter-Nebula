using UnityEngine;
using System.Collections;

public class RotateBackAndForth : MonoBehaviour {

    public float rotation = 90.0f;
    public float rotationTime = 1.0f;
    private Vector3 firePosition;
    // Use this for initialization
    void Start()
    {

        StartCoroutine("RotateForth");
    }

    IEnumerator RotateForth()
    {
        float t = 0.0f;
        while (t < rotationTime * 0.5f)
        {
            firePosition = new Vector3(transform.position.x, 0, transform.position.z);

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
            firePosition = new Vector3(transform.position.x, 0, transform.position.z);

            transform.RotateAround(transform.position, transform.up, -Time.deltaTime / (rotationTime * 0.5f) * rotation);
            t += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("RotateForth");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
