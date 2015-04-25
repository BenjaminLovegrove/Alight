using UnityEngine;
using System.Collections;

public class Lantern_small : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	public ParticleSystem particles;
	public AudioClip lanternEnable; //SFX
	//GameObject mainSwarm; Only needed if the distance statement in update is needed for performance
	
	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		//mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");
		
		if (!startActive) {
			particles.gameObject.SetActive (false);
		}
	}

	void Update(){
		/*This is for performance if needed. Turns lanterns that you've gone a bit past off.
		 * if (Vector3.Distance(this.transform.position, mainSwarm.transform.position) > 250) {
			lanternLight.enabled = false;
			particles.gameObject.SetActive(false);
		}
		*/
	}

	
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly" && lanternLight.enabled == false) {
			lanternLight.enabled = true;
			particles.gameObject.SetActive(true);
			Camera.main.BroadcastMessage("PlaySound", lanternEnable);

		}
	}
}
