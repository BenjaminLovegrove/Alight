using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	public float timer;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, timer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
