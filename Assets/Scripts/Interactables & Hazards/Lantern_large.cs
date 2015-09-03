using UnityEngine;
using System.Collections;

public class Lantern_large : MonoBehaviour {

	public SwarmManagement mainSwarmScr;
	public Light lanternLight; //To turn lantern on
	float desiredIntensity;
	public ParticleSystem particles;
	public AudioClip lanternEnable; //SFX
	GameObject mainSwarm; //To send checkpoints to
	public GameObject spawnPoint;
	public GameObject firefly;

	public bool startActive = false; // this is so the first lantern can start on.
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		particles = GetComponentInChildren<ParticleSystem>();
		mainSwarm = GameObject.FindGameObjectWithTag ("MainSwarm");
		mainSwarmScr = mainSwarm.GetComponent<SwarmManagement>();

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

		if (Vector3.Distance (this.transform.position, Camera.main.transform.position) > 70 && lanternLight.enabled == true) {
			particles.gameObject.SetActive (false);
		} else if (Vector3.Distance (this.transform.position, Camera.main.transform.position) < 70 && lanternLight.enabled == true) {
			particles.gameObject.SetActive (true);
		}
	}
	
	void OnTriggerEnter (Collider col) {
		//Set checkpoint and refresh fireflies.
		if (col.gameObject.tag == "FireFly" && lanternLight.enabled == false) {
			lanternLight.enabled = true;
			particles.gameObject.SetActive(true);
			Camera.main.BroadcastMessage("PlaySound", lanternEnable);

			mainSwarm.SendMessage ("Checkpoint", spawnPoint.transform.position);

			//Up swarm size by 2 and add fireflies.
			mainSwarmScr.maxSwarmSize += 2;
			int fireflyDifference = mainSwarmScr.maxSwarmSize - mainSwarmScr.swarmCount;
			for (int i = 0; i < fireflyDifference; i++){
				Instantiate (firefly, transform.position, Quaternion.identity);
			}
		}
	}
}
