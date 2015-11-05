using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	public float gyroForce = 50;
	public GUIText scoreText;
	public int score = 0;
	private GameController gameController;

    private static readonly float MAX_SPEED = 20;

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

        //Clamp player speed so bouncing doesn't get out of control
        if(myRigidbody.velocity.SqrMagnitude() > MAX_SPEED*MAX_SPEED)
            myRigidbody.velocity = myRigidbody.velocity.normalized*MAX_SPEED;
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
