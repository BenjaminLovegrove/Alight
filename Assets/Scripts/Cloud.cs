using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public enum Direction {
		LEFT,
		RIGHT
	}

	public 	float		speed;
	public 	Direction	direction = Direction.LEFT;
	public	float		cloudDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.GetComponent<AudioListener>() != null) {
			Debug.Log ("Triggered");
			_TriggerCloudMovement();
		}
	}

	private void _TriggerCloudMovement() {
		WeatherManager wm = WeatherManager.instance;
		if (!wm.cloudsHaveTriggered) {
			wm.cloudsHaveTriggered = true;
			
			foreach (Cloud c in wm.clouds) {
				StartCoroutine(_MoveCloud(c));
				c.GetComponent<BoxCollider>().enabled = false;
			}
		}
	}
	
	IEnumerator _MoveCloud(Cloud c) {
		float timer = 0.0f;
		float x = c.transform.position.x;
		float newX = (c.direction == Cloud.Direction.LEFT) ? x - cloudDistance : x + cloudDistance;
		while (c.transform.position.x != newX) {
			//	Prevent divide by zero errors


			timer += Time.deltaTime * speed;
			
			c.transform.position = new Vector3(Mathf.Lerp(x, newX, timer),
			                                   c.transform.position.y,
			                                   c.transform.position.z);
			
			yield return 0;
		}
	}
}
