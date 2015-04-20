using UnityEngine;
using System.Collections;

public class TempMovement : MonoBehaviour {

	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		//transform.Translate (Input.GetAxis ("Horizontal") * Time.deltaTime, Input.GetAxis ("Vertical") * Time.deltaTime, transform.position.z);

		rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 100, Input.GetAxis("Vertical") * Time.deltaTime * 100, 0));
	}
}
