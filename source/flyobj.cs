using UnityEngine;
using System.Collections;


public class FLyObject : MonoBehaviour {

	public float T = 0.01f;
	public float mass;
	public Vector2 velocity;
	public GameObject[] others;
	public FLyObject[] othersScripts;
			
	private float fx (float localX) {
		float a = 0;
		for (int i=0; i<others.Length; i++) {
			float r = new Vector2(others[i].transform.position.x-localX, others[i].transform.position.y-transform.position.y).magnitude;
			a += othersScripts[i].mass*(others[i].transform.position.x - transform.position.x)/Mathf.Pow(r, 3);
		}
		return a;
	}
	
	float fy (float localY) {
		float a = 0;
		for (int i=0; i<others.Length; i++) { 
			float r = new Vector2(others[i].transform.position.y-localY, others[i].transform.position.x-transform.position.x).magnitude;
			a += othersScripts[i].mass*(others[i].transform.position.y - transform.position.y)/Mathf.Pow(r, 3);
		}
		return a;
	}
	
	void CalcX () {
		float k1 = T*fx(transform.position.x);
		float q1 = T*velocity.x;

		float k2 = T*fx(transform.position.x + q1/2);
		float q2 = T*(velocity.x + k1/2);
		
		float k3 = T*fx(transform.position.x + q2/2);
		float q3 = T*(velocity.x + k2/2);
		
		float k4 = T*fx(transform.position.x + q3);
		float q4 = T*(velocity.x + k3);
		
		velocity.x += Time.deltaTime*(k1 + 2 * k2 + 2 * k3 + k4) / 6;
		transform.Translate ((Time.deltaTime * (q1 + 2 * q2 + 2 * q3 + q4) / 6), 0f, 0f);
	}
	
	void CalcY () {
		float k1 = T*fy(transform.position.y);
		float q1 = T*velocity.y;
		
		float k2 = T*fy(transform.position.y + q1/2);
		float q2 = T*(velocity.y + k1/2);
		
		float k3 = T*fy(transform.position.y + q2/2);
		float q3 = T*(velocity.y + k2/2);
		
		float k4 = T*fy(transform.position.y + q3);
		float q4 = T*(velocity.y + k3);
		
		velocity.y += Time.deltaTime*(k1 + 2 * k2 + 2 * k3 + k4) / 6;
		transform.Translate (0f, (Time.deltaTime * (q1 + 2 * q2 + 2 * q3 + q4) / 6),  0f);
	}
	
	void FixedUpdate () {
		CalcX();
		CalcY();
		transform.Translate(Time.deltaTime*velocity.x, Time.deltaTime*velocity.y, 0f);
	}
}
