using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lantern_small : MonoBehaviour {
	
	public Light[] lanternLight; //To turn lantern on
	float desiredIntensity0;
	float desiredIntensity1;
	public ParticleSystem particles;
	public AudioClip lanternEnable; //SFX
	//GameObject mainSwarm; Only needed if the distance statement in update is needed for performance
	
	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentsInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		//mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");

		desiredIntensity0 = lanternLight[0].intensity;
		desiredIntensity1 = lanternLight[1].intensity;
		lanternLight[0].intensity = 0;
		lanternLight[1].intensity = 0;
		
		if (!startActive) {
			particles.gameObject.SetActive (false);
		}
	}

	void Update(){
		//Lerp lantern on so it doesnt just go BAM light. Also helps performance when touching a lantern.
		if (lanternLight[0].enabled == true && lanternLight[0].intensity < desiredIntensity0) {
			lanternLight[0].intensity = Mathf.Lerp(lanternLight[0].intensity, desiredIntensity0, Time.deltaTime * 2.2f);
		}

		if (lanternLight[1].enabled == true && lanternLight[1].intensity < desiredIntensity1) {
			lanternLight[1].intensity = Mathf.Lerp(lanternLight[1].intensity, desiredIntensity1, Time.deltaTime * 2.2f);
		}

		if (Vector3.Distance (this.transform.position, Camera.main.transform.position) > 70 && lanternLight[0].enabled == true) {
			particles.gameObject.SetActive (false);
		} else if (Vector3.Distance (this.transform.position, Camera.main.transform.position) < 70 && lanternLight[0].enabled == true) {
			particles.gameObject.SetActive (true);
		}
	}

	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly" && lanternLight[0].enabled == false) {
			lanternLight[0].enabled = true;
			lanternLight[1].enabled = true;
			particles.gameObject.SetActive(true);
			Camera.main.BroadcastMessage("PlaySound", lanternEnable);

		}
	}
}
