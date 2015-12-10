using UnityEngine;
using System.Collections;

public class CamMouseMovement : MonoBehaviour {

	public bool mouseCameraControl = false;
	Rigidbody rb;
	Vector3 mousePos;
	Vector3 screenCenterPoint;
	public GameObject playerCursor;
	GameObject mainSwarm;
	GameObject secondarySwarm;
	SwarmManagement mainSwarmScr;
	public int startSwarmCount;
	float startCamSize;
	float camSizeTarg;
	Vector3 mainSwarmxy;
	float minCamHeight = -20;
	float camLerp = 0;
	float camReturnLerp = 0;
	Vector3 startReturnPos;
	Vector3 ReturnPos;
	public float speed = 0.7f;
	bool camReturning = false;

	public GameObject fadeToBlack;
	Material ftbMat;
	bool ftb = false;
	float ftbLerp = 0f;
	float ftbAlpha = 0f;

	public AudioClip endingMusic;
	
	bool fadeOut = false;
	bool fadeIn = false;
	float startVol;
	float lerpTimer;
	bool endZoom = false;
	float endZoomLerpTimer = 0f;
	float lerpSpeed = 1000f;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");
		secondarySwarm = GameObject.FindGameObjectWithTag ("SecondarySwarm");
		mainSwarmScr = mainSwarm.GetComponent<SwarmManagement> ();
		startVol = audio.volume;
		ftbMat = fadeToBlack.GetComponent<MeshRenderer>().materials[0];
		startCamSize = this.camera.orthographicSize;
	}

	void Update () {
		//Zoom camera depending on 
		if (!endZoom) {
			if (mainSwarmScr.currentlyControlling == 0){
				camSizeTarg = startCamSize + ((mainSwarmScr.swarmCount - startSwarmCount) / 2f);
			} else if (mainSwarmScr.currentlyControlling == 1) {
				camSizeTarg = startCamSize + ((3 - startSwarmCount) / 3f);
			} else if (mainSwarmScr.currentlyControlling == 2){
				camSizeTarg = startCamSize + ((1 - startSwarmCount) / 3f);
			}

			this.camera.orthographicSize = Mathf.Lerp (this.camera.orthographicSize, camSizeTarg, Time.deltaTime/2);
		}

		//Get mouse pos & center screen
		mousePos = this.camera.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs (this.camera.transform.position.z)));
		screenCenterPoint = this.camera.ScreenToWorldPoint(new Vector3 (Screen.width/2, Screen.height/2, Mathf.Abs (this.camera.transform.position.z)));
		
		//Cam restraint Stuff
		if (transform.position.y < minCamHeight) {
			transform.position = new Vector3(transform.position.x, minCamHeight, transform.position.z);
		}
		
		mainSwarmxy = new Vector3 (mainSwarm.transform.position.x, mainSwarm.transform.position.y, this.transform.position.z);

		//If in swamp area change min cam height to match lower ground plane
		if (transform.position.x > 82 && transform.position.x < 140){
			minCamHeight = -25;
		} else if (transform.position.x < 82 && transform.position.y < -20) {
			camLerp += Time.deltaTime * speed;
			minCamHeight = Mathf.Lerp (-25, -20, camLerp);
		} else if (transform.position.x > 140 && transform.position.y < -20) {
			camLerp += Time.deltaTime * speed;
			minCamHeight = Mathf.Lerp (-25, -20, camLerp);
		} else {
			camLerp = 0;
		}

		//Return camera to main swarm (after second swarm death)
		if (camReturning){
			camReturnLerp += Time.deltaTime * 1f;
			transform.position = Vector3.Lerp (startReturnPos, ReturnPos, camReturnLerp);

			if (camReturnLerp > 1f){
				rb.isKinematic = false;
				camReturning = false;
			}
		}

		if (fadeOut){
			lerpTimer += (Time.deltaTime / 7.0f);
			audio.volume = Mathf.Lerp(startVol, 0f, lerpTimer);
			
			if (audio.volume < 0.1f){
				audio.Stop();
				audio.clip = endingMusic;
				audio.Play ();
				audio.loop = false;
				fadeIn = true;
				fadeOut = false;
				lerpTimer = 0f;
			}
		}
		
		if (fadeIn){
			lerpTimer += Time.deltaTime / 9f;
			audio.volume = Mathf.Lerp(0f, startVol, lerpTimer);
		}

		if (endZoom){
			endZoomLerpTimer += (Time.deltaTime / lerpSpeed);
			this.camera.orthographicSize = Mathf.Lerp(this.camera.orthographicSize, 25f, endZoomLerpTimer);
		}

		if (ftb){
			ftbLerp += (Time.deltaTime / 1500.0f);
			ftbAlpha = Mathf.Lerp(0f, 255f, ftbLerp);
			ftbMat.color = new Color(ftbMat.color.r, ftbMat.color.g, ftbMat.color.b,  ftbAlpha);
			if (Input.anyKeyDown){
				Application.LoadLevel ("Menu_Main");
			} else if (Input.GetMouseButtonDown(0)){
				Application.LoadLevel ("Menu_Main");
			}
		}

		//Esc key
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Menu_Main");
		}

	}

	void FixedUpdate(){
		if (!rb.isKinematic) {
			if (mouseCameraControl) {
				//Move the camera
				if (playerCursor.renderer.isVisible) {
					if (Mathf.Abs (mousePos.x - screenCenterPoint.x) > 4) {
						rb.AddForce ((mousePos - screenCenterPoint) * Time.deltaTime * 1.5f);
					} else if (Mathf.Abs (mousePos.y - screenCenterPoint.y) > 2.5f) {
						rb.AddForce ((mousePos - screenCenterPoint) * Time.deltaTime * 1.5f);
					}
				}

						
				if (Vector3.Distance (transform.position, mainSwarmxy) > 40) {
					rb.AddForce ((mainSwarmxy - transform.position) * Time.deltaTime * 2f);
				}
			} else {
				//Fixed cam
				if (mainSwarmScr.currentlyControlling == 0) {
					Vector3 swarmPos = new Vector3 (mainSwarm.transform.position.x, mainSwarm.transform.position.y, mainSwarm.transform.position.z - 30);
					Vector3 targPos = Vector3.Lerp (mousePos, swarmPos, 0.5f);
					if (Input.GetMouseButton(0)){
						transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 1f);
					} else {
						transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 0.4f);
					}
				} else if (mainSwarmScr.currentlyControlling == 1) {
					Vector3 swarmPos = new Vector3 (secondarySwarm.transform.position.x, secondarySwarm.transform.position.y, secondarySwarm.transform.position.z - 30);
					Vector3 targPos = Vector3.Lerp (mousePos, swarmPos, 0.5f);
					if (Input.GetMouseButton(0)){
						transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 1f);
					} else {
						transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 0.4f);
					}
				} else if (mainSwarmScr.currentlyControlling == 2) {
					Vector3 swarmPos = new Vector3 (mainSwarmScr.soloFirefly.transform.position.x, mainSwarmScr.soloFirefly.transform.position.y, mainSwarmScr.soloFirefly.transform.position.z - 30);
					Vector3 targPos = Vector3.Lerp (mousePos, swarmPos, 0.5f);
					if (Input.GetMouseButton(0)){
						transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 1f);
					} else {
						transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 0.4f);
					}
				}
			}

			/* OLD CAM STUFF

else {
				//Fixed cam
				if (mainSwarmScr.currentlyControlling == 0) {

					Vector3 targPos = new Vector3 (mainSwarm.transform.position.x, mainSwarm.transform.position.y, mainSwarm.transform.position.z - 30);
					transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 1);
				} else if (mainSwarmScr.currentlyControlling == 1) {
					Vector3 targPos = new Vector3 (secondarySwarm.transform.position.x, secondarySwarm.transform.position.y, secondarySwarm.transform.position.z - 30);
					transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 1);
				} else if (mainSwarmScr.currentlyControlling == 2) {
					Vector3 targPos = new Vector3 (mainSwarmScr.soloFirefly.transform.position.x, mainSwarmScr.soloFirefly.transform.position.y, mainSwarmScr.soloFirefly.transform.position.z - 30);
					transform.position = Vector3.Lerp (transform.position, targPos, Time.deltaTime * 1);
				}
			}*/
}
	}

	void ReturnToSwarm(){ 
		startReturnPos = transform.position;

		ReturnPos = mainSwarmxy;
		if (ReturnPos.y < minCamHeight){
			ReturnPos = new Vector3(ReturnPos.x, minCamHeight, ReturnPos.z);
		}

		rb.isKinematic = true;
		camReturning = true;
		camReturnLerp = 0;
	}

	void Lock(){ //this is called when the player reaches the end to stop the cam moving and load the cinematic
		rb.isKinematic = true;
	}

	void Unlock(){
		rb.isKinematic = false;
	}

	
	void EndMusic(){
		fadeOut = true;
		lerpTimer = 0;
		endZoom = true;
	}

	
	void FadeToBlackInitiate(){
		Invoke ("FadeToBlack", 45f);
	}

	void FadeToBlack(){
		ftb = true;
	}

}
