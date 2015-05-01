using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	GameObject fadeOutObj;
	Image fadeOutImg;
	public AudioSource[] dialogueSource;

	float musicLerpTimer = 0;
	bool starting = false;

	void Start(){
		fadeOutObj = GameObject.Find ("Image");
		fadeOutImg = fadeOutObj.GetComponent<Image>();
		fadeOutObj.SetActive(false);
		dialogueSource = GetComponentsInChildren<AudioSource> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (starting){
			musicLerpTimer += Time.deltaTime * 0.000065f;

			float musicStartVol = dialogueSource[0].volume;
			dialogueSource[0].volume = Mathf.Lerp(musicStartVol, 0f, musicLerpTimer);
		}

	}

	void Begin () {
		starting = true;
		dialogueSource[1].PlayDelayed (1f);
		fadeOutObj.SetActive(true);
		fadeOutImg.CrossFadeAlpha (255, 2f, false);
		Invoke ("Load", 16f);
	}

	void Load(){
		Application.LoadLevel ("Scene_MVPMain");
	}
}
