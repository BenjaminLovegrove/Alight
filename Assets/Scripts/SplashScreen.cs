using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

	GameObject fadeOutObj;
	Image fadeOutImg;

	// Use this for initialization
	void Start () {
		fadeOutObj = GameObject.Find ("Image");
		fadeOutImg = fadeOutObj.GetComponent<Image>();
		Invoke ("Fade", 3f);
	}
	

	void Fade () {
		fadeOutObj.SetActive(true);
		fadeOutImg.CrossFadeAlpha (255, 2f, false);
		Invoke ("Load", 2.5f);
	}

	void Load (){
		Application.LoadLevel ("Menu_Main");
	}


}
