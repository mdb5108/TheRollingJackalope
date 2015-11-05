using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidsController : MonoBehaviour {

    private float neighborRadius;
    private List<Character> characters = new List<Character>();
	// Use this for initialization
	void Start () {
	
        neighborRadius = 300f;
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

        Vector2 sum = Vector2.zero;
        foreach (Character character in characterInNeighborhood) {
            sum += (Vector2) character.GetComponent<Transform>().position; 
        }
        
        attractiveForce = sum / characters.Count;
        return attractiveForce;
    }
	// Update is called once per frame
	void Update () {
        //foreach (Character character in characters) {
        for (int i = 0; i < characters.Count; ++ i) {
            Vector2 sumForce = Vector2.zero;
            Character character = characters[i];
            sumForce += GetSeparationForce(ref character);
            sumForce += GetCohensionForce(ref character);
            character.GetComponent<Transform>().position = (Vector2)character.GetComponent<Transform>().position + sumForce;
        }
	}
}
