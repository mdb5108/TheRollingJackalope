using UnityEngine;
using System.Collections;

public class ShopButton : MonoBehaviour {

	void OnMouseDown()
	{
		Application.LoadLevel ("CharCustomize");
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}