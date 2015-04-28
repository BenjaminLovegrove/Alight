using UnityEngine;
using System.Collections;

public class Croaking : MonoBehaviour {

	float rngTimer = 3f;

	void Update () {
		rngTimer -= Time.deltaTime;

		if (rngTimer <= 0) {
			audio.Play();
			rngTimer = Random.Range (3f, 9f);
		}
	}
}
