using UnityEngine;
using System.Collections;

public class Lantern_large : MonoBehaviour {
	
	public Light lanternLight; //To turn lantern on
	float desiredIntensity;
	public ParticleSystem particles;
	public AudioClip lanternEnable; //SFX
	GameObject mainSwarm; //To send checkpoints to
	public GameObject spawnPoint;

	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");

		desiredIntensity = lanternLight.intensity;
		lanternLight.intensity = 0;

		if (!startActive) {
			particles.gameObject.SetActive (false);
		}
	}

	void Update(){
		//Lerp lantern on so it doesnt just go BAM light. Also helps performance when touching a lantern.
		if (lanternLight.enabled == true && lanternLight.intensity < desiredIntensity) {
			lanternLight.intensity = Mathf.Lerp(lanternLight.intensity, desiredIntensity, Time.deltaTime * 1f);
		}

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

			mainSwarm.SendMessage ("Checkpoint", spawnPoint.transform.position);
		}
	}
}
