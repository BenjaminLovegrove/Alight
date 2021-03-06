﻿using UnityEngine;
using System.Collections;

public class WeatherTrigger : MonoBehaviour {

	public 	bool			reduceRain 		= false;
	public 	bool			increaseRain	= false;
	public 	float			thunderDelay	= 0.0f;
	public 	float			lightningFreq	= 0.0f;
	public	bool			finalTrigger 	= false;
	
	private WeatherManager	m_WeatherManager;

	void Awake() {
		m_WeatherManager 	= WeatherManager.instance;
	}

	void OnTriggerEnter(Collider col) {
		FireflyMovement check = col.GetComponent<FireflyMovement>();
		if (check != null) {
			if (reduceRain)
				m_WeatherManager.ReduceRain();
			if (increaseRain)
				m_WeatherManager.IncreaseRain();
			if (thunderDelay > 0.0f)
				m_WeatherManager.IncreaseThunderDelay(thunderDelay);
			if (thunderDelay < 0.0f)
				m_WeatherManager.ReduceThunderDelay(-thunderDelay);
			if (lightningFreq > 0.0f)
				m_WeatherManager.IncreaseLightningFrequency(lightningFreq);
			if (lightningFreq < 0.0f)
				m_WeatherManager.ReduceLightningFrequency(-lightningFreq);

			gameObject.SetActive(false);
		}

		if (finalTrigger){
			Camera.main.SendMessage ("EndMusic");
			GameObject GO = GameObject.FindGameObjectWithTag("Lightning");
			if (GO != null){
				GO.SetActive(false);
			}
		}
	}

}
