using UnityEngine;
using System.Collections;

public class TESTMovementv3 : MonoBehaviour {

	public Rigidbody rb;
	public GameObject FireflyDragger;
	Vector3 dir;
	
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {

		Vector3 FireflyDraggerPos = FireflyDragger.transform.position;
		dir = FireflyDraggerPos - transform.position;
		
		if (Input.GetMouseButton (0)) {
			rb.AddForce (dir.normalized * Time.deltaTime * Random.Range(15,25));
		}
	}
}
