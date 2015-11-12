using UnityEngine;
using System.Collections;

public class FriendInBubble : MonoBehaviour {

	private GameObject bubble;
	private float radius;
	private Vector2 velocity;
	private float[] randomDir;
	private float randomSpeed;

	void Start()
	{
		randomDir = new float[]{1.0f,-1.0f,0.5f,-0.5f,2.0f,-2.0f};
		randomSpeed = Random.Range (0.01f, 0.05f);
		bubble = GameObject.FindGameObjectWithTag ("Bubble");
		velocity = new Vector2(randomSpeed*randomDir[Random.Range (0, randomDir.Length)],randomSpeed*randomDir[Random.Range (0, randomDir.Length)]);
		transform.localEulerAngles = new Vector3(0.0f,0.0f,Random.Range (0,360));
	}

	void Update ()
	{
		radius = bubble.transform.localScale.x - 0.012f;

		transform.localPosition = new Vector2(transform.localPosition.x+velocity.x,transform.localPosition.y+velocity.y);
		if(transform.localPosition.x >= radius || transform.localPosition.x <= -radius)
		{
			velocity.x *= -1;
			randomSpeed = Random.Range (0.01f, 0.05f);
			//velocity.y = randomSpeed*randomDir[Random.Range (0, 2)];
		}
		if(transform.localPosition.y >= radius || transform.localPosition.y <= -radius )
		{
			velocity.y *= -1;
			randomSpeed = Random.Range (0.01f, 0.05f);
			//velocity.x = randomSpeed*randomDir[Random.Range (0, 2)];
		}
	}
}