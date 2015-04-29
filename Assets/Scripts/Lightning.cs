using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {


	public float	minSecondsBetweenFlash;
	public float	maxSecondsBetweenFlash;
	public float	flashSpeed;

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

		float t = 0.0f;
		for (float timer = 0.0f; timer < flashSpeed; timer += Time.deltaTime) {
			m_Lightning.intensity = Mathf.Lerp (1.0f, 0.0f, t);

			t += Time.deltaTime * flashSpeed * 3.0f;

			yield return 0;
		}

		m_Lightning.intensity = 1.0f;
		m_Lightning.enabled = false;
	}

	private void _ResetTimer() {
		m_Timer = Random.Range(minSecondsBetweenFlash, maxSecondsBetweenFlash);
	}
}
