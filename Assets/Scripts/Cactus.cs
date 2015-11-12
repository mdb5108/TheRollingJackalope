using UnityEngine;
using System.Collections;

public class Cactus : MonoBehaviour {
	
	public Sprite[] cactusSprites;
	
	private SpriteRenderer spriteRenderer;

    public float health;
    private float healthMax;

    public float scaleMin;
    private float scaleMax;

    private static readonly float STATIC_HEALTH_DECREMENT = 1;


	
	void Start()
	{
        healthMax = health;
        scaleMax = transform.localScale.x;
		spriteRenderer = GetComponent<SpriteRenderer> ();

		spriteRenderer.sprite = cactusSprites[Random.Range (0,cactusSprites.Length)];
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            health -= STATIC_HEALTH_DECREMENT;

            var perc = health/healthMax;
            var scale = (scaleMax - scaleMin)*perc;
            transform.localScale = new Vector3(scale, scale, scale);

            if(health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
