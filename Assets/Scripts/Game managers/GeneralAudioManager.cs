using UnityEngine;
using System.Collections;

public class GeneralAudioManager : MonoBehaviour {

	//Simply send message to main camera (PlaySound, AudioClip)
	public AudioClip death;
	public AudioClip respawnSFX;
	bool firstWallDeath = true;
	public AudioClip wallDeathDialogue;

	void PlaySound (AudioClip SFX) {
		AudioSource.PlayClipAtPoint (SFX, this.transform.position);
	}

	void Death(int wallTrigger){ //Because I use OnDestroy for sending this message from the firefly I can't send an audio clip (as it would have been destroyed)
		AudioSource.PlayClipAtPoint (death, this.transform.position);

		if (wallTrigger == 1 && firstWallDeath && Camera.main.transform.position.x > -85){
			Camera.main.BroadcastMessage ("PlayVoice", wallDeathDialogue);
			firstWallDeath = false;
		}
	}

	void Respawn(){ //Because the cam instantly moves when respawning and the sound is played where the cam was
		Invoke ("PlayRespawn", 0.1f);
	}

	void PlayRespawn(){
		AudioSource.PlayClipAtPoint (respawnSFX, this.transform.position);
	}
}
