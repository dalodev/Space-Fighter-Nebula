using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

	public GameObject pausable;
	public Canvas pauseCanvas;
    public AudioSource[] audios;
    public AudioSource btnAudio;
	private bool isPaused = false;
	private Animator anim;
	private Component[] pausableInterfaces;
	private Component[] quittableInterfaces;
    private GCSurvival gcSurvival;

	void Start() 
	{
		// PauseManager requires the EventSystem - make sure there is one
		if (FindObjectOfType<EventSystem>() == null)
		{
			var es = new GameObject("EventSystem", typeof(EventSystem));
			es.AddComponent<StandaloneInputModule>();
		}

		pausableInterfaces = pausable.GetComponents (typeof(IPausable));
		quittableInterfaces = pausable.GetComponents (typeof(IQuittable));
		anim = pauseCanvas.GetComponent<Animator> ();
        gcSurvival = GameObject.FindObjectOfType(typeof(GCSurvival)) as GCSurvival;
       
		pauseCanvas.enabled = false;
	}
	
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape) && !gcSurvival.isStartGame)
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gcSurvival.GameOver1)
        {
            pauseCanvas.enabled = false;
        }
        if ((Input.GetKeyDown(KeyCode.Escape) && !isPaused) && gcSurvival.isStartGame && !gcSurvival.GameOver1)
		{
			if( isPaused ) {
				OnUnPause();
			} else {
				OnPause();
            }
        }

		pauseCanvas.enabled = isPaused;
		//anim.SetBool( "IsPaused", isPaused );
	}
		
	public void OnQuit() {
		Debug.Log ("PauseManager.OnQuit");
       
        if (gcSurvival.GamePaused && gcSurvival.Score != 0)
        {
            PlayerPrefs.SetInt("GamePaused", 1);

        }
        foreach (var quittableComponent in quittableInterfaces) {		
			IQuittable quittableInterface = (IQuittable)quittableComponent;
			if( quittableInterface != null)
            {
                quittableInterface.OnQuit ();
                btnAudio.Play();
                //SceneManager.LoadScene(0);
            }
        }		
	}
	
	public void OnUnPause() {
		Debug.Log ("PauseManager.OnUnPause");
        isPaused = false;
        PlayerPrefs.SetInt("GamePaused", 0);

        foreach (AudioSource audio in audios)
        {
            audio.enabled = true;
        }
        foreach (var pausableComponent in pausableInterfaces) {		
			IPausable pausableInterface = (IPausable)pausableComponent;
			if( pausableInterface != null )
				pausableInterface.OnUnPause ();
                btnAudio.Play();

        }
    }

	public void OnPause() {
		Debug.Log ("PauseManager.OnPause");
		isPaused = true;
        gcSurvival.GamePaused = true;

        if(gcSurvival.Score != 0)
        {
            PlayerPrefs.SetInt("GamePaused", 1);
        }

        foreach (AudioSource audio in audios)
        {
            audio.enabled = false;
        }
		foreach (var pausableComponent in pausableInterfaces) {		
			IPausable pausableInterface = (IPausable)pausableComponent;
			if( pausableInterface != null )
				pausableInterface.OnPause ();
                btnAudio.Play();

        }
    }
}
