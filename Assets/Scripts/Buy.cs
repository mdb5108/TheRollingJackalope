using UnityEngine;
using System.Collections;

public class Buy : MonoBehaviour {
	
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