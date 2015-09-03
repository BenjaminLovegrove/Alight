using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmManagement : MonoBehaviour {

	public GameObject[] fireFlies = new GameObject[15];
	public GameObject[] fireFliesSecondary = new GameObject[3];
	public int swarmCount;
	public int maxSwarmSize;
	bool secondarySwarmActive = false;
	Vector3 checkpointLoc;
	float minSecondaryCollideTimer;
	public GameObject secondarySwarmPoint;
	float createSwarmCooldown; //This is because when moving a secondary swarm back to the main swarm it just instantly made another secondary swarm due to holding right click the next frame.
	public GameObject fireFlyPrefab;
	bool firstRespawn = true;
	bool noRespawn = false;

	public AudioClip respawnDialogue;
	public AudioClip respawnSFX;


	GameObject secondarySwarm01;
	GameObject secondarySwarm02;
	GameObject secondarySwarm03;

	bool respawnTrigger = false;

	void Start () {
		UpdateFireFlies ();
		checkpointLoc = transform.position; //Set first checkpoint to where the mainswarm object starts.
		maxSwarmSize = 15;
	}
	

	void Update () {
		UpdateFireFlies ();
		swarmCount = fireFlies.Length;

		//Tell 3 fireflies to move to seconds swarm
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount > 4 && createSwarmCooldown <= 0) {
			secondarySwarmPoint.transform.position = this.transform.position;
			if (fireFlies.Length >= 1 && fireFlies[0] != null)
				fireFlies[0].SendMessage("SwarmSplit");
			if (fireFlies.Length >= 2 && fireFlies[1] != null)
				fireFlies[1].SendMessage("SwarmSplit"); //Sends a message to 3 fireflies (swarming script) telling them to follow 2nd swarm point instead of first
			if (fireFlies.Length >= 3 && fireFlies[2] != null)
				fireFlies[2].SendMessage("SwarmSplit");
			secondarySwarm01 = fireFlies[0];
			secondarySwarm02 = fireFlies[1];
			secondarySwarm03 = fireFlies[2];

			secondarySwarmActive = true;

			minSecondaryCollideTimer = 4f;
		}

		//Tell 1 fireflies to move to seconds swarm, if there are only 2-4 fireflies left
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount <= 4 && swarmCount > 1 && createSwarmCooldown <= 0) {
			secondarySwarmPoint.transform.position = this.transform.position;
			if (fireFlies.Length >= 1 && fireFlies[0] != null)
				fireFlies[0].SendMessage("SwarmSplit");
			secondarySwarm01 = fireFlies[0];
			
			secondarySwarmActive = true;
			
			minSecondaryCollideTimer = 4f;
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
		}
	}

	void NoRespawn(){
		noRespawn = true;
	}



}
