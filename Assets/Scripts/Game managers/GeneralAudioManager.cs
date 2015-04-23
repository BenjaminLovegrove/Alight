using UnityEngine;
using System.Collections;

public class GeneralAudioManager : MonoBehaviour {

	//Simply send message to main camera (PlaySound, AudioClip)

	void PlaySound (AudioClip SFX) {
		AudioSource.PlayClipAtPoint (SFX, this.transform.position);
	}
}
