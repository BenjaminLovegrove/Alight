using UnityEngine;
using System.Collections;

public class Instructions_rightclick : MonoBehaviour {
	
	SpriteRenderer sprText;
	float startTimer = 3.5f;
	bool startEnable = false;
	GameObject mainswarm;
	Vector3 defaultScale;
	Vector3 pressedScale;
	Light pressedGlow;

	// Use this for initialization
	void Start () {
		sprText = GetComponent<SpriteRenderer> ();
		sprText.enabled = false;
		mainswarm = GameObject.FindGameObjectWithTag ("MainSwarm");
		defaultScale = transform.localScale;
		pressedScale = new Vector3 (transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);
		pressedGlow = GetComponentInChildren<Light> ();
		pressedGlow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mainswarm.transform.position, transform.position) < 35) {
			startTimer -= Time.deltaTime;
		}
		
		if (startTimer <= 0 && startEnable == false) {
			startEnable = true;
			sprText.enabled = true;
		}

		if (Input.GetMouseButton (1) && sprText.enabled == true) {
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
		}
	}
}
