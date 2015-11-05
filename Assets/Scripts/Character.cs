#define DEBUG
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Character : MonoBehaviour {
    public Vector2 velocity;
    private float MAX_SPEED = 0.01f;
	// Use this for initialization
	void Start () {
        velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
        velocity += new Vector2 (Random.Range(0f, 0.0001f), Random.Range(0f, 0.0001f));	
        if (this.transform.position.magnitude > 5) {
            velocity += (Vector2.zero - (Vector2)this.transform.position).normalized / 1000f;
        }
        if (velocity.magnitude > MAX_SPEED) {
            velocity = velocity.normalized * MAX_SPEED;
        }
#if DEBUG
#endif    
	}
    
}
