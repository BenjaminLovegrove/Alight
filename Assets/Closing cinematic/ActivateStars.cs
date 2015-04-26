using UnityEngine;
using System.Collections;

public class ActivateStars : MonoBehaviour {
	
	GameObject stars;

	void Start(){
		stars = GameObject.FindGameObjectWithTag ("Star");
	}

	void OnCollisionEnter (Collision col) {

	}
}
