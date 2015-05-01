using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour {

	public AudioClip thisDialogue;
	bool triggered = false;

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "FireFly"){
			if (!triggered){
				Camera.main.BroadcastMessage ("PlayVoice", thisDialogue);
				triggered = true;
			}
		}

	}
}
