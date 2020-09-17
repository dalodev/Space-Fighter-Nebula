using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AppQuit : MonoBehaviour, IQuittable {
	public void OnQuit() {
		Debug.Log ("AppQuit.Quit");
        //Application.Quit ();
        SceneManager.LoadScene(0);
	}
}
