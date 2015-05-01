using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour {

	#region singleton
	private static WeatherManager _instance;

	public static WeatherManager instance
	{
		get
		{
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<WeatherManager>();
			return _instance;
		}
	}
	#endregion

	public 	List<GameObject>	rain;
	public	List<Cloud>			clouds;
	public	AudioSource			rainSound;

	private Lightning			m_Lightning;
	private bool				m_CloudsHaveTriggered;


	void Awake() {
		m_Lightning = gameObject.GetComponentInChildren<Lightning>();
	}

	// Use this for initialization
	void Start () {
		m_CloudsHaveTriggered = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReduceRain() {
		rainSound.volume = rainSound.volume * 0.7f;
		GameObject r;
		for (int i = 0; i < rain.Count; ++i) {
			r = rain[i];
			if (r.activeSelf) {
				r.SetActive(false);

				bool flag = false;
				foreach (GameObject obj in rain) {
					if (obj.activeSelf == true)
						flag = true;
				}

				if (!flag)
					StartCoroutine(FadeOutRainSound());
				break;
			} 
		}
	}

	IEnumerator FadeOutRainSound() {
		float timer = 0.0f;
		while (rainSound.volume > 0.0f) {
			timer += Time.deltaTime * 0.5f;
			Mathf.Lerp(rainSound.volume, 0.0f, timer);

			yield return 0;
		}

		rainSound.Stop();
	}

	public void IncreaseRain() {
		rainSound.volume = rainSound.volume * 1.3f;
		GameObject r;
		for (int i = rain.Count-1; i >= 0; --i) {
			r = rain[i];
			if (!r.activeSelf) {
				r.SetActive(true);
				break;
			}
		}
	}

	public void IncreaseThunderDelay(float numSeconds) {
		float delay = m_Lightning.GetThunderDelay() + numSeconds;
		m_Lightning.SetThunderDelay(delay);
	}

	public void ReduceThunderDelay(float numSeconds) {
		float delay = m_Lightning.GetThunderDelay() - numSeconds;

		if (delay < 0.0f)
			delay = 0.0f;

		m_Lightning.SetThunderDelay(delay);
	}

	public void ReduceLightningFrequency(float numSeconds) {
		if (numSeconds <= 0.0f)
			return;

		m_Lightning.minSecondsBetweenFlash += numSeconds;
		m_Lightning.maxSecondsBetweenFlash += numSeconds;
	}

	public void IncreaseLightningFrequency(float numSeconds) {
		if (numSeconds <= 0.0f)
			return;

		m_Lightning.minSecondsBetweenFlash -= numSeconds;
		m_Lightning.maxSecondsBetweenFlash -= numSeconds;

		if (m_Lightning.minSecondsBetweenFlash < 0.0f)
			m_Lightning.minSecondsBetweenFlash = 0.0f;

		if (m_Lightning.maxSecondsBetweenFlash < 0.0f)
			m_Lightning.maxSecondsBetweenFlash = 0.0f;
	}

	public void TriggerCloudMovement() {
		if (!m_CloudsHaveTriggered) {
			m_CloudsHaveTriggered = true;

			foreach (Cloud c in clouds) {
				c.GetComponent<Rigidbody>().velocity = new Vector3(c.velocity, 0.0f, 0.0f);
			}
		}
	}
}
