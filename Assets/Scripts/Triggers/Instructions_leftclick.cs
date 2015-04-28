using UnityEngine;
using System.Collections;

public class Instructions_leftclick : MonoBehaviour {

	SpriteRenderer sprText;
	Light pressedGlow;
	float startTimer = 2.5f;
	bool startEnable = false;
	Vector3 defaultScale;
	Vector3 pressedScale;

	void Start(){
		sprText = GetComponent<SpriteRenderer> ();
		sprText.enabled = false;
		pressedGlow = GetComponentInChildren<Light> ();
		pressedGlow.enabled = false;
		defaultScale = transform.localScale;
		pressedScale = new Vector3 (transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);
	}

	void Update(){
		startTimer -= Time.deltaTime;

		if (startTimer <= 0 && startEnable == false) {
			startEnable = true;
			sprText.enabled = true;
		}

		if (Input.GetMouseButton (0) && sprText.enabled == true) {
			pressedGlow.enabled = true;
			transform.localScale = pressedScale;
		} else {
			pressedGlow.enabled = false;
			transform.localScale = defaultScale;
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
