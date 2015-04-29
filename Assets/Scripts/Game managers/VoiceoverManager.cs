using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceoverManager : MonoBehaviour {

	public 	float				delayBetweenClipsInSeconds = 1.0f;

	private AudioSource			m_AudioSource;
	private Queue<AudioClip>	m_VoiceClips;
	private bool				m_WasPlaying = false;

	void Awake() {
		m_AudioSource = GetComponent<AudioSource>();

		m_VoiceClips = new Queue<AudioClip>();
	}

	void Update() {
		if (!m_AudioSource.isPlaying && m_VoiceClips.Count > 0) {
			m_AudioSource.clip = m_VoiceClips.Dequeue();
			if (m_WasPlaying) {
				m_AudioSource.Play();
			}
			else {
				ulong delay = 44100 * (ulong)(delayBetweenClipsInSeconds);
				m_AudioSource.Play(delay);
				m_WasPlaying = true;
			}
		}
	}

	void PlayVoice (AudioClip SFX) {
		m_VoiceClips.Enqueue(SFX);
	}
}
