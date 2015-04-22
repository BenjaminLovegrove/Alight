using UnityEngine;
using System.Collections;

public class AudioScriptReceiver : MonoBehaviour {
	
	public AudioClip fireflys;
	public AudioClip Alight;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTestSceneStart()
	{
		AudioSource.PlayClipAtPoint (fireflys, this.transform.position);
	}
	
	void OnMainMenuStart()
	{
		AudioSource.PlayClipAtPoint (Alight, this.transform.position);
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}


