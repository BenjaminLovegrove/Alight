using UnityEngine;
using System.Collections;

public class GetMousePosition : MonoBehaviour {

	private Vector3 targetPos;
	
	void Start() {
		targetPos = transform.position;    
	}
	
	void Update () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
		targetPos = Camera.main.ScreenToWorldPoint(targetPos);
		
		transform.position = Vector3.MoveTowards (transform.position, targetPos, 1);
	}
}
