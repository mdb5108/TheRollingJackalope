using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	public float gyroForce = 50;
	public GUIText scoreText;
	public int score = 0;
	private GameController gameController;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		gameController = Camera.main.GetComponent<GameController> ();
	}

	void FixedUpdate ()
	{
		//Debug.Log (Input.acceleration.x + "," + Input.acceleration.y);
		myRigidbody.AddForce (new Vector2(Input.acceleration.x*gyroForce, Input.acceleration.y*gyroForce));
		myRigidbody.AddForce (new Vector2(Input.GetAxis("Horizontal")*gyroForce,Input.GetAxis("Vertical")*gyroForce));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Friend")
		{
			gameController.AddScore(1);
			Destroy(other.gameObject);
		}
	}
}