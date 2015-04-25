using UnityEngine;
using System.Collections;

public class SwarmPointMovement : MonoBehaviour {
	
	public Rigidbody rb;
	public GameObject FireflyDragger;
	Vector3 dir;
	public bool mainSwarm; //Need to set this for each swarm point so left/right click only drag one of them.
	Swarming scrSwarm;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		FireflyDragger = GameObject.FindGameObjectWithTag ("FireflyDragger");
	}
	
	void Update () {
		
		Vector3 FireflyDraggerPos = FireflyDragger.transform.position;
		dir = FireflyDraggerPos - transform.position;

	}

	void FixedUpdate(){
		if (Input.GetMouseButton (0) && mainSwarm) {
			rb.AddForce (dir.normalized * Time.deltaTime * 20);
		}
		
		if (Input.GetMouseButton (1) && !mainSwarm) {
			rb.AddForce (dir.normalized * Time.deltaTime * 20);
		}
	}
}