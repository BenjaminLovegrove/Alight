using UnityEngine;
using System.Collections;

public class ActivateStars : MonoBehaviour {
	
	GameObject[] stars;
	bool triggered = false;

	void Start(){
		stars = GameObject.FindGameObjectsWithTag ("Star");
	}

	void OnTriggerEnter (Collider col) {
		print (col.gameObject.tag);
		if (triggered == false){
			triggered = true;

			foreach (GameObject star in stars) {
				star.SendMessage ("Activate", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
