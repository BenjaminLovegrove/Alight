using UnityEngine;
using System.Collections;

public class FireflyMovement : MonoBehaviour {

	public 	Rigidbody 		rb;
	public 	GameObject 		FireflyDragger;
	private Vector3 		dir;
	public 	bool 			mainSwarm;
	private	Swarming 		scrSwarm;
	public 	AudioClip 		ffDeath;
	private	bool 			lightOn = false;
	public 	bool 			initialFirefly = false; //Needs to be manually set to true on starting fireflies so they start without light.
	private	TrailRenderer 	trail;
	public int currentlyControlling = 0; //mainswarm 0, secondary 1, third 2
	public SwarmManagement swarmManager;

	//Ending
	public GameObject star1;
	float starTimer;
	public AudioClip starSound;
	bool ending = false;
	bool starMade = false;

	//For light fade in
	public 	Light 			areaLight;
	private float 			startIntensity;


	void Start () {
		rb = GetComponent<Rigidbody>();
		FireflyDragger = GameObject.FindGameObjectWithTag ("FireflyDragger");
		scrSwarm = GetComponent<Swarming>();
		trail = GetComponent<TrailRenderer> ();
		swarmManager = GameObject.FindGameObjectWithTag ("MainSwarm").GetComponent<SwarmManagement>();

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

		//Ending
		if (ending && starMade == false){
			starTimer -= Time.deltaTime;
			if (starTimer <= 0){
				Instantiate (star1, transform.position, Quaternion.identity);
				AudioSource.PlayClipAtPoint (starSound, Camera.main.transform.position);
				starMade = true;
				Destroy (this.gameObject);
			}
		}

	}

	void FixedUpdate(){
		//Movement
		if (Input.GetMouseButton (0) && mainSwarm && swarmManager.currentlyControlling == 0) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}
		
		if (Input.GetMouseButton (0) && !mainSwarm && swarmManager.soloFirefly != this.gameObject && swarmManager.currentlyControlling == 1) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}

		if (Input.GetMouseButton (0) && swarmManager.soloFirefly == this.gameObject && swarmManager.currentlyControlling == 2) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(20,25));
		}

	}

	void LightOn(){
		lightOn = true;
	}

	void EndingStars(float delay){
		//Get random range within 20 like with stars.
		//lerp to spot.
		//When reached turn into star.
		
		Invoke ("Lock", (delay + 3f));
		starTimer = delay + 2f;
		ending = true;
	}

	void Lock(){
		rb.isKinematic = true;
	}


}
