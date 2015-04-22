using UnityEngine;
using System.Collections;

public class SwarmManagement : MonoBehaviour {

	public GameObject[] fireFlies;
	int swarmCount;
	bool secondarySwarmActive = false;
	Vector3 checkpointLoc;

	void Start () {
		UpdateFireFlies ();
		checkpointLoc = transform.position; //Set first checkpoint to where the mainswarm object starts.
	}
	

	void Update () {
		swarmCount = fireFlies.Length;

		//Tell 3 fireflies to move to seconds swarm
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount >= 7) {
			fireFlies[0].SendMessage("SwarmSplit");
			fireFlies[1].SendMessage("SwarmSplit"); //Sends a message to 3 fireflies (swarming script) telling them to follow 2nd swarm point isntead of first
			fireFlies[2].SendMessage("SwarmSplit");

			secondarySwarmActive = true;
		}
	

		if (swarmCount <= 0) {
			Respawn();
		}
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
}
