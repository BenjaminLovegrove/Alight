using UnityEngine;
using System.Collections;

public class GetMousePosition : MonoBehaviour {

	void Start() {
  
	}
	
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs (Camera.main.transform.position.z)));
	}
}
