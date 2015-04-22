using UnityEngine;
using System.Collections;

public class FireflyMovement : MonoBehaviour {

	public Rigidbody rb;
	public GameObject FireflyDragger;
	Vector3 dir;
	public bool mainSwarm;
	Swarming scrSwarm;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
		FireflyDragger = GameObject.FindGameObjectWithTag ("FireflyDragger");
		scrSwarm = GetComponent<Swarming>();
	}
	
	void Update () {
		if (scrSwarm.mainSwarm) {
			mainSwarm = true;
		} else {
			mainSwarm = false;
		}

		Vector3 FireflyDraggerPos = FireflyDragger.transform.position;
		dir = FireflyDraggerPos - transform.position;
		
		if (Input.GetMouseButton (0) && mainSwarm) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}

		if (Input.GetMouseButton (1) && !mainSwarm) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}
	}
}
