using UnityEngine;
using System.Collections;

public class FinalVine : MonoBehaviour {

	public GameObject[] lanterns;
	int currentLantern = 1;
	//Chime sound here
	//Fail sound here
	
	void Start () {
		lanterns = GameObject.FindGameObjectsWithTag ("finalLantern");
	}

	void Update(){
		if (currentLantern >= 5) {
			//Animate branch
			print ("woo");
		}
	}

	void LanternOn (int lanternNo) {
		if (lanternNo == currentLantern) {
			currentLantern ++;
			//audio.pitch (lanternNo / 4);
			//Play chime sound
		} else {
			foreach (GameObject lantern in lanterns){
				lantern.SendMessage("Incorrect");
				//Play incorrect sound;
				currentLantern = 1;
			}
		}
	}
}
