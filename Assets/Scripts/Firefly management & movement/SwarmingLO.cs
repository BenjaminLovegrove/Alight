using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmingLO : MonoBehaviour {

	public 	float 				speed 		= 10.0f;
	public	float				nearRange	= 10.0f;
	public	GameObject			boidPreFab;

	private List<BoidLO>		m_Boids;
	private Transform			m_Transform;

	void Awake() {
		m_Boids 			= new List<BoidLO>();
		m_Transform 		= GetComponent<Transform>();
	}

	void Start() {
		for (int i = 0; i < 15; ++i) {
			GameObject GO = (GameObject)Instantiate(boidPreFab,
			                            			new Vector3(Random.Range(0.0f, 40.0f),
			            										Random.Range(0.0f, 40.0f),
			            										0.0f),
			                            						Quaternion.identity);

			GO.transform.parent = m_Transform;

			BoidLO b = GO.AddComponent<BoidLO>();

			//	Randomize starting velocity
			b.velocity = new Vector3(Random.Range(-1.0f, 1.0f),
			                         Random.Range(-1.0f, 1.0f),
			                         0.0f);
			b.velocity.Normalize();
			b.velocity *= speed;

			m_Boids.Add(b);
		}
	}

	void Update() {
		//	Loop through each boid
		for (int i = 0; i < m_Boids.Count; ++i) {
			//	Init average variables and comparer counter
			int count = 0;
			Vector3 averageVel = new Vector3(0.0f, 0.0f, 0.0f);
			Vector3 averagePos = new Vector3(0.0f, 0.0f, 0.0f);

			//	Loop through each boid again to compare
			for (int j = 0; j < m_Boids.Count; ++j) {
				//	Ignore results is comparing with self
				if (i != j) {
					//	Get vector of distance between the two boids
					//Vector3 delta = m_Boids[j].transform.position - m_Boids[i].transform.position;

					//	If it is within the range we care about
					if (true/*delta.magnitude < nearRange*/) {

						if (true /*Vector3.Dot(delta.normalized, m_Boids[i].velocity.normalized) > 0.5f*/) {
							count++;

							averageVel += m_Boids[j].velocity;
							averagePos += m_Boids[j].transform.position;
						}
					}
				}

				//	Apply average velocity to boid.
				if (count > 0) {
					averageVel /= (float)count;
					averagePos /= (float)count;

					m_Boids[i].velocity += averageVel * Time.deltaTime * 20;
					//m_Boids[i].velocity  = (m_Boids[i].velocity * 0.99f) + (averageVel * 0.01f);
					m_Boids[i].velocity += (averagePos-m_Boids[i].transform.position).normalized * Time.deltaTime * 20;
				}
				if (m_Boids[i].velocity.magnitude > speed) {
					m_Boids[i].velocity = m_Boids[i].velocity.normalized * speed;
				}

				m_Boids[i].GetComponent<Rigidbody>().velocity = m_Boids[i].velocity;
			}
		}
	}
}
