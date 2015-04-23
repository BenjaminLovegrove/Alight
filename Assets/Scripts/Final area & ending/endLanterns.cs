using UnityEngine;
using System.Collections;

public class endLanterns : MonoBehaviour {

	public int lanternNumber;
	public GameObject endVine;
	public Light lanternLight; //To turn lantern on
	//public ParticleSystem particles;
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		endVine = GameObject.FindGameObjectWithTag ("finalVine");
		//particles = GetComponentInChildren<ParticleSystem>();
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly" && lanternLight.enabled == false) {
			lanternLight.enabled = true;
			endVine.SendMessage("LanternOn", lanternNumber);
			//particles.gameObject.SetActive(true);
		}
	}

	void Incorrect(){
		Invoke ("TurnOff", 2f);
	}

	void TurnOff(){
		lanternLight.enabled = false;
	}
}
