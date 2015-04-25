using UnityEngine;
using System.Collections;

public class Instructions_rightclick : MonoBehaviour {
	
	SpriteRenderer sprText;
	float startTimer = 2f;
	bool startEnable = false;
	GameObject mainswarm;

	// Use this for initialization
	void Start () {
		sprText = GetComponent<SpriteRenderer> ();
		sprText.enabled = false;
		mainswarm = GameObject.FindGameObjectWithTag ("MainSwarm");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mainswarm.transform.position, transform.position) < 25) {
			startTimer -= Time.deltaTime;
		}
		
		if (startTimer <= 0 && startEnable == false) {
			startEnable = true;
			sprText.enabled = true;
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly") {
			sprText.enabled = false;
		}
	}
}
