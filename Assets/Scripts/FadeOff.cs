using UnityEngine;
using System.Collections;

public class FadeOff : MonoBehaviour {

    public float velocidadFade = 0.1f;
    private float alpha = 1.0f;
	
	// Update is called once per frame
	void Update () {


        alpha -= velocidadFade * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = new Color(255,255,255, alpha);
        
        if(alpha <= 0)
        {
            Destroy(gameObject);
        }
	}
}
