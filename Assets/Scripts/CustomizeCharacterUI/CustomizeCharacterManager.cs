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

    [SerializeField] private AccessoryScroller headScroller;
    [SerializeField] private AccessoryScroller bodyScroller;
    [SerializeField] private AccessoryScroller footScroller;

    int downInRect = -1;

    bool interactible = true; 

    public void TurnInteractible(bool interact)
    {
        interactible = interact;
    }

    void Update()
    {
        if(interactible)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if     (RectTransformUtility.RectangleContainsScreenPoint(headScroller.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    downInRect = 0;
                }
                else if(RectTransformUtility.RectangleContainsScreenPoint(bodyScroller.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    downInRect = 1;
                }
                else if(RectTransformUtility.RectangleContainsScreenPoint(footScroller.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    downInRect = 2;
                }
                else
                {
                    downInRect = -1;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                if     (downInRect == 0 && RectTransformUtility.RectangleContainsScreenPoint(headScroller.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    headScroller.OpenSelected();
                }
                else if(downInRect == 1 && RectTransformUtility.RectangleContainsScreenPoint(bodyScroller.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    bodyScroller.OpenSelected();
                }
                else if(downInRect == 2 && RectTransformUtility.RectangleContainsScreenPoint(footScroller.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    footScroller.OpenSelected();
                }
                downInRect = -1;
            }
        }
    }

    public void LoadGame()
    {
        Application.LoadLevel("Menu");
    }
}
