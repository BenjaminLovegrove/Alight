using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmingLO : MonoBehaviour {
	//	Enum bit flag.
	[System.Flags]
	public enum Rules {
		NONE 			= 1 << 0,
		MATCH_VELOCITY	= 1 << 1,
		MATCH_HEADING	= 1 << 2,
		FOLLOW_MOUSE	= 1 << 3
	}

	public 	float 				speed;
	public	float				nearVelRange;
	public	float				nearPosRange;
	public  float				averageScalerVel;
	public 	float				averageScalerPos;
	public  float				scalerMouse;
	public 	float				mouseRadius;
	public	GameObject			boidPreFab;
	public	Rules				rules;

	private List<BoidLO>		m_Boids;
	private Transform			m_Transform;

	void Awake() {
		m_Boids 			= new List<BoidLO>();
		m_Transform 		= GetComponent<Transform>();
	}

	void Start()
	{
		for (int i = 0; i < 15; ++i)
		{
			GameObject GO = (GameObject)Instantiate(boidPreFab, new Vector3(Random.Range(0.0f, 40.0f), Random.Range(0.0f, 40.0f), 0.0f),Quaternion.identity);

			GO.transform.parent = m_Transform;

			BoidLO b = GO.AddComponent<BoidLO>();

			//	Randomize starting velocity
			b.velocity = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f);
			b.velocity.Normalize();
			b.velocity *= speed;

			m_Boids.Add(b);
		}

		rules = Rules.MATCH_HEADING;
	}

	void Update()
	{
		//	Loop through each boid
		int averageVelCount = 0;
		int averagePosCount = 0;
		float scalerVel = averageScalerVel * Time.deltaTime;
		float scalerPos = averageScalerPos * Time.deltaTime;
		//	For each boid.
		for (int i = 0; i < m_Boids.Count; ++i)
		{
			Vector3 averageVel = new Vector3(0.0f, 0.0f, 0.0f);
			Vector3 averagePos = new Vector3(0.0f, 0.0f, 0.0f);

			averageVelCount = 0;
			averagePosCount = 0;

			//	Loop through every boid
			for (int j = 0; j < m_Boids.Count; ++j)
			{
				//	If the boid isn't comparing agains itself.
				if (i != j)
				{
					//	Get vector between the two boids and its normalized vector
					Vector3 delta 			= m_Boids[j].transform.position - m_Boids[i].transform.position;
					Vector3 deltaNormalized = delta.normalized;

					//	If the compared boid is within the velocity near range
					if (delta.magnitude < nearVelRange)
					{
						//	Add that boids velocity to the average velocity vector.
						averageVel += m_Boids[j].velocity;
						averageVelCount++;
					}
					//	If the compared boid is within the position near range
					if (delta.magnitude < nearPosRange)
					{
						//	Add that boids velocity to the average position vector.
						averagePos += m_Boids[j].transform.position;
						averagePosCount++;
					}
				}
			}
			//	Are there any boids inside the velocity range
			if ((rules & Rules.MATCH_VELOCITY) == Rules.MATCH_VELOCITY && averageVelCount > 0)
			{
				averageVel /= averageVelCount;
				m_Boids[i].velocity = m_Boids[i].velocity * (1.0f-scalerVel) + averageVel * scalerVel;
			}
			//	Are there any boids inside the position range
			if ((rules & Rules.MATCH_HEADING) == Rules.MATCH_HEADING && averagePosCount > 0)
			{
				averagePos /= averagePosCount;
				m_Boids[i].velocity = m_Boids[i].velocity * (1.0f-scalerPos) + averagePos * scalerPos;
			}
			if ((rules & Rules.FOLLOW_MOUSE) == Rules.FOLLOW_MOUSE)
			{
				Vector3 mouseP 				= Input.mousePosition;
				mouseP.z 					= Camera.main.nearClipPlane;
				Vector3 mouseWorldPos		= Camera.main.ScreenToWorldPoint(mouseP);
				mouseWorldPos.z 			= 0;
				Vector3 mouseDelta 			= mouseWorldPos - m_Boids[i].transform.position;
				Vector3 mouseDeltaNormal 	= mouseDelta.normalized;

				if (mouseDelta.magnitude >= mouseRadius)
				{
					m_Boids[i].velocity += mouseDeltaNormal * scalerMouse * Time.deltaTime;
				}
				else if (Vector3.Dot(mouseDeltaNormal, m_Boids[i].velocity.normalized) > 0.5f)
				{ 
					m_Boids[i].velocity -= mouseDeltaNormal * scalerMouse * Time.deltaTime * (mouseRadius - mouseDelta.magnitude);
				}
			}

			m_Boids[i].transform.position = m_Boids[i].transform.position + m_Boids[i].velocity * Time.deltaTime;

		}
		
//		//	Loop through each boid
//		for (int i = 0; i < m_Boids.Count; ++i) {
//			//	Init average variables and comparer counter
//			int count = 0;
//			Vector3 averageVel = new Vector3(0.0f, 0.0f, 0.0f);
//			Vector3 averagePos = new Vector3(0.0f, 0.0f, 0.0f);
//			
//			//	Loop through each boid again to compare
//			for (int j = 0; j < m_Boids.Count; ++j) {
//				//	Ignore results is comparing with self
//				if (i != j) {
//					//	Get vector of distance between the two boids
//					Vector3 delta = m_Boids[j].transform.position - m_Boids[i].transform.position;
//					
//					//	If it is within the range we care about
//					if (delta.magnitude < nearRange) {
//						
//						//if (Vector3.Dot(delta.normalized, m_Boids[i].velocity.normalized) > 0.5f)
//							count++;
//						
//						averageVel += m_Boids[j].velocity;
//						averagePos += m_Boids[j].transform.position;
////						if (delta.magnitude < 50) {
////							m_Boids[i].transform.position -= delta.normalized * Time.deltaTime * (800 / (delta.magnitude + 1));
////						}
//					}
//				}
//				
//				//	Apply average velocity to boid.
//				if (count > 0) {
//					averageVel /= (float)count;
//					averagePos /= (float)count;
//					
//					//m_Boids[i].velocity += averageVel * Time.deltaTime * 20;
//					m_Boids[i].velocity  = (m_Boids[i].velocity * 0.99f) + (averageVel * 0.01f);
//					//m_Boids[i].velocity += (averagePos-m_Boids[i].transform.position).normalized * Time.deltaTime * 20;
//				}
//				if (m_Boids[i].velocity.magnitude > speed) {
//					m_Boids[i].velocity = m_Boids[i].velocity.normalized * speed;
//				}
//				
//				//m_Boids[i].velocity = m_Boids[i].velocity * Time.deltaTime;
//				
//				Rigidbody rb = m_Boids[i].GetComponent<Rigidbody>();
//				rb.position = m_Boids[i].velocity * Time.deltaTime;
//				
////				if(rb.position.x<0)
////					rb.position = new Vector3(rb.position.x + 1024, rb.position.y, rb.position.z);
////				if(rb.position.x>1023)
////					rb.position = new Vector3(rb.position.x - 1024, rb.position.y, rb.position.z);
////				if(rb.position.y<0)
////					rb.position = new Vector3(rb.position.x, rb.position.y + 768, rb.position.z);
////				if(rb.position.y>767)
////					rb.position = new Vector3(rb.position.x, rb.position.y - 768, rb.position.z);
//			}
//		}
//
	}
}
