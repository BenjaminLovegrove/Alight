using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {


	public float	minSecondsBetweenFlash;
	public float	maxSecondsBetweenFlash;
	public float	flashSpeed;
	public float	maxIntensity = 5f;
	public float	timeBetweenDoubleFlashMin = 0.1f;
	public float	timeBetweenDoubleFlashMax = 0.2f;

	private float	m_Timer;
	private Light	m_Lightning;


	void Awake() {
		m_Lightning = GetComponent<Light>();
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
		m_Lightning.enabled = true;

		float timeBetweenDoubleFlash = Random.Range(timeBetweenDoubleFlashMin, timeBetweenDoubleFlashMax);
		float t = 0.0f;
		for (float timer = 0.0f; timer < flashSpeed; timer += Time.deltaTime) {
			m_Lightning.intensity = Mathf.Lerp (maxIntensity, 0.0f, t);

			t += Time.deltaTime * flashSpeed * 3.0f;

			yield return 0;

			if (timer >= timeBetweenDoubleFlash)
				break;
		}
		t = 0.0f;
		for (float timer = 0.0f; timer < flashSpeed; timer += Time.deltaTime) {
			m_Lightning.intensity = Mathf.Lerp (maxIntensity, 0.0f, t);
			
			t += Time.deltaTime * flashSpeed * 3.0f;
			
			yield return 0;
		}

		m_Lightning.intensity = maxIntensity;
		m_Lightning.enabled = false;
	}

	private void _ResetTimer() {
		m_Timer = Random.Range(minSecondsBetweenFlash, maxSecondsBetweenFlash);
	}
}
