using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmingLO : MonoBehaviour {

	private class Boid {
		public 	Vector3 velocity;
		public 	Vector3	posistion;
	}

	public 	float 		speed 		= 10.0f;
	public	float		nearRange	= 10.0f;

	private List<Boid>	boids;
	private Transform	tr;

	void Awake() {
		boids = new List<Boid>();
		tr = GetComponent<Transform>();
	}

	void Start() {
		for (int i = 0; i < 15; ++i) {
			Boid b = new Boid();

			//	Randomize starting position
			b.posistion.x = Random.Range(0.0f, 40.0f);
			b.posistion.y = Random.Range(0.0f, 40.0f);
			b.posistion.z = Random.Range(0.0f, 40.0f);

			//	Randomize starting velocity
			b.velocity = new Vector3(Random.Range(-1.0f, 1.0f),
			                         Random.Range(-1.0f, 1.0f),
			                         Random.Range(-1.0f, 1.0f));
			b.velocity.Normalize();
			b.velocity *= speed;

			boids.Add(b);
		}
	}

	void Update() {
		//	Loop through each boid
		for (int i = 0; i < boids.Count; ++i) {
			//	Init average variables and comparer counter
			int count = 0;
			Vector3 averageVel = new Vector3(0.0f, 0.0f, 0.0f);
			Vector3 averagePos = new Vector3(0.0f, 0.0f, 0.0f);

			//	Loop through each boid again to compare
			for (int j = 0; j < boids.Count; ++j) {
				//	Ignore results is comparing with self
				if (i != j) {
					//	Get vector of distance between the two boids
					Vector3 delta = boids[j].posistion - boids[i].posistion;

					//	If it is within the range we care about
					if (delta.magnitude < nearRange) {

						if (Vector3.Dot(delta.normalized, boids[i].velocity.normalized) > 0.5f) {
							count++;

							averageVel += boids[j].velocity;
							averagePos += boids[j].posistion;
						}
					}
				}

				//	Apply average velocity to boid.
				if (count > 0) {
					averageVel /= count;
					averagePos /= count;

					boids[i].velocity  = (boids[i].velocity * 0.99f) + (averageVel * 0.01f);
					boids[i].velocity += (averagePos-boids[i].posistion).normalized * Time.deltaTime * 20;
				}
				if (boids[i].velocity.magnitude > speed) {
					boids[i].velocity = boids[i].velocity.normalized * speed;
				}
				//TODO
			}
		}
	}
}
