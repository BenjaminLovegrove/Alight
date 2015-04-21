using UnityEngine;
using System.Collections;

public class MenuGUIScript : MonoBehaviour {

	public Texture2D menuDrop;
	public Texture2D menuLogo;
	public Texture2D companyLogo;
	public GUIStyle menuText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MenuController ();
	}

	void MenuController(){
		if (Input.GetKeyDown ("joystick button 7") || Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel (1);
		}
	}

	void OnGUI(){

		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), menuDrop, ScaleMode.StretchToFill);

		Vector2 logoSize = new Vector2 ((menuLogo.width / 1.5f), (menuLogo.height / 1.5f));
		GUI.DrawTexture (new Rect ((Screen.width / 2) - (logoSize.x / 2), 100, logoSize.x, logoSize.y), menuLogo);

		GUI.DrawTexture (new Rect (Screen.width - companyLogo.width - 10, 10, companyLogo.width, companyLogo.height), companyLogo);

		GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 1.3f, 200, 200), "Begin (Start)", menuText);
		GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 1.15f, 200, 200), "Resume (Select)", menuText);
		GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 1.3f, 200, 200), "Resume (Unpause)", menuText);
	}
}
