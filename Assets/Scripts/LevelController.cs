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
    private int maxLevel;
    private int currentLevel;
    // No Magic Numbers.
	private float[] milestones = new float[4];
    // Magic Numbers.
    //private float[] milestones = {0f, -25f, -90f, -200f};
    private NpcSpawner npcSpawner;
    private bool hasSpawned;
    private int[] numOfNpc = {0, 15, 15, 15};
    private BoidsController boidsController;

	// Use this for initialization
	void Start () {
        player = playerObject.GetComponent<Player>();	
        cameraController = cameraObject.GetComponent<CameraController>();
		gameController = cameraObject.GetComponent<GameController> ();
        fullScore = 15;
        maxLevel = 3;
        currentLevel = 1;
        hasSpawned = false;

        gameController.SetThreshold(fullScore);

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
        npcSpawner = new NpcSpawner();
        boidsController = new BoidsController();

        boidsController.AddCharacter(player);

        List<GameObject> npcPrefabs = new List<GameObject>();
        npcPrefabs.Add(foxPrefab);
        npcPrefabs.Add(birdPrefab);
        npcSpawner.SetNpcPrefabs(npcPrefabs);
        // Spawn for the first level.
        boidsController.AddCharacters(npcSpawner.SpawnNpc(15, new Vector2(0f, -6f), new Vector2(40f, 30f)));
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
        // Open the bridge.
        if (gameController.GetScore() >= fullScore && cameraController.GetIsCrossing() == false) {
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("Bridge" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = false;
                river.GetComponent<Collider2D>().enabled = false;
            }
            cameraController.SetIsCrossing(true);
        }

        // Spawn for next level
        if (cameraController.GetIsCrossing() && player.GetComponent<Transform>().position.y > milestones[currentLevel] + 5f && currentLevel < maxLevel && hasSpawned == false) {
            GameObject nextPlayground = GameObject.Find("Playground"+(currentLevel+1));
            Transform borders = nextPlayground.GetComponent<Transform>().FindChild("Borders");
            
            Bounds totalBounds = borders.GetComponentInChildren<Collider2D>().bounds;
            foreach(var col in borders.GetComponentsInChildren<Collider2D>())
            {
                totalBounds.Encapsulate(col.bounds);
            }

            Vector2 size = totalBounds.max - totalBounds.min;
            size -= new Vector2(10f, 10f);
            Vector2 center = (Vector2)totalBounds.min + new Vector2(5f, 5f) + size / 2;
            boidsController.AddCharacters(npcSpawner.SpawnNpc(numOfNpc[currentLevel + 1], center, size));
            hasSpawned = true;
        }

        // Go to next level.
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
            //cameraController.SetOffset((Vector2)playgound.GetComponent<Transform>().position);
            cameraController.StartZoom();
            currentLevel ++;
            hasSpawned = false;
        }
        boidsController.FixedUpdate();
    }
}
