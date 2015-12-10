using UnityEngine;
using System.Collections;

public class Swarming : MonoBehaviour {

	float newposx;
	float newposy;
	Vector3 newpos;
	public GameObject swarmPoint;
	public GameObject mainSwarmPoint;
	public GameObject secondarySwarmPoint;
	float changeDirTime;
	public GameObject fireflyDead;
	public SwarmManagement swarmManager;

	public bool mainSwarm = true;
	public bool soloFirefly = false;

	public Light fireflyHalo;
//	Color originalHalo;
	float originalHaloSize;

	//Editable variables
	float swarmSpeed = 2.5f;
	float swarmNormSpeed;
	float swarmRange = 14f;
	float swarmNormRange;
	float swarmDirectionVolatility = 1.2f;
	
	void Start () {
		mainSwarmPoint = GameObject.FindGameObjectWithTag ("MainSwarm");
		secondarySwarmPoint = GameObject.FindGameObjectWithTag ("SecondarySwarm");
		swarmPoint = mainSwarmPoint;
		swarmNormSpeed = swarmSpeed;
		swarmNormRange = swarmRange;
//		originalHalo = fireflyHalo.color;
		originalHaloSize = fireflyHalo.range;
		swarmManager = GameObject.FindGameObjectWithTag ("MainSwarm").GetComponent<SwarmManagement>();

		//Set initial direction
		ChangeDir ();

		//Give fireflies varying timers
		swarmDirectionVolatility = Random.Range (swarmDirectionVolatility * 0.5f, swarmDirectionVolatility * 1.5f);
	}

	void Update () {
		//Move
		transform.position = Vector3.MoveTowards (transform.position, newpos, swarmSpeed * Time.deltaTime);

		//If it's been some time, change direction
		changeDirTime -= Time.deltaTime;
		if (changeDirTime <= 0) {
			ChangeDir();
		}

		if (!swarmManager.firstSplit) {
			if (mainSwarm) {
				if (swarmManager.currentlyControlling == 0) {
					fireflyHalo.range = originalHaloSize * 1.5f;
				} else {
					fireflyHalo.range = originalHaloSize / 1.5f;
				}
			} else if (!soloFirefly) {
				if (swarmManager.currentlyControlling == 1) {
					fireflyHalo.range = originalHaloSize * 2f;
				} else {
					fireflyHalo.range = originalHaloSize / 1.5f;
				}
			} else if (soloFirefly){
				if (swarmManager.currentlyControlling == 2) {
					fireflyHalo.range = originalHaloSize * 2f;
				} else {
					fireflyHalo.range = originalHaloSize / 1.5f;
				}
			}
		}
	}
	
	void ChangeDir(){
		//Set new location to head towards
		newposx = Random.Range (swarmPoint.transform.position.x - swarmRange, swarmPoint.transform.position.x + swarmRange);
		newposy = Random.Range (swarmPoint.transform.position.y - swarmRange, swarmPoint.transform.position.y + swarmRange);
		newpos = new Vector3 (newposx, newposy, transform.position.z);

		//Set countdown to next new direction
		changeDirTime = swarmDirectionVolatility;
	}

	void SwarmSplit(){ //Called when right click is pressed
		//Change to follow secondary spawn point.
		swarmPoint = secondarySwarmPoint;
		mainSwarm = false;
		swarmRange = 7f;
		swarmSpeed = 1.5f;
		//fireflyHalo.color = Color.red;
		//fireflyHalo.range = originalHaloSize * 2f;
	}

	void SoloSplit(){
		swarmPoint = this.gameObject;
		mainSwarm = false;
		swarmRange = 2.5f;
		swarmSpeed = 2f;
		soloFirefly = true;
		swarmManager.soloFirefly = this.gameObject;
	}

	void SwarmReturn(){
		//When secondary swarm collides with main swarm, tell secondary swarm fireflies to do this.
		//Change swarmPoint back to main swarm point.
		swarmPoint = mainSwarmPoint;
		mainSwarm = true;
		swarmRange = swarmNormRange;
		swarmSpeed = swarmNormSpeed;
		soloFirefly = false;
		//fireflyHalo.color = originalHalo;
		//fireflyHalo.range = originalHaloSize;
	}

	//Solo firefly return to swarms
	void OnTriggerStay(Collider col){
		if (soloFirefly) {
			if (col.gameObject.tag == "SecondarySwarm" && swarmManager.secondarySwarmActive) {
				if (swarmManager.secondarySwarm03 == null) {
					swarmManager.secondarySwarm03 = this.gameObject;
					SwarmSplit ();
					soloFirefly = false;
					swarmManager.soloFirefly = null;
					if (swarmManager.currentlyControlling == 2) {
						swarmManager.currentlyControlling = 1;
					}
				} else if (swarmManager.secondarySwarm02 == null) {
					swarmManager.secondarySwarm02 = this.gameObject;
					SwarmSplit ();
					soloFirefly = false;
					swarmManager.soloFirefly = null;
					if (swarmManager.currentlyControlling == 2) {
						swarmManager.currentlyControlling = 1;
					}
				} else if (swarmManager.secondarySwarm01 == null) {
					swarmManager.secondarySwarm01 = this.gameObject;
					SwarmSplit ();
					soloFirefly = false;
					swarmManager.soloFirefly = null;
					if (swarmManager.currentlyControlling == 2) {
						swarmManager.currentlyControlling = 1;
					}
				}
			} else if (col.gameObject.tag == "MainSwarm" && swarmManager.minSecondaryCollideTimer < 0f) {
				SwarmReturn ();
				soloFirefly = false;
				swarmManager.soloFirefly = null;
				if (swarmManager.currentlyControlling == 2) {
					swarmManager.currentlyControlling = 0;
				}
			}
		}
			
	}
	
	void WallCollide(bool web){
		if (soloFirefly) {
			swarmManager.soloFirefly = null;
			if (swarmManager.secondarySwarmActive){
				swarmManager.currentlyControlling = 1;
			} else {
				swarmManager.currentlyControlling = 0;
			}
		}
		if (!web) {
			Instantiate (fireflyDead, transform.position, Quaternion.identity);
		}
		Destroy (this.gameObject);
		//Play a sound;
	}

}
