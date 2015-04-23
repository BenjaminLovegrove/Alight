using UnityEngine;
using System.Collections;

public class Lantern_small : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	public ParticleSystem particles;
	public AudioClip lanternEnable; //SFX
	
	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		
		if (!startActive) {
			particles.gameObject.SetActive (false);
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
