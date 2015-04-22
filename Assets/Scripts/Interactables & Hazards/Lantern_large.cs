using UnityEngine;
using System.Collections;

public class Lantern_large : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	AudioClip lanternEnable; //SFX
	GameObject mainSwarm; //To send checkpoints to
	public GameObject spawnPoint;
	
	void Start () {
		lanternLight = GetComponent<Light>();
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");
	}
	
	
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "FireFly") {
			lanternLight.enabled = true;
			//play sound.

			mainSwarm.SendMessage ("Checkpoint", spawnPoint.transform.position);
		}
	}
}
