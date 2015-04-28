using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {

	public LineRenderer frogTongue;
	public float counter = 2f;
	public float tongueTimer = 0.4f;
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
		frogTongue.SetWidth (1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
		counter -= Time.deltaTime;
		tongueTimer -= Time.deltaTime;

		if (counter <= 0){
			counter = 2f;
			fireflys = GameObject.FindGameObjectsWithTag("FireFly");

			foreach (GameObject firefly in fireflys){
				thisFireFlyDistance = Vector3.Distance (this.transform.position, firefly.transform.position);

				if (thisFireFlyDistance < fireFlyDistance){
					closestFireFly = firefly;
					fireFlyDistance = thisFireFlyDistance;
				}
			}

			if (fireFlyDistance < 30f && closestFireFly != null){
				frogTongue.SetPosition(1, closestFireFly.transform.position);
				tongueTimer = 0.4f;
				Destroy (closestFireFly);
			}
		}
		//
		if (tongueTimer <= 0){
			frogTongue.SetPosition(1, transform.position);
		}

	}
}
