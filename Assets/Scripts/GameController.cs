using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText scoreText;
	private int score;
	private int scoreThreshold;
	
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
		scoreText.text = score + "/" + scoreThreshold;
	}

	public void AddScore(int newScore)
	{
		score += newScore;
		UpdateScore();
	}

	public void SetThreshold(int newValue)
	{
		scoreThreshold = newValue;
	}

    public int GetScore() {
        return score;
    }
    public void SetScore(int i_score) {
        score = i_score;
        UpdateScore();
    }
}
