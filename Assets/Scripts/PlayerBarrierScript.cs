using UnityEngine;
using System.Collections;

public class PlayerBarrierScript : MonoBehaviour {
	
	public GameObject cube;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 leftWall = Camera.main.ViewportToWorldPoint (new Vector3(0,0,0));
		Vector3 rightWall = Camera.main.ViewportToWorldPoint (new Vector3(1,0,0));
		Vector3 topWall = Camera.main.ViewportToWorldPoint (new Vector3(0,1,0));
		Vector3 bottomWall = Camera.main.ViewportToWorldPoint (new Vector3(0,0,0));

		if (gameObject.name == "LeftWall"){
			transform.position = new Vector3(leftWall.x + 1, 0, 0);
		} 
		if (gameObject.name == "RightWall"){
			transform.position = new Vector3(rightWall.x - 1, 0, 0);
		}
		if (gameObject.name == "TopWall"){
			transform.position = new Vector3(0, topWall.y - 3, 0);
		}
		if (gameObject.name == "BottomWall"){
			transform.position = new Vector3(0, bottomWall.y + 0.5f, 0);
		}
	}
}
