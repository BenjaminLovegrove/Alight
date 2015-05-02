using UnityEngine;
using System.Collections;

public class EndStarTrigger : MonoBehaviour {

	public float delay = 2f;
	public GameObject mainSwarm;
	bool firstFireFly = true;
	public AudioClip finalDialogue; 

	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (firstFireFly){
			mainSwarm.SendMessage("NoRespawn");
			Camera.main.BroadcastMessage("PlayVoice", finalDialogue);
			firstFireFly = false;
			Camera.main.SendMessage("FadeToBlackInitiate");
		}

		if (col.gameObject.tag =="FireFly"){
			col.SendMessage("EndingStars", delay);
			delay += 3f;
		}	
	}
}
