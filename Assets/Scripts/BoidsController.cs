using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidsController {

    private float neighborRadius;
    private List<Character> characters = new List<Character>();
	// Use this for initialization
	public void Start () {
	
        neighborRadius = 2f;
	}
	
    public void AddCharacter(ref Character i_character) {
        characters.Add(i_character);
    }

    //
    public List<Character> FindCharactersInNeighborhood(ref Character i_character) {
        Vector2 center = (Vector2) i_character.GetComponent<Transform>().position;
        List<Character> characterInNeighborhood = new List<Character>();
//        Character[] characters = FindObjectsOfType(typeof(Character)) as Character[];
        foreach (Character character in characters) {
            float distance = Vector2.Distance(center, (Vector2) character.GetComponent<Transform>().position);
            if (distance < neighborRadius) {
                characterInNeighborhood.Add(character);
            }
        }

        return characterInNeighborhood;
    }
    public Vector2 GetSeparationForce(ref Character i_character) {
        Vector2 repulsiveForce = Vector2.zero;
        List<Character> characterInNeighborhood = FindCharactersInNeighborhood(ref i_character);
        foreach (Character character in characterInNeighborhood) {
            repulsiveForce += (Vector2) i_character.GetComponent<Transform>().position - (Vector2)character.GetComponent<Transform>().position;
        }
        return repulsiveForce;
    }
    public Vector2 GetCohensionForce(ref Character i_character) {
        Vector2 attractiveForce = Vector2.zero;
        List<Character> characterInNeighborhood = FindCharactersInNeighborhood(ref i_character);
        if (characterInNeighborhood.Count < 1) {
            return Vector2.zero;
        }

        Vector2 sum = Vector2.zero;
        foreach (Character character in characterInNeighborhood) {
            sum += (Vector2) character.GetComponent<Transform>().position; 
        }
        
        attractiveForce = (Vector2)i_character.GetComponent<Transform>().position - sum / characterInNeighborhood.Count;
        return attractiveForce;
    }
	// Update is called once per frame
	public void Update () {
        for (int i = 0; i < characters.Count; ++ i) {
            Vector2 sumForce = Vector2.zero;
            Character character = characters[i];
            sumForce += GetSeparationForce(ref character) / 100f;
            sumForce += GetCohensionForce(ref character);
            character.velocity += sumForce/1000f;
            //character.GetComponent<Transform>().position = (Vector2)character.GetComponent<Transform>().position + sumForce;
            character.GetComponent<Transform>().position = (Vector2)character.GetComponent<Transform>().position + character.velocity;
        }
	}
}
