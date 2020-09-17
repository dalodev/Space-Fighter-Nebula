using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOffImage : MonoBehaviour {

    public float speedFade = 0.1f;
    private float alpha = 1.0f;

    // Update is called once per frame
    void Update()
    {


        alpha -= speedFade * Time.deltaTime;
        GetComponent<Image>().color = new Color(255, 255, 255, alpha);

        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
