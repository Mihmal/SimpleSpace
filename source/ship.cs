using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour{
	public float drawMass;
	public float fuelMass;
	public float maxFuelMass;
	public float fuelConsumption;
	public float maxFuelConsumption;
	public float fuelSpeed;
	public FLyObject flyObject;

	public float angle;
	public Vector2 velocity;

	private void CalcThrust () {
		float additionalVelocity = fuelConsumption * fuelSpeed * Time.deltaTime / (drawMass+fuelMass);
		velocity.x += additionalVelocity * Mathf.Cos (angle);
		velocity.y += additionalVelocity * Mathf.Sin (angle);
		fuelMass -= fuelConsumption;
	}
	
	void FixedUpdate () {
		flyObject.mass = drawMass + fuelMass;
		CalcThrust ();
		transform.Translate (Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0f);
	}
}
