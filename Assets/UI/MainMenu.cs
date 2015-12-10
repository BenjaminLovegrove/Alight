using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	GameObject fadeOutObj;
	Image fadeOutImg;
	Image credits;
	float scrollDelay = 1f;
	bool creditsOpen = false;
	Vector3 OriginalcreditsPos;
	public AudioSource[] dialogueSource;

	private float pauseTimer = 0.0f;
	float musicLerpTimer = 0;
	bool starting = false;
	private float musicStartVol;

	void Start(){
		fadeOutObj = GameObject.Find ("Image");
		fadeOutImg = fadeOutObj.GetComponent<Image>();
		credits = GameObject.Find ("Credits").GetComponent<Image> ();
		fadeOutObj.SetActive(false);
		credits.enabled = false;
		dialogueSource = GetComponentsInChildren<AudioSource> ();
		OriginalcreditsPos = credits.gameObject.transform.position;
		musicStartVol = dialogueSource[0].volume;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) && !creditsOpen) {
			Application.Quit ();
		}

		if (starting){
			musicLerpTimer += Time.deltaTime / 10;
		
			dialogueSource[0].volume = Mathf.Lerp(musicStartVol, 0f, musicLerpTimer);
		}

		if (creditsOpen){

			scrollDelay -= Time.deltaTime;
			if (scrollDelay <= 0 && credits.gameObject.transform.position.y <= 520.0f){
				credits.gameObject.transform.Translate (Vector3.up * Time.deltaTime * 30);
			} else {
				pauseTimer += Time.deltaTime;
				if (pauseTimer >= 5.0f) {
					credits.enabled = false;
					creditsOpen = false;
					pauseTimer = 0.0f;
				}
			}

			if (Input.GetMouseButtonDown(0)){
				credits.enabled = false;
				creditsOpen = false;
			}

			if (Input.anyKeyDown && credits.enabled == true){
				credits.enabled = false;
				creditsOpen = false;
			}

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

	void Credits(){
		scrollDelay = 1f;
		credits.gameObject.transform.position = OriginalcreditsPos;
		credits.enabled = true;
		creditsOpen = true;
	}
}
