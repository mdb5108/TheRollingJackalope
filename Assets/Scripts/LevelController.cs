using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
    public GameObject playerObject;
    public GameObject cameraObject;
    Player player;
    private CameraController cameraController;
    private GameController gameController;
    private int fullScore;
    private int currentLevel;
    // No Magic Numbers.
	private float[] milestones = new float[4];

	// Use this for initialization
	void Start () {
        player = playerObject.GetComponent<Player>();	
        cameraController = cameraObject.GetComponent<CameraController>();
		gameController = cameraObject.GetComponent<GameController> ();
        fullScore = 1;
        currentLevel = 1;

		// Get the milestones.
		for (int i = 1; i <= 3; ++ i) {
			GameObject[] bridges = GameObject.FindGameObjectsWithTag("Bridge" + i);
			float minY = float.MaxValue;
			foreach (GameObject bridge in bridges) {
				if (bridge.GetComponent<Transform>().position.y < minY) {
					minY = bridge.GetComponent<Transform>().position.y;
					//Debug.Log (i + " " + bridge.name + " " + minY);
				}
			}
			milestones[i] = minY - 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        if (gameController.GetScore() >= fullScore && cameraController.GetIsCrossing() == false) {
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("Bridge" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = false;
                river.GetComponent<Collider2D>().enabled = false;
            }
            cameraController.SetIsCrossing(true);
        }


        if (player.GetComponent<Transform>().position.y < milestones[currentLevel] && cameraController.GetIsCrossing() == true) {
            gameController.SetScore(0);
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("Bridge" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = true;
                river.GetComponent<Collider2D>().enabled = true;
            }

            GameObject playgound = GameObject.Find("Playground"+(currentLevel + 1));
            cameraObject.GetComponent<CameraController>().borders = playgound.GetComponent<Transform>().FindChild("Borders");      
            cameraController.SetIsCrossing(false);
            cameraController.StartZoom();
            currentLevel ++;
			gameController.ChangeLevel (currentLevel);
        }
    }
}
