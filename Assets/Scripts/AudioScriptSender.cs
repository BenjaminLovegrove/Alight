using UnityEngine;
using System.Collections;

public class AudioScriptSender : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		//When main menu loads
		Camera.main.SendMessage("OnMainMenuStart", SendMessageOptions.DontRequireReceiver); 
		
		//When test scene loads
		Camera.main.SendMessage("OnTestSceneStart", SendMessageOptions.DontRequireReceiver); 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


