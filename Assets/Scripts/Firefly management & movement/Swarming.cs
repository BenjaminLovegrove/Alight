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

	public bool mainSwarm = true;

	public Light fireflyHalo;
	Color originalHalo;
	float originalHaloSize;

	//Editable variables
	float swarmSpeed = 2f;
	float swarmNormSpeed;
	float swarmRange = 16f;
	float swarmNormRange;
	float swarmDirectionVolatility = 1f;
	
	void Start () {
		mainSwarmPoint = GameObject.FindGameObjectWithTag ("MainSwarm");
		secondarySwarmPoint = GameObject.FindGameObjectWithTag ("SecondarySwarm");
		swarmPoint = mainSwarmPoint;
		swarmNormSpeed = swarmSpeed;
		swarmNormRange = swarmRange;
		originalHalo = fireflyHalo.color;
		originalHaloSize = fireflyHalo.range;

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
		fireflyHalo.range = originalHaloSize * 2f;
	}

	void SwarmReturn(){
		//When secondary swarm collides with main swarm, tell secondary swarm fireflies to do this.
		//Change swarmPoint back to main swarm point.
		swarmPoint = mainSwarmPoint;
		mainSwarm = true;
		swarmRange = swarmNormRange;
		swarmSpeed = swarmNormSpeed;
		fireflyHalo.color = originalHalo;
		fireflyHalo.range = originalHaloSize;
	}

	
	void WallCollide(){
		Instantiate (fireflyDead, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
		//Play a sound;
	}

}
