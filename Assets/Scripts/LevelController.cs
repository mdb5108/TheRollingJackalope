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
			milestones[i] = minY;
		}
        npcSpawner = new NpcSpawner();
        boidsController = new BoidsController();

        boidsController.AddCharacter(player);

        List<GameObject> npcPrefabs = new List<GameObject>();
        npcPrefabs.Add(foxPrefab);
        npcPrefabs.Add(birdPrefab);
        npcSpawner.SetNpcPrefabs(npcPrefabs);

        // Spawn for the first level.
		Vector2 center = new Vector2();
		Vector2 size = new Vector2();
		GetTotalBounds ("Playground1", out center, out size);
		boidsController.AddCharacters (npcSpawner.SpawnNpc (numOfNpc [1], center, size));

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
			gameController.SetScore(0);
        }

        // Spawn for next level
        if (cameraController.GetIsCrossing() && player.GetComponent<Transform>().position.y > milestones[currentLevel] + 5f && currentLevel < maxLevel && hasSpawned == false) {
			Vector2 center =  new Vector2();
			Vector2 size = new Vector2();
			GetTotalBounds("Playground"+(currentLevel+1), out center, out size);
            boidsController.AddCharacters(npcSpawner.SpawnNpc(numOfNpc[currentLevel + 1], center, size));
			hasSpawned = true;
        }

        // Go to next level.
        if (player.GetComponent<Transform>().position.y < milestones[currentLevel]-5f && cameraController.GetIsCrossing() == true) {
            GameObject[] rivers = GameObject.FindGameObjectsWithTag("Bridge" + currentLevel);
            foreach (GameObject river in rivers) {
                river.GetComponent<BoxCollider2D>().enabled = true;
                river.GetComponent<Collider2D>().enabled = true;
            }

            GameObject playgound = GameObject.Find("Playground"+(currentLevel + 1));
            cameraObject.GetComponent<CameraController>().borders = playgound.GetComponent<Transform>().FindChild("Borders");
            cameraController.SetIsCrossing(false);
            //cameraController.SetOffset((Vector2)playgound.GetComponent<Transform>().position);
            currentLevel ++;
            hasSpawned = false;
			gameController.ChangeLevel (currentLevel);
			cameraController.StartZoom();
        }

        boidsController.FixedUpdate();
    }

	public void GetTotalBounds (string name, out Vector2 o_center, out Vector2 o_size) {
		GameObject playground = GameObject.Find(name);
		Transform borders = playground.GetComponent<Transform>().FindChild("Borders");
		
		Bounds totalBounds = borders.GetComponentInChildren<Collider2D>().bounds;
		foreach(var col in borders.GetComponentsInChildren<Collider2D>())
		{
			totalBounds.Encapsulate(col.bounds);
		}
		
		o_size = totalBounds.max - totalBounds.min;
		o_size -= new Vector2(10f, 10f);
		o_center = (Vector2)totalBounds.min + new Vector2(5f, 5f) + o_size / 2;
	}
}
