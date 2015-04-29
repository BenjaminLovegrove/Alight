using UnityEngine;
using System.Collections;

public class CamMouseMovement : MonoBehaviour {

	Rigidbody rb;
	Vector3 mousePos;
	Vector3 screenCenterPoint;
	public GameObject playerCursor;
	GameObject mainSwarm;
	Vector3 mainSwarmxy;
	float minCamHeight = -20;
	float camLerp = 0;
	public float speed = 0.7f;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");
	}

	// Update is called once per frame
	void Update () {
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs (Camera.main.transform.position.z)));
		screenCenterPoint = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width/2, Screen.height/2, Mathf.Abs (Camera.main.transform.position.z)));

		if (transform.position.y < minCamHeight) {
			transform.position = new Vector3(transform.position.x, minCamHeight, transform.position.z);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Menu_Main");
		}

		mainSwarmxy = new Vector3 (mainSwarm.transform.position.x, mainSwarm.transform.position.y, this.transform.position.z);
		

		if (transform.position.x > -400){
			minCamHeight = -25;
		} else if (transform.position.x < -400 && transform.position.y < -20) {
			camLerp += Time.deltaTime * speed;
			minCamHeight = Mathf.Lerp (-25, -20, camLerp);
		} else {
			camLerp = 0;
		}
	}

	void FixedUpdate(){
		if (playerCursor.renderer.isVisible) {
			if (Mathf.Abs (mousePos.x - screenCenterPoint.x) > 12) {
				rb.AddForce ((mousePos - screenCenterPoint) * Time.deltaTime * 1.5f);
			} else if (Mathf.Abs (mousePos.y - screenCenterPoint.y) > 5) {
				rb.AddForce ((mousePos - screenCenterPoint) * Time.deltaTime * 1.5f);
			}
		}

				
		if (Vector3.Distance (transform.position, mainSwarmxy) > 30) {
			rb.AddForce ((mainSwarmxy - transform.position) * Time.deltaTime * 2f);
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
