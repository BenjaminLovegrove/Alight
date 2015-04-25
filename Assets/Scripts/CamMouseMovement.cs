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
		if (Mathf.Abs(mousePos.x - screenCenterPoint.x) > 13) {
			rb.AddForce ((mousePos - screenCenterPoint) * Time.deltaTime * 1.5f);
		} else if (Mathf.Abs(mousePos.y - screenCenterPoint.y) > 5) {
			rb.AddForce ((mousePos - screenCenterPoint) * Time.deltaTime * 1.5f);
		}

		//This makes x a bit too sensative and y not enough.
		/*if (Vector3.Distance (mousePos, screenCenterPoint) > 8){
			rb.AddForce((mousePos - screenCenterPoint) * Time.deltaTime * 2);
		}
		*/
	}

	void Lock(){ //this is called when the player reaches the end to stop the cam moving and load the cinematic
		rb.isKinematic = true;
	}

	void Unlock(){
		rb.isKinematic = false;
	}
}
