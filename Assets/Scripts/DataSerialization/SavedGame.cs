using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SavedGameData
{
    public string headAccessory;
    public string bodyAccessory;
    public string footAccessory;

    public Dictionary<string, bool> headUnlocked;
    public Dictionary<string, bool> bodyUnlocked;
    public Dictionary<string, bool> footUnlocked;

    public SavedGameData()
    {
        headAccessory = "None";
        bodyAccessory = "None";
        footAccessory = "None";
        headUnlocked = new Dictionary<string, bool>();
        bodyUnlocked = new Dictionary<string, bool>();
        footUnlocked = new Dictionary<string, bool>();
        headUnlocked.Add("None", true);
        bodyUnlocked.Add("None", true);
        footUnlocked.Add("None", true);
    }
}

public class SavedGameManager : MonoBehaviour
{
    private static SavedGameManager _instance;
    public static SavedGameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<SavedGameManager>();
                if(_instance == null)
                {
                    var go = new GameObject("SavedGameManager (new)");
                    _instance = go.AddComponent<SavedGameManager>();
                    _instance.data = new SavedGameData();
                    DontDestroyOnLoad(_instance);
                }
            }

            return _instance;
        }
    }

    private SavedGameData data;

    public SavedGameData GetData()
    {
        return data;
    }
}
