using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	private float gyroForce = 10;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		myRigidbody.AddForce (new Vector2(Input.acceleration.x*gyroForce, Input.acceleration.y*gyroForce));
		Debug.Log (Input.acceleration.x + "," + Input.acceleration.y);
	}
}