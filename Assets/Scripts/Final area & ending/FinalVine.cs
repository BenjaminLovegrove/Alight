using UnityEngine;
using System.Collections;

public class FinalVine : MonoBehaviour {

	public GameObject[] lanterns;
	public Transform completePos;
	Vector3 startPos;
	Vector3 endPos;

	int currentLantern = 1;

	public AudioClip chime1;
	public AudioClip chime2;
	public AudioClip chime3;
	public AudioClip chime4;
	public AudioClip grow;

	bool end = false;

	//Chime sound here
	//Fail sound here
	
	void Start () {
		lanterns = GameObject.FindGameObjectsWithTag ("finalLantern");
		completePos = transform.Find ("completePos");
		startPos = transform.position;
	}

	void Update(){
		if (currentLantern >= 5 && end == false) {
			Invoke ("vineClear", 1.5f);
			end = true;
		}
	}

	void LanternOn (int lanternNo) {
		switch (lanternNo){
		case 1:
			AudioSource.PlayClipAtPoint(chime1, Camera.main.transform.position);
			break;
		case 2:
			AudioSource.PlayClipAtPoint(chime2, Camera.main.transform.position);
			break;
		case 3:
			AudioSource.PlayClipAtPoint(chime3, Camera.main.transform.position);
			break;
		case 4:
			AudioSource.PlayClipAtPoint(chime4, Camera.main.transform.position);
			break;
		}

		if (lanternNo == currentLantern) {
			currentLantern ++;
		} else {
			foreach (GameObject lantern in lanterns){
				lantern.SendMessage("Incorrect");
				//Play incorrect sound (maybe);
				currentLantern = 1;
			}
		}
	}

	void vineClear(){
		AudioSource.PlayClipAtPoint(grow, Camera.main.transform.position);
		transform.position = Vector3.Lerp(startPos, completePos.position, .4f);
	}
}
