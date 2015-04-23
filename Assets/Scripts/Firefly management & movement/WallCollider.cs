using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		SendMessageUpwards ("WallCollide", SendMessageOptions.DontRequireReceiver);
		Camera.main.BroadcastMessage("Death");
	}
}
