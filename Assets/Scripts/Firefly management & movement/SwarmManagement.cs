using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmManagement : MonoBehaviour {

	public GameObject[] fireFlies = new GameObject[15];
	public GameObject[] fireFliesSecondary = new GameObject[3];
	int swarmCount;
	bool secondarySwarmActive = false;
	Vector3 checkpointLoc;
	float minSecondaryCollideTimer;
	public GameObject secondarySwarmPoint;
	float createSwarmCooldown; //This is because when moving a secondary swarm back to the main swarm it just instantly made another secondary swarm due to holding right click the next frame.
	public GameObject fireFlyPrefab;

	public AudioClip respawnSFX;

	GameObject secondarySwarm01;
	GameObject secondarySwarm02;
	GameObject secondarySwarm03;

	void Start () {
		UpdateFireFlies ();
		checkpointLoc = transform.position; //Set first checkpoint to where the mainswarm object starts.
	}
	

	void Update () {
		UpdateFireFlies ();
		swarmCount = fireFlies.Length;

		//Tell 3 fireflies to move to seconds swarm
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount >= 4 && createSwarmCooldown <= 0) {
			secondarySwarmPoint.transform.position = this.transform.position;
			fireFlies[0].SendMessage("SwarmSplit");
			fireFlies[1].SendMessage("SwarmSplit"); //Sends a message to 3 fireflies (swarming script) telling them to follow 2nd swarm point instead of first
			fireFlies[2].SendMessage("SwarmSplit");
			secondarySwarm01 = fireFlies[0];
			secondarySwarm02 = fireFlies[1];
			secondarySwarm03 = fireFlies[2];

			secondarySwarmActive = true;

			minSecondaryCollideTimer = 4f;
		}

		//See if secondary swarm is dead.
		if (secondarySwarm01 == null && secondarySwarm02 == null && secondarySwarm03 == null) {
			secondarySwarmActive = false;
		}

		if (swarmCount <= 0) {
			Invoke ("Respawn", 2f);
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
		Camera.main.BroadcastMessage ("Respawn", respawnSFX);
		Camera.main.transform.position = new Vector3 (checkpointLoc.x, checkpointLoc.y, -30);
		transform.position = checkpointLoc;
		for(int i = 0; i < 15; i++){
			Instantiate(fireFlyPrefab, checkpointLoc, fireFlyPrefab.transform.rotation);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "SecondarySwarm" && minSecondaryCollideTimer <= 0 && secondarySwarmActive) {
			fireFlies[0].SendMessage("SwarmReturn", SendMessageOptions.DontRequireReceiver);
			fireFlies[1].SendMessage("SwarmReturn", SendMessageOptions.DontRequireReceiver); //Sends a message to 3 fireflies (swarming script) telling them to follow 2nd swarm point isntead of first
			fireFlies[2].SendMessage("SwarmReturn", SendMessageOptions.DontRequireReceiver);

			secondarySwarmActive = false;

			createSwarmCooldown = 2f;
		}
	}

}
