using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText scoreText;
	private int score;
	
	void Start ()
	{
		score = 0;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void UpdateScore ()
	{
		scoreText.text = "" + score;
	}

	public void AddScore(int newScore)
	{
		score += newScore;
		UpdateScore();
	}
}
