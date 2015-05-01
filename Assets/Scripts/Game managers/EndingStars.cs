using UnityEngine;
using System.Collections;

public class EndingStars : MonoBehaviour {

	public AudioClip starSound;
	public GameObject nextStar;
	Vector3 nextStarPos;
	float randomx;
	float randomy;
	float rngTimer;



	void Start () {
		rngTimer = Random.Range (2f, 7f);
	}
	
	// Update is called once per frame
	void Update () {
		rngTimer -= Time.deltaTime;

		if (rngTimer <= 0){

			randomx = Random.Range(transform.position.x - 20, transform.position.x + 20);
			randomy = Random.Range(transform.position.y - 20, transform.position.y + 20);

			if (this.gameObject.tag == "Star1"){
				nextStarPos = new Vector3 (randomx, randomy, 5);
				AudioSource.PlayClipAtPoint (starSound, Camera.main.transform.position, 0.6f);
			} else if (this.gameObject.tag == "Star2"){
				nextStarPos = new Vector3 (randomx * 0.7f, randomy * 0.7f, 8);
				AudioSource.PlayClipAtPoint (starSound, Camera.main.transform.position, 0.3f);
			}

			Instantiate (nextStar, nextStarPos, Quaternion.identity);
		}
	}
}
