using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceoverManager : MonoBehaviour {

	public 	float				delayBetweenClipsInSeconds = 1.0f;
	public 	AudioClip			voice_Movement;

	private AudioSource			m_AudioSource;
	private Queue<AudioClip>	m_VoiceClips;
	private bool				m_WasPlaying = false;



	void Awake() {
		m_AudioSource = GetComponent<AudioSource>();
		m_VoiceClips = new Queue<AudioClip>();

		//Play initial movement audio
		m_AudioSource.PlayDelayed (3f);
	}

	void Update() {
		if (!m_AudioSource.isPlaying && m_VoiceClips.Count > 0) {
			m_AudioSource.clip = m_VoiceClips.Dequeue();
			if (m_WasPlaying) {
				m_AudioSource.Play();
			}
			else {
				m_AudioSource.PlayDelayed(delayBetweenClipsInSeconds);
				m_WasPlaying = true;
			}
		}
	}

	public void PlayVoice (AudioClip SFX) {
		m_VoiceClips.Enqueue(SFX);
	}

}
