using UnityEngine;
using System.Collections;

public class GetMousePosition : MonoBehaviour {

	//private Vector3 targetPos;
	
	void Start() {
		//targetPos = transform.position;    
	}
	
	void Update () {
		/*float distance = transform.position.z - Camera.main.transform.position.z;
		targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30);
		targetPos = Camera.main.ScreenToWorldPoint(targetPos);
		
		transform.position = Vector3.MoveTowards (transform.position, targetPos, 1);		Not sure if all of this was necessary because the swarms already use force. And it wasn't working so I just made it snap (see below) for now
		*/
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30));
	}
}
