using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {

	public LineRenderer frogTongue;
	public float counter = 3f;
	public float tongueTimer = 0.2f;
	public float dist;
	public float lineDrawSpeed = 6f;
	public GameObject[] fireflys;
	public float fireFlyDistance = 999999;
	public float thisFireFlyDistance;
	public GameObject closestFireFly;

	// Use this for initialization
	void Start () {
		frogTongue = GetComponent<LineRenderer> ();
		frogTongue.SetPosition (0, transform.position);
		frogTongue.SetWidth (0.3f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
	//Start counter and tongueTimer
		counter -= Time.deltaTime;
		tongueTimer -= Time.deltaTime;
		//Resets counter and selects all fireflies
		if (counter <= 0){
			counter = 3f;
			fireflys = GameObject.FindGameObjectsWithTag("FireFly");
			//Finds all fireflies' distances
			foreach (GameObject firefly in fireflys){
				thisFireFlyDistance = Vector3.Distance (this.transform.position, firefly.transform.position);
				//Finds closest firefly
				if (thisFireFlyDistance < fireFlyDistance){
					closestFireFly = firefly;
					fireFlyDistance = thisFireFlyDistance;
				}
			}
			//Attack and destroy nearest firefly
			if (fireFlyDistance < 20f && closestFireFly != null){
				frogTongue.SetPosition(1, closestFireFly.transform.position);
				tongueTimer = 0.2f;
				Destroy (closestFireFly);
				fireFlyDistance = 99999;
				audio.Play();
			}
		}
		//Resets tongue's position after countdown
		if (tongueTimer <= 0){
			frogTongue.SetPosition(1, transform.position);
		}

	}
}
