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
		if (fuelMass > 0) {
			float additionalVelocity = fuelConsumption * fuelSpeed * Time.deltaTime / (drawMass + fuelMass);
			velocity.x += additionalVelocity * Mathf.Cos (angle);
			velocity.y += additionalVelocity * Mathf.Sin (angle);
			fuelMass -= fuelConsumption;
		}
	}

	private void proceedInput() {
		float turningPower = Input.GetAxis("Horizontal")/10f;
		angle += turningPower;
		transform.Rotate (0f, 0f, turningPower);
		float enginePower = Input.GetAxis ("Vertical") / 100f;
		fuelConsumption += enginePower;
		if (fuelConsumption < 0) fuelConsumption = 0;
		else if (fuelConsumption > maxFuelConsumption) fuelConsumption = maxFuelConsumption;
	}

	void FixedUpdate () {
		proceedInput ();
		flyObject.mass = drawMass + fuelMass;
		CalcThrust ();
		transform.Translate (Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0f);
	}
}
