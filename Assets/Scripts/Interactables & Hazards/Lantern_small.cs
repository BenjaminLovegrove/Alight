﻿using UnityEngine;
using System.Collections;

public class Lantern_small : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	float desiredIntensity;
	public ParticleSystem particles;
	public AudioClip lanternEnable; //SFX
	//GameObject mainSwarm; Only needed if the distance statement in update is needed for performance
	
	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		//mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");

		desiredIntensity = lanternLight.intensity;
		lanternLight.intensity = 0;
		
		if (!startActive) {
			particles.gameObject.SetActive (false);
		}
	}

	void Update(){
		//Lerp lantern on so it doesnt just go BAM light. Also helps performance when touching a lantern.
		if (lanternLight.enabled == true && lanternLight.intensity < desiredIntensity) {
			lanternLight.intensity = Mathf.Lerp(lanternLight.intensity, desiredIntensity, Time.deltaTime * 2.2f);
		}

		if (Vector3.Distance (this.transform.position, Camera.main.transform.position) > 70 && lanternLight.enabled == true) {
			particles.gameObject.SetActive (false);
		} else if (Vector3.Distance (this.transform.position, Camera.main.transform.position) < 70 && lanternLight.enabled == true) {
			particles.gameObject.SetActive (true);
		}
	}

	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly" && lanternLight.enabled == false) {
			lanternLight.enabled = true;
			particles.gameObject.SetActive(true);
			Camera.main.BroadcastMessage("PlaySound", lanternEnable);

		}
	}
}
