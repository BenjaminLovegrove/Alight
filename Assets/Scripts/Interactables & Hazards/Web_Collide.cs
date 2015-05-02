using UnityEngine;
using System.Collections;

public class Web_Collide : MonoBehaviour {

	public GameObject Fire1;
	public GameObject Fire2;
	public GameObject Log;
	public GameObject[] frogsToCrush;

	// Use this for initialization
	void Start () {
		Fire1.SetActive(false);
		Fire2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "FireFly"){
			Camera.main.BroadcastMessage("Death", 2);
			Destroy(collision.gameObject);
			StartCoroutine(BurnMe(2.0F));

			if (frogsToCrush.Length > 0){
				foreach (GameObject frog in frogsToCrush){
					frog.SendMessage ("Crushed", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	IEnumerator BurnMe(float waitTime) {
		if (Fire1 != null){
			Fire1.SetActive(true);
		}
		if (Fire2 != null){
			Fire2.SetActive(true);
		}
	
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
		if (Log != null) {
			Log.rigidbody.isKinematic = false;
			Log.rigidbody.useGravity = true;
		}
	}
}
