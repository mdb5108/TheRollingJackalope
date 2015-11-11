using UnityEngine;
using System.Collections;

public class CustomizeCharacterManager : MonoBehaviour
{
    private static CustomizeCharacterManager _instance;
    public static CustomizeCharacterManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<CustomizeCharacterManager>();
            }
            return _instance;
        }
    }

    public void LoadGame()
    {
        Application.LoadLevel("Playground1");
    }
}
