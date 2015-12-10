using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmManagement : MonoBehaviour {

	public GameObject[] fireFlies = new GameObject[99];
	public int swarmCount;
	public int maxSwarmSize;
	public bool secondarySwarmActive = false;
	Vector3 checkpointLoc;
	public float minSecondaryCollideTimer;
	public GameObject secondarySwarmPoint;
	float createSwarmCooldown; //This is because when moving a secondary swarm back to the main swarm it just instantly made another secondary swarm due to holding right click the next frame.
	public GameObject fireFlyPrefab;
	bool firstRespawn = true;
	bool noRespawn = false;
	public int currentlyControlling = 0; //mainswarm 0, secondary 1, third 2
	public bool firstSplit = true;
	bool canSplit = false;

	public AudioClip respawnDialogue;
	public AudioClip respawnSFX;


	public GameObject secondarySwarm01;
	public GameObject secondarySwarm02;
	public GameObject secondarySwarm03;
	public GameObject soloFirefly;

	bool respawnTrigger = false;

	void Start () {
		UpdateFireFlies ();
		checkpointLoc = transform.position; //Set first checkpoint to where the mainswarm object starts.
		maxSwarmSize = 11;
	}
	

	void Update () {
		UpdateFireFlies ();
		swarmCount = fireFlies.Length;
		if (canSplit){
			SplitFireflies ();
		} else if (Camera.main.GetComponent<Rigidbody>().isKinematic == false){
			canSplit = true;
		}

		//See if secondary swarm is dead.
		if (secondarySwarm01 == null && secondarySwarm02 == null && secondarySwarm03 == null && secondarySwarmActive == true) {
			secondarySwarmActive = false;
			Camera.main.SendMessage("ReturnToSwarm");
		}

		if (swarmCount <= 0 && respawnTrigger == false) {
			respawnTrigger = true;
			if (firstRespawn){
				Invoke ("Respawn", 6.2f);
				firstRespawn = false;
				Camera.main.BroadcastMessage ("PlayVoice", respawnDialogue);
			} else {
				Invoke ("Respawn", 2.5f);
			}
		}

		//Change which swarm is being controlled
		if (Input.GetMouseButtonDown (1)) {
			if (currentlyControlling == 0){
				currentlyControlling = 1;
			} else if (currentlyControlling == 1) {
				currentlyControlling = 2;
			} else if (currentlyControlling == 2) {
				currentlyControlling = 0;
			}
		}

		//Reduce cooldowns
		minSecondaryCollideTimer -= Time.deltaTime;
		createSwarmCooldown -= Time.deltaTime;
	}

	void UpdateFireFlies(){ 
		//Call this whenever fireflies are respawned/use in some way when secondary spawn returns
		//Get all fireflies as an array
		fireFlies = GameObject.FindGameObjectsWithTag ("FireFly");
	}

	void Checkpoint(Vector3 checkpoint){
		checkpointLoc = checkpoint;
	}

	void Respawn(){
		//Respawn fireflies at checkpoint.
		//Move cam to checkpoint.
		//Move main swarm point to respawn point.
		if (!noRespawn){
			Camera.main.BroadcastMessage ("Respawn", respawnSFX);
			Camera.main.transform.position = new Vector3 (checkpointLoc.x, checkpointLoc.y, -30);
			transform.position = checkpointLoc;
			for(int i = 0; i < 15; i++){
				Instantiate(fireFlyPrefab, checkpointLoc, fireFlyPrefab.transform.rotation);
			}
			respawnTrigger = false;
		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "SecondarySwarm" && minSecondaryCollideTimer <= 0 && secondarySwarmActive) {
			if (fireFlies.Length >= 1 && secondarySwarm01 != null )
				secondarySwarm01.SendMessage("SwarmReturn", SendMessageOptions.DontRequireReceiver);
			if (fireFlies.Length >= 2 && secondarySwarm02 != null)
				secondarySwarm02.SendMessage("SwarmReturn", SendMessageOptions.DontRequireReceiver);
			if (fireFlies.Length >= 3 && secondarySwarm03 != null)
				secondarySwarm03.SendMessage("SwarmReturn", SendMessageOptions.DontRequireReceiver);

			secondarySwarmActive = false;

			createSwarmCooldown = 0f;
			currentlyControlling = 0;
		}
	}

	void SplitFireflies (){
		//Tell 3 fireflies to move to seconds swarm
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount > 4 && createSwarmCooldown <= 0) {
			bool extraRequired = false;
			int fireFlyRequired = 0;
			secondarySwarmPoint.transform.position = this.transform.position;
			if (fireFlies.Length >= 1 && fireFlies[0] != null){
				if (fireFlies[0].GetComponent<Swarming>().soloFirefly == false){
					fireFlies[0].SendMessage("SwarmSplit");
					secondarySwarm01 = fireFlies[0];
				} else {
					extraRequired = true;
					fireFlyRequired = 0;
				}
			}
			if (fireFlies.Length >= 2 && fireFlies[1] != null){
				if (fireFlies[1].GetComponent<Swarming>().soloFirefly == false){
					fireFlies[1].SendMessage("SwarmSplit");
					secondarySwarm02 = fireFlies[1];
				} else {
					extraRequired = true;
					fireFlyRequired = 1;
				}
			}
			if (fireFlies.Length >= 3 && fireFlies[2] != null){
				if (fireFlies[2].GetComponent<Swarming>().soloFirefly == false){
					fireFlies[2].SendMessage("SwarmSplit");
					secondarySwarm03 = fireFlies[2];
				} else {
					extraRequired = true;
					fireFlyRequired = 2;
				}
			}

			if (extraRequired){
				if (fireFlies.Length >= 4 && fireFlies[4] != null){
					fireFlies[4].SendMessage("SwarmSplit");
					if (fireFlyRequired == 0){
						secondarySwarm01 = fireFlies[4];
					} else if (fireFlyRequired == 1){
						secondarySwarm02 = fireFlies[4];
					} else if (fireFlyRequired == 2){
						secondarySwarm03 = fireFlies[4];
					}
				}
			}

			secondarySwarmActive = true;
			
			minSecondaryCollideTimer = 4f;
			
			if (firstSplit){
				firstSplit = false;
			}
		}
		
		//Tell 1 firefly to move to seconds swarm, if there are only 2-4 fireflies left
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount <= 4 && swarmCount > 1 && createSwarmCooldown <= 0) {
			secondarySwarmPoint.transform.position = this.transform.position;
			if (fireFlies.Length >= 1 && fireFlies[0] != null)
				fireFlies[0].SendMessage("SwarmSplit");
			secondarySwarm01 = fireFlies[0];
			
			secondarySwarmActive = true;
			
			minSecondaryCollideTimer = 4f;
		}

		if (Input.GetMouseButtonDown (1) && secondarySwarmActive && soloFirefly == null && currentlyControlling == 1) {
			if (secondarySwarm03 != null){
				secondarySwarm03.SendMessage("SoloSplit");
				secondarySwarm03 = null;
			} else if (secondarySwarm02 != null){
				secondarySwarm02.SendMessage("SoloSplit");
				secondarySwarm02 = null;
			} else if (secondarySwarm01 != null){
				secondarySwarm01.SendMessage("SoloSplit");
				secondarySwarm01 = null;
			}
		}
	}

	void NoRespawn(){
		noRespawn = true;
	}



}
