﻿using UnityEngine;
using System.Collections;

public class oldSwarmManagement : MonoBehaviour {

	public GameObject[] fireFlies;
	int swarmCount;
	bool secondarySwarmActive = false;

	void Start () {
		UpdateFireFlies ();
	}
	

	void Update () {
		swarmCount = fireFlies.Length;

		//Tell 3 fireflies to move to seconds swarm
		if (Input.GetMouseButtonDown (1) && secondarySwarmActive == false && swarmCount >= 7) {
			fireFlies[0].SendMessage("SwarmSplit");
			fireFlies[1].SendMessage("SwarmSplit");
			fireFlies[2].SendMessage("SwarmSplit");

			secondarySwarmActive = true;
		}
	
	}

	void UpdateFireFlies(){ 
		//Call this whenever fireflies are respawned/use in some way when secondary spawn returns
		//Get all fireflies as an array
		fireFlies = GameObject.FindGameObjectsWithTag ("FireFly");
	}
}
