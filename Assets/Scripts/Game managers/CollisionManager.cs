using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnCollisionEnter (Collision coll) {

		//If fireflies hit vine/spider web/water
		if (gameObject.tag == "Firefly" && coll.gameObject.tag == "Hazard")
		Destroy(this.gameObject);

		}

	// Update is called once per frame
	void Update () {
	
	}
}
