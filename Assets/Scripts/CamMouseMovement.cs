using UnityEngine;
using System.Collections;

public class CamMouseMovement : MonoBehaviour {

	Rigidbody rb;
	Vector3 mousePos;
	Vector3 screenCenterPoint;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
	}

	// Update is called once per frame
	void Update () {
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 30));
		screenCenterPoint = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width/2, Screen.height/2, 30));

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void FixedUpdate(){
		if (Vector3.Distance (mousePos, screenCenterPoint) > 8){
			rb.AddForce((mousePos - screenCenterPoint) * Time.deltaTime * 2);
		}
	}

	void Lock(){ //this is called when the player reaches the end to stop the cam moving and load the cinematic
		rb.isKinematic = true;
	}

	void Unlock(){
		rb.isKinematic = false;
	}
}
