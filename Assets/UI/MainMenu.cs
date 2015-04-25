using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void Begin () {
		Application.LoadLevel ("Scene_MVPMain");
	}
}
