using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

	Vector3 mousePos;


	void Update () {
		Screen.showCursor = false;
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 20));

		transform.position = (mousePos);
	}
}
