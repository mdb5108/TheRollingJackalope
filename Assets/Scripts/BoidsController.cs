using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidsController {

    private float neighborRadius;
    public List<Character> characters = new List<Character>();
	// Use this for initialization
	public void Start () {
	    Time.fixedDeltaTime = 0.02f;
        neighborRadius = 5f;
	}
	
    public void AddCharacter(Character i_character) {
        characters.Add(i_character);
    }
    public void AddCharacters(List<Character> i_characters) {
        characters.AddRange(i_characters);
    }

    public List<Character> FindCharactersInNeighborhood( Character i_character) {
        Vector2 center = (Vector2) i_character.GetComponent<Transform>().position;
        List<Character> characterInNeighborhood = new List<Character>();
        foreach (Character character in characters) {
            float distance = Vector2.Distance(center, (Vector2) character.GetComponent<Transform>().position);
            if (distance < neighborRadius) {
                characterInNeighborhood.Add(character);
            }
        }

        return characterInNeighborhood;
    }
    public Vector2 GetSeparationForce( Character i_character) {
        Vector2 repulsiveForce = Vector2.zero;
        List<Character> characterInNeighborhood = FindCharactersInNeighborhood( i_character);
        foreach (Character character in characterInNeighborhood) {
            Vector2 force = (Vector2) i_character.GetComponent<Transform>().position - (Vector2)character.GetComponent<Transform>().position;
            if (force.magnitude != 0f) {
                //repulsiveForce += force.normalized * Mathf.Pow((1f/force.magnitude), 3f);
                repulsiveForce += Mathf.Sqrt(neighborRadius * neighborRadius - force.magnitude * force.magnitude) * force.normalized;
            }
        }
        return repulsiveForce;
    }
    public Vector2 GetCohensionForce( Character i_character) {
        Vector2 attractiveForce = Vector2.zero;
        List<Character> characterInNeighborhood = FindCharactersInNeighborhood( i_character);
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
	public void FixedUpdate () {
        // Remove destroyed objects.
        for (int i = 0; i < characters.Count; ++ i) {
            if (characters[i] == null) {
                characters.Remove(characters[i]);
            }
        }
        for (int i = 0; i < characters.Count; ++ i) {
            Vector2 sumForce = Vector2.zero;
            Character character = characters[i];
            
            if (character.name == "Player") {
                continue;
            }

            sumForce += GetSeparationForce( character) / 10f;
            sumForce += GetCohensionForce( character) / 100f;
            character.velocity += sumForce/5f;

            character.GetComponent<Rigidbody2D>().MovePosition((Vector2)character.GetComponent<Transform>().position + character.velocity);
        }
	}
}
