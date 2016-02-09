using UnityEngine;
using System.Collections;

public class EndingStars : MonoBehaviour {

	public AudioClip starSound;
	public GameObject nextStar;
	Vector3 nextStarPos;
	float randomx;
	float randomy;
	float rngTimer;

	bool triggered = false;



	void Start () {
		rngTimer = Random.Range (1f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		rngTimer -= Time.deltaTime;

		if (rngTimer <= 0 && triggered == false){

			randomx = Random.Range(transform.position.x - 8, transform.position.x + 8);
			randomy = Random.Range(transform.position.y - 8, transform.position.y + 8);

			if (this.gameObject.tag == "Star1"){
				nextStarPos = new Vector3 (randomx, randomy, 5);
				AudioSource.PlayClipAtPoint (starSound, Camera.main.transform.position, 0.6f);
			} else if (this.gameObject.tag == "Star2"){
				nextStarPos = new Vector3 (randomx, randomy, 8);
				AudioSource.PlayClipAtPoint (starSound, Camera.main.transform.position, 0.3f);
			}

			Instantiate (nextStar, nextStarPos, Quaternion.identity);

			triggered = true;
		}
	}
}
