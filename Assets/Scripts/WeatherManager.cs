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

	private Lightning			m_Lightning;


	void Awake() {
		m_Lightning = gameObject.GetComponentInChildren<Lightning>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReduceRain() {
		GameObject r;
		for (int i = 0; i < rain.Count; ++i) {
			r = rain[i];
			if (r.activeSelf) {
				r.SetActive(false);
				break;
			} 
		}
	}

	public void IncreaseThunderDelay(float numSeconds) {
		float delay = m_Lightning.GetThunderDelay() + numSeconds;
		m_Lightning.SetThunderDelay(delay);
	}

	public void ReduceLightningFrequency(float numSeconds) {
		if (numSeconds <= 0.0f)
			return;

		m_Lightning.minSecondsBetweenFlash += numSeconds;
		m_Lightning.maxSecondsBetweenFlash += numSeconds;
	}
}
