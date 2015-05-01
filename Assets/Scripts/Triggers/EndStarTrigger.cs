using UnityEngine;
using System.Collections;

public class EndStarTrigger : MonoBehaviour {


	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag =="FireFly"){
			col.SendMessage("EndingStars");
		}	
	}
}
