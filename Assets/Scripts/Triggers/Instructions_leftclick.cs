using UnityEngine;
using System.Collections;

public class Instructions_leftclick : MonoBehaviour {

	SpriteRenderer sprText;
	float startTimer = 3f;
	bool startEnable = false;

	void Start(){
		sprText = GetComponent<SpriteRenderer> ();
		sprText.enabled = false;
	}

	void Update(){
		startTimer -= Time.deltaTime;

		if (startTimer <= 0 && startEnable == false) {
			startEnable = true;
			sprText.enabled = true;
		}
	}
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly") {
			sprText.enabled = false;
			col.SendMessage("LightOn");
			Camera.main.SendMessage ("Unlock");
		}
	}
}
