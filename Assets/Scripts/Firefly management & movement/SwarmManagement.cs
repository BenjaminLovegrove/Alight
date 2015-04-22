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

	void Start () {
		UpdateFireFlies ();
		checkpointLoc = transform.position; //Set first checkpoint to where the mainswarm object starts.
	}
	

	void Update () {
		swarmCount = fireFlies.Length;

		//If there is no secondary swarm keep the secondary swarm point at the same location.
		if (!secondarySwarmActive) {
			secondarySwarmPoint.transform.position = this.transform.position;
		}

		//Tell 3 fireflies to move to seconds swarm
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount >= 7 && createSwarmCooldown <= 0) {
			fireFlies[0].SendMessage("SwarmSplit");
			fireFlies[1].SendMessage("SwarmSplit"); //Sends a message to 3 fireflies (swarming script) telling them to follow 2nd swarm point isntead of first
			fireFlies[2].SendMessage("SwarmSplit");

			secondarySwarmActive = true;

			minSecondaryCollideTimer = 5f;
		}
	

		if (swarmCount <= 0) {
			Respawn();
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
	}

	void onCollisionEnter(Collision col){
		print ("yiiisss 2");

		if (col.gameObject.tag == "SecondarySwarm" && minSecondaryCollideTimer <= 0) {
			fireFlies[0].SendMessage("SwarmReturn");
			fireFlies[1].SendMessage("SwarmReturn"); //Sends a message to 3 fireflies (swarming script) telling them to follow 2nd swarm point isntead of first
			fireFlies[2].SendMessage("SwarmReturn");

			secondarySwarmActive = false;

			createSwarmCooldown = 2f;
		}
	}
}
