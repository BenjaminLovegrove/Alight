using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {

	public LineRenderer frogTongue;
	public float counter;
	public float tongueTimer;
	public float setFrogStrikeTime;
	public float dist;
	public float lineDrawSpeed = 6f;
	public GameObject[] fireflys;
	public float fireFlyDistance = 999999;
	public float thisFireFlyDistance;
	public GameObject closestFireFly;
	bool crushed = false;
	public AudioClip crushSound;

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
			counter = setFrogStrikeTime;
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
			if (fireFlyDistance < 15f && closestFireFly != null && crushed == false){
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

	void Crushed(){
		crushed = true;

		float rngSoundTimer = Random.Range (3.5f, 4f);
		Invoke ("PlaySquish", rngSoundTimer);

		Transform croakingSFX = transform.Find ("Croaking");
		croakingSFX.gameObject.SetActive (false);

		Transform frogParticles = transform.Find ("FrogParticles");
		if (frogParticles != null) {
			if (frogParticles.gameObject != null)
				Destroy (frogParticles.gameObject);
		}
	}

	void PlaySquish(){
		if (crushSound != null) {
			AudioSource.PlayClipAtPoint (crushSound, transform.position, 0.7f);
		}
	}
}
