using UnityEngine;
using System.Collections;

public class TESTMovementv2 : MonoBehaviour {

	public Rigidbody rb;
	Vector3 worldMousePoint;
	Vector3 dir;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void Update () {
		worldMousePoint = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30));
		dir = worldMousePoint - transform.position;

		if (Input.GetMouseButton (0)) {
			rb.AddForce (dir.normalized * Time.deltaTime * 25);
		}
	}
}
