using UnityEngine;
using System.Collections;

public class FireflyMovement : MonoBehaviour {

	public Rigidbody rb;
	public GameObject FireflyDragger;
	Vector3 dir;
	public bool mainSwarm;
	Swarming scrSwarm;
	public AudioClip ffDeath;
	bool lightOn = false;
	public bool initialFirefly = false; //Needs to be manually set to true on starting fireflies so they start without light.
	TrailRenderer trail;

	//For light fade in
	public Light areaLight;
	float startIntensity;

	void Start () {
		rb = GetComponent<Rigidbody>();
		FireflyDragger = GameObject.FindGameObjectWithTag ("FireflyDragger");
		scrSwarm = GetComponent<Swarming>();
		trail = GetComponent<TrailRenderer> ();

		//Set starting fireflies to start with no light
		areaLight = GetComponentInChildren<Light>();
		startIntensity = areaLight.intensity;
		if (initialFirefly) {
			areaLight.intensity = 0;
		}
	}
	
	void Update () {
		//Check if main swarm
		if (scrSwarm.mainSwarm) {
			mainSwarm = true;
		} else {
			mainSwarm = false;
		}

		//Get direction
		Vector3 FireflyDraggerPos = FireflyDragger.transform.position;
		dir = FireflyDraggerPos - transform.position;

		//Lerp up light at the start
		if (lightOn == true && areaLight.intensity < startIntensity) {
			areaLight.intensity = Mathf.Lerp(areaLight.intensity, startIntensity, Time.deltaTime * 0.25f);
		}


		//Trail only when moving
		if (rb.velocity.magnitude > 1.2f){
			trail.enabled = true;
		} else {
			trail.enabled = false;
		}

	}

	void FixedUpdate(){
		//Movement
		if (Input.GetMouseButton (0) && mainSwarm) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}
		
		if (Input.GetMouseButton (1) && !mainSwarm) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}
	}

	void LightOn(){
		lightOn = true;
	}
}
