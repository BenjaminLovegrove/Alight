using UnityEngine;
using System.Collections;

public class scr_star : MonoBehaviour {

	Light[] lights;
	MeshRenderer meshRenderer;
	float rngTimer = 9999;
	bool activated = false;
	public AudioClip starDing;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		lights = GetComponentsInChildren<Light> ();

		meshRenderer.enabled = false;
		foreach (Light thislight in lights) {
			thislight.enabled = false;
		}
	}

	void Update(){
		if (rngTimer <= 0 && activated == false) {
			activated = true;
			Camera.main.BroadcastMessage("PlaySound", starDing);

			meshRenderer.enabled = true;
			foreach (Light thislight in lights) {
				thislight.enabled = true;
			}
		}

		rngTimer -= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Activate () {
		rngTimer = Random.Range (2.4f, 5f);
	}
}
