using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		SendMessageUpwards ("WallCollide", false, SendMessageOptions.DontRequireReceiver);
		Camera.main.BroadcastMessage("Death", 1);  //Added 1 to trigger that this is a wall collision for dialogue.
	}
}
