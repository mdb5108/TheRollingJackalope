using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
    public GameObject playerObject;
    public GameObject cameraObject;
    public GameObject foxPrefab;
    public GameObject birdPrefab;
    Player player;
    private CameraController cameraController;
    private GameController gameController;
    private int fullScore;
    private int currentLevel;
    // Magic Numbers.
    private float[] milestones = {0f, -25f, -90f, -200f};
    private NpcSpawner npcSpawner;
    private BoidsController boidsController;

	// Use this for initialization
	void Start () {
        player = playerObject.GetComponent<Player>();	
        cameraController = cameraObject.GetComponent<CameraController>();
		gameController = cameraObject.GetComponent<GameController> ();
        fullScore = 1;
        currentLevel = 1;
        npcSpawner = new NpcSpawner();
        boidsController = new BoidsController();

        boidsController.AddCharacter(player);

        List<GameObject> npcPrefabs = new List<GameObject>();
        npcPrefabs.Add(foxPrefab);
        npcPrefabs.Add(birdPrefab);
        npcSpawner.SetNpcPrefabs(npcPrefabs);
        // Spawn for the first level.
        boidsController.AddCharacters(npcSpawner.SpawnNpc(15, new Vector2(0f, 0f), new Vector2(40f, 40f)));
        /*List<Character> npcs = npcSpawner.SpawnNpc(15, new Vector2(0f, 0f), new Vector2(40f, 40f));
        foreach (Character npc in npcs) {
            boidsController.AddCharacter(npc);
        }
        */
        boidsController.Start();
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
            // Spawn for the next level
            
        }
        if (player.GetComponent<Transform>().position.y < milestones[currentLevel] && cameraController.GetIsCrossing() == true) {
            // Go to next level.
            gameController.SetScore(0);
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("Bridge" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = true;
                river.GetComponent<Collider2D>().enabled = true;
            }

            GameObject playgound = GameObject.Find("Playground"+(currentLevel + 1));
            cameraObject.GetComponent<CameraController>().borders = playgound.GetComponent<Transform>().FindChild("Borders");
            cameraController.SetIsCrossing(false);
            cameraController.SetOffset((Vector2)playgound.GetComponent<Transform>().position);
            cameraController.StartZoom();
            currentLevel ++;
        }
        boidsController.FixedUpdate();
    }
}
