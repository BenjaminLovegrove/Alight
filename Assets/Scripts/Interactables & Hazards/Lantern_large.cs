using UnityEngine;
using System.Collections;

public class Lantern_large : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	AudioClip lanternEnable; //SFX
	GameObject mainSwarm; //To send checkpoints to
	
	void Start () {
		lanternLight = GetComponent<Light>();
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");
	}
	
	
	void onCollisionEnter (Collision col) {
		if (col.gameObject.tag == "FireFly") {
			lanternLight.enabled = true;
			//play sound.

			mainSwarm.SendMessage ("Checkpoint", transform.position);
		}
	}
}
