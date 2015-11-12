using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
    private GameObject characterPrefab;
    public GameObject foxPrefab;
    public GameObject ostrichPrefab;
    private Vector2 ArenaPosition;
    private Vector2 ArenaSize;
    private int numOfCharacters;
    private BoidsController boidsController;
	private GameController gameController;

	void Start ()
	{
		gameController = Camera.main.GetComponent<GameController> ();
        boidsController = new BoidsController();
	    ArenaPosition = new Vector2(-20f, -20f);
        ArenaSize = new Vector2(40f, 40f);

        numOfCharacters = 15;
		gameController.SetThreshold (numOfCharacters);

        for (int i = 0; i < numOfCharacters; ++i) {
			Character character = ((GameObject)Instantiate(foxPrefab, Vector2.zero, Quaternion.identity)).GetComponent<Character>();

            Vector2 position = new Vector2(Random.Range(0, ArenaSize.x), Random.Range(0, ArenaSize.y));
            position += ArenaPosition;
            character.GetComponent<Transform>().position = position;
            boidsController.AddCharacter(character);


			Character character2 = ((GameObject)Instantiate(ostrichPrefab, Vector2.zero, Quaternion.identity)).GetComponent<Character>();
			Vector2 position2 = new Vector2(Random.Range(0, ArenaSize.x), Random.Range(0, ArenaSize.y));
			position2 += ArenaPosition;
			character2.GetComponent<Transform>().position = position2;
			boidsController.AddCharacter(character2);
        }
        
        Character player = GameObject.Find("Player").GetComponent<Character>();
        boidsController.AddCharacter(player);

        boidsController.Start();
	}

	void FixedUpdate ()
	{
        //Debug.Log(boidsController.characters.Count);
        boidsController.FixedUpdate();	
	}
}