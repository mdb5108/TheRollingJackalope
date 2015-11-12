using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GUIText scoreText;
	public GameObject endGame;
	public GameObject player;

	private int score;
	private int scoreThreshold;
	private int currentLevel;
	private bool won = false;
	
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

		if(currentLevel == 3 && score == 1 && !won)
		{
			won = true;
			Instantiate(endGame,(Vector2)Camera.current.transform.position,Quaternion.identity);
			player.GetComponent<Rigidbody2D>().isKinematic = true;
			Camera.current.GetComponent<CameraController>().enabled = false;
			player.GetComponent<Player>().respondToInput = false;
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

	public void ChangeLevel(int i_level)
	{
		currentLevel = i_level;
	}
}
