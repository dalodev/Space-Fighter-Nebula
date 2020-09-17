using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {


    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").SendMessage("PowerUp");
            Destroy(gameObject);

        }
       

    }
    

}
