using UnityEngine;
using System.Collections;

public class ShopButton : MonoBehaviour {

	private AudioSource audioSource;
	private bool clicked = false;
	
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	void OnMouseDown()
	{
		clicked = true;
		audioSource.Play();
	}

	void Update()
	{
		if(!audioSource.isPlaying && clicked)
		{
			Application.LoadLevel ("CharCustomize");
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}