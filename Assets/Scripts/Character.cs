#define DEBUG
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Character : MonoBehaviour {
    public Vector2 velocity;
    private float MAX_SPEED = 0.01f;
	private float scale = 0.7f;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	public void Start () {
        velocity = Vector2.zero;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
        velocity += new Vector2 (Random.Range(0f, 0.0001f), Random.Range(0f, 0.0001f));	
        if (this.transform.position.magnitude > 15) {
            velocity += (Vector2.zero - (Vector2)this.transform.position).normalized / 1000f;
        }
        if (velocity.magnitude > MAX_SPEED) {
            velocity = velocity.normalized * MAX_SPEED;
        }

		if(spriteRenderer != null)
		{
			if(velocity.x >=0)
			{
				//transform.localScale = new Vector2(-scale,scale);
				spriteRenderer.transform.localScale = new Vector2(-scale,scale);
			}
			else
			{
				//transform.localScale = new Vector2(scale,scale);
				spriteRenderer.transform.localScale = new Vector2(scale,scale);
			}
		}
#if DEBUG
#endif    
	}
    
}
