using UnityEngine;
using System.Collections;

public class Cactus : MonoBehaviour {
	
	public Sprite[] cactusSprites;
	
	private SpriteRenderer spriteRenderer;
	
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();

		spriteRenderer.sprite = cactusSprites[Random.Range (0,cactusSprites.Length)];
	}

}