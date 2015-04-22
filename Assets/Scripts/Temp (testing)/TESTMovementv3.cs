using UnityEngine;
using System.Collections;

public class TESTMovementv3 : MonoBehaviour {

	//Place this script on fireflies and on the swarm point. So the fireflies move towards where you press and then the point to swarm around is still at the center of them.
	//Doing this also means that they tighten their formation the further they move. (since they are heading towards a single mouse point and not swarming around a point.

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
