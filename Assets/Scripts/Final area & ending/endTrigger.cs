using UnityEngine;
using System.Collections;

public class endTrigger : MonoBehaviour {

	public GameObject moveTowards;
	public GameObject mainSwarm;
	bool end = false;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly" && end == false) {
			end = true;
			mainSwarm.transform.position = moveTowards.transform.position;
			Invoke ("LoadCinematic", 5f);
			Camera.main.SendMessage("Lock");
		}
	}

	void LoadCinematic(){
		Application.LoadLevel ("ending");
	}
}
