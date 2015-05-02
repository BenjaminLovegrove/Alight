using UnityEngine;
using System.Collections;

public class EndStarTrigger : MonoBehaviour {

	public float delay = 2f;
	public GameObject mainSwarm;
	bool firstFireFly = true;

	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (firstFireFly){
			mainSwarm.SendMessage("NoRespawn");
		}

		if (col.gameObject.tag =="FireFly"){
			col.SendMessage("EndingStars", delay);
			delay += 3f;
		}	
	}
}
