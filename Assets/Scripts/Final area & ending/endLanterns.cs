using UnityEngine;
using System.Collections;

public class endLanterns : MonoBehaviour {

	public int lanternNumber;
	public GameObject endVine;
	public Light lanternLight; //To turn lantern on
	
	void Start () {
		lanternLight = GetComponentInChildren<Light>();
		endVine = GameObject.FindGameObjectWithTag ("finalVine");

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
