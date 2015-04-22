using UnityEngine;
using System.Collections;

public class Lantern_small : MonoBehaviour {

	public Light lanternLight;
	AudioClip lanternEnable;

	void Start () {
		lanternLight = GetComponent<Light>();
	}
	

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "FireFly") {
			lanternLight.enabled = true;
			//play sound.
		}
	}
}
