using UnityEngine;
using System.Collections;

public class CustomizeCharacterManager : MonoBehaviour
{
    public enum BODY_SECTION {HEAD = 0, BODY = 1, FOOT = 2};
    BODY_SECTION curSection = BODY_SECTION.HEAD;

    public AccessoryScroller accessoryScroller;

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

    public void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SetBodySection(BODY_SECTION.HEAD);
    }

    public void SetBodySection(BODY_SECTION section)
    {
        curSection = section;
        accessoryScroller.SetBodyActive(section);
    }

    public BODY_SECTION GetCurrentSection()
    {
        return curSection;
    }
}
