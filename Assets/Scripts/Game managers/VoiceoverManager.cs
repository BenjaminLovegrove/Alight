using UnityEngine;
using System.Collections;

public class VoiceoverManager : MonoBehaviour {

	//Simply send message to main camera (PlayVoice, AudioClip)

	void PlayVoice (AudioClip SFX) {
		AudioSource.PlayClipAtPoint (SFX, this.transform.position);
	}
}
