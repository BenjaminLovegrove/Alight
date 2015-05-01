using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightning : MonoBehaviour {

	public float			minSecondsBetweenFlash = 2.5f;
	public float			maxSecondsBetweenFlash = 4.0f;
	public float			flashSpeed;
	public float			maxIntensity = 5.0f;
	public float			timeBetweenDoubleFlashMin = 0.1f;
	public float			timeBetweenDoubleFlashMax = 0.2f;
	public List<AudioClip>	thunderAudio;

	private float			m_Timer;
	private float			m_ThunderDelay = 0.5f;
	private Light			m_LightningFlash;
	private AudioSource		m_AudioSource;

	void Awake() {
		m_LightningFlash 	= GetComponent<Light>();
		m_AudioSource 	= GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		_ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		m_Timer -= Time.deltaTime;

		if (m_Timer <= 0.0f) {
			_ResetTimer();

			StartCoroutine(LightningFlash());
		}
	}

	IEnumerator LightningFlash() {
		m_LightningFlash.enabled = true;

		float timeBetweenDoubleFlash = Random.Range(timeBetweenDoubleFlashMin, timeBetweenDoubleFlashMax);
		float t = 0.0f;

		_PlayRandomThunder();

		for (float timer = 0.0f; timer < flashSpeed; timer += Time.deltaTime) {
			m_LightningFlash.intensity = Mathf.Lerp (maxIntensity, 0.0f, t);

			t += Time.deltaTime * flashSpeed * 3.0f;

			yield return 0;

			if (timer >= timeBetweenDoubleFlash)
				break;
		}
		t = 0.0f;
		for (float timer = 0.0f; timer < flashSpeed; timer += Time.deltaTime) {
			m_LightningFlash.intensity = Mathf.Lerp (maxIntensity, 0.0f, t);
			
			t += Time.deltaTime * flashSpeed * 3.0f;
			
			yield return 0;
		}

		m_LightningFlash.intensity = maxIntensity;
		m_LightningFlash.enabled = false;
	}

	public float GetThunderDelay() {
		return m_ThunderDelay;
	}

	public void SetThunderDelay(float delay) {
		m_ThunderDelay = delay;
	}

	private void _PlayRandomThunder() {
		if (thunderAudio.Count == 0 || m_AudioSource.isPlaying)
			return;

		int rand = Random.Range(0, thunderAudio.Count);

		m_AudioSource.clip = thunderAudio[rand];
		m_AudioSource.PlayDelayed(m_ThunderDelay);
	}

	private void _ResetTimer() {
		m_Timer = Random.Range(minSecondsBetweenFlash, maxSecondsBetweenFlash);
	}
}
