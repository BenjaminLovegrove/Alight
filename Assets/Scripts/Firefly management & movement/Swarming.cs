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
	float swarmSplitSpeedTimer = 0; //Timer for speeding up split fireflies to move to new point faster *TEMP* this is just while testing until proper movement is put in.

	public bool mainSwarm = true;

	//Editable variables
	float swarmSpeed = 3f;
	float swarmNormSpeed;
	float swarmRange = 15f;
	float swarmDirectionVolatility = 1f;
	
	void Start () {
		mainSwarmPoint = GameObject.FindGameObjectWithTag ("MainSwarm");
		secondarySwarmPoint = GameObject.FindGameObjectWithTag ("SecondarySwarm");
		swarmPoint = mainSwarmPoint;
		swarmNormSpeed = swarmSpeed;

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

		//*TEMP* this is just while testing until proper movement is put in.
		swarmSplitSpeedTimer -= Time.deltaTime;
		if (swarmSplitSpeedTimer <= 0) {
			swarmSpeed = swarmNormSpeed;
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

		//Speed up only the split fireflies *TEMP* this is just while testing until proper movement is put in.
		swarmSpeed = 1.5f;
		swarmSplitSpeedTimer = 10f;
	}

	void SwarmReturn(){
		//When secondary swarm collides with main swarm, tell secondary swarm fireflies to do this.
		//Change swarmPoint back to main swarm point.
	}

}
