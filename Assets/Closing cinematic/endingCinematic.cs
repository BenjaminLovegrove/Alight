using UnityEngine;
using System.Collections;

public class endingCinematic : MonoBehaviour {
	
	public GameObject targPos;

	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.MoveTowards (transform.position, targPos.transform.position, 0.05f);
	}
}
