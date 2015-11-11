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
    // Magic Numbers.
    private float[] milestones = {0f, -25f, -90f, -200f};
    

	// Use this for initialization
	void Start () {
        player = playerObject.GetComponent<Player>();	
        cameraController = cameraObject.GetComponent<CameraController>();
		gameController = cameraObject.GetComponent<GameController> ();
        fullScore = 1;
        currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        if (gameController.GetScore() >= fullScore && cameraController.GetIsCrossing() == false) {
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("River" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = false;
                river.GetComponent<Collider2D>().enabled = false;
            }
            cameraController.SetIsCrossing(true);
        }
        if (player.GetComponent<Transform>().position.y < milestones[currentLevel] && cameraController.GetIsCrossing() == true) {
            gameController.SetScore(0);
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("River" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = true;
                river.GetComponent<Collider2D>().enabled = true;
            }

            GameObject playgound1 = GameObject.Find("Playground"+(currentLevel + 1));
            cameraObject.GetComponent<CameraController>().borders = playgound1.GetComponent<Transform>().FindChild("Borders");      
            cameraController.SetIsCrossing(false);
            cameraController.SetOffset((Vector2)playgound1.GetComponent<Transform>().position);
            cameraController.StartZoom();
            currentLevel ++;
        }
    }
}
