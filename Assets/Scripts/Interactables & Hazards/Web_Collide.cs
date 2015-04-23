using UnityEngine;
using System.Collections;

public class Web_Collide : MonoBehaviour {

	public GameObject Fire1;
	public GameObject Fire2;
	public GameObject Fire3;
	public GameObject Log;

	// Use this for initialization
	void Start () {
		Fire1.SetActive(false);
		Fire2.SetActive(false);
		Fire3.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "FireFly"){
			Destroy(collision.gameObject);
			StartCoroutine(BurnMe(2.0F));
		}
	}
	IEnumerator BurnMe(float waitTime) {
		Fire1.SetActive(true);
		Fire2.SetActive(true);
		Fire3.SetActive(true);
		yield return new WaitForSeconds(waitTime);
		Destroy(this.gameObject);
		if (Log != null) {
			Log.rigidbody.isKinematic = false;
			Log.rigidbody.useGravity = true;
		}
	}
}
