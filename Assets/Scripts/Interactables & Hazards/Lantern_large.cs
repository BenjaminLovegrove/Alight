using UnityEngine;
using System.Collections;

public class Lantern_large : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	public ParticleSystem particles;
	AudioClip lanternEnable; //SFX
	GameObject mainSwarm; //To send checkpoints to
	public GameObject spawnPoint;

	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");

		if (!startActive) {
			particles.gameObject.SetActive (false);
		}
	}
	
	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly" && lanternLight.enabled == false) {
			lanternLight.enabled = true;
			particles.gameObject.SetActive(true);
			//play sound.

			mainSwarm.SendMessage ("Checkpoint", spawnPoint.transform.position);
		}
	}
}
