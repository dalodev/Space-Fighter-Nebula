using UnityEngine;
using System.Collections;

public class PlayerLaserBeam : MonoBehaviour {

    public GameObject deathEffect;
    public float turnSpeed;
    public float damagePerSecond;
    private LineRenderer lineRenderer;
    private GameController gameController;
    private GCSurvival survivalController;
    private GameObject nearestEnemy;
    float nearestDistance;
    float distance = 0.0f;
    private Rigidbody rb;
    //BoxCollider col;
    Vector3 _dir = Vector3.zero;
    //private float maxSqrVelocity;

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody>();

        //col = GetComponent<BoxCollider>();
        rb = GetComponentInParent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(.2f, .2f);
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        nearestDistance = Mathf.Infinity;
        
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            survivalController = gameControllerObject.GetComponent<GCSurvival>();
        }

        if (gameController == null && survivalController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        transform.position = transform.parent.position;

        GameObject obj = GameObject.FindGameObjectWithTag("Enemy");
        if (obj != null)
        {
            distance = (transform.position - obj.transform.position).magnitude;
            _dir = obj.transform.position - rb.position;

            lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), turnSpeed * Time.deltaTime);
            StartCoroutine(ShootLaserBeam());

            /*foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (obj != null)
                {
                    distance = (transform.position - obj.transform.position).magnitude;
                    _dir = obj.transform.position - rb.position;

                    lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), turnSpeed * Time.deltaTime);
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            EnemyHealth enemyHealt = hit.collider.gameObject.GetComponent<EnemyHealth>();
                            //Destroy(hit.collider.gameObject);
                            if (enemyHealt != null)
                            {
                                Debug.Log(enemyHealt.name);
                                StartCoroutine(DestroyDelay(hit.collider.gameObject));
                                enemyHealt.TakeDamage(damagePerSecond * Time.deltaTime, obj);
                            }
                        }
                    }
                }*/
        }


    }

    

    IEnumerator ShootLaserBeam()
    {
        
        RaycastHit hit;
        //lineRenderer.SetPosition(1, new Vector3(0, 0, Camera.main.orthographicSize));

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {

            /*distance = (transform.position - obj.transform.position).magnitude;
            _dir = obj.transform.position - rb.position;
            col.center = new Vector3(0, 0, distance / 2);
            col.size = new Vector3(0.1f, 0, distance);*/
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    EnemyHealth enemyHealt = hit.collider.gameObject.GetComponent<EnemyHealth>();
                    //Destroy(hit.collider.gameObject);
                    if (enemyHealt != null)
                    {
                        yield return new WaitForSeconds(0.8f);
                        Instantiate(deathEffect, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
                        Destroy(hit.collider.gameObject);
                        //enemyHealt.TakeDamage(damagePerSecond * Time.deltaTime, obj);
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);

        }
        lineRenderer.SetPosition(1, new Vector3(0, 0, distance));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), turnSpeed * Time.deltaTime);
    }
    

    
}
