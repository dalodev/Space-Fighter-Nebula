using UnityEngine;
using System.Collections;

public class SeekingMissile : MonoBehaviour {

    public float initialVelocity = 10.0f;
    public float missileAccelerationForce = 1.0f;
    public float maxVelocity = 10.0f;

    private float maxSqrVelocity;
    private GameObject nearestEnemy;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * initialVelocity, ForceMode.VelocityChange);

        float nearestDistance = Mathf.Infinity;
        nearestEnemy = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = (transform.position - obj.transform.position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = obj;
            }
        }
        maxSqrVelocity = maxVelocity * maxVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        missileAccelerationForce += 100.0f;
        if (nearestEnemy != null)
        {
            transform.rotation = Quaternion.LookRotation(nearestEnemy.transform.position - transform.position, Vector3.up);
        }

        rb.AddForce(transform.forward * missileAccelerationForce * Time.deltaTime, ForceMode.Acceleration);

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

    }
}
