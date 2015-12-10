using UnityEngine;
using System.Collections;

public class SwarmPointMovement : MonoBehaviour {
	
	public Rigidbody rb;
	public GameObject FireflyDragger;
	Vector3 dir;
	public bool mainSwarm; //Need to set this for each swarm point so left/right click only drag one of them.
	Swarming scrSwarm;
	public SwarmManagement swarmManager;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		FireflyDragger = GameObject.FindGameObjectWithTag ("FireflyDragger");
		swarmManager = GameObject.FindGameObjectWithTag ("MainSwarm").GetComponent<SwarmManagement>();
	}
	
	void Update () {
		
		Vector3 FireflyDraggerPos = FireflyDragger.transform.position;
		dir = FireflyDraggerPos - transform.position;

	}

	void FixedUpdate(){
		if (mainSwarm && swarmManager.currentlyControlling == 0) {
			rb.AddForce (dir.normalized * Time.deltaTime * 13);
		}
		
		if (!mainSwarm && swarmManager.currentlyControlling == 1) {
			rb.AddForce (dir.normalized * Time.deltaTime * 13);
		}
	}
}