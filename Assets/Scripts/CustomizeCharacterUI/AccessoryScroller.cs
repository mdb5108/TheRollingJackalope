using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

public class AccessoryScroller : MonoBehaviour
{
    enum BODY_SECTION { HEAD = 0, BODY = 1, FOOT = 2 };

    [SerializeField]
    private GameObject UIContainer;

    KeyValuePair<string, GameObject>[] items;
    GameObject[] instantiated;

    [SerializeField]
    private HorizontalScrollSnap scroller;
    [SerializeField]
    private GameObject contentPane;

    [SerializeField]
    private BODY_SECTION ourSection;

    // Use this for initialization
    void Start ()
    {
        scroller.OnSelectionChanged = OnSelectionChanged;
        KeyValuePair<string, GameObject>[] accessories;
        string accessoryName;

        switch(ourSection)
        {
            case BODY_SECTION.HEAD:
                accessories = AccessoryManager.Instance.GetHeadAccessories();
                accessoryName = SavedGameManager.Instance.GetData().headAccessory;
                break;
            case BODY_SECTION.BODY:
                accessories = AccessoryManager.Instance.GetBodyAccessories();
                accessoryName = SavedGameManager.Instance.GetData().bodyAccessory;
                break;
            default:
            case BODY_SECTION.FOOT:
                accessories = AccessoryManager.Instance.GetFootAccessories();
                accessoryName = SavedGameManager.Instance.GetData().footAccessory;
                break;
        }

        SetItems(accessories);
        scroller.Start();

        int i;
        for(i = 0; i < accessories.Length; i++)
        {
            if(accessories[i].Key == accessoryName)
            {
                SetInitialSelect(i);
                break;
            }
        }
        if(i == accessories.Length)
        {
            Debug.Log("Not Found: " + accessoryName);
        }
    }

    public void SetInitialSelect(int item)
    {
        scroller.JumpToScreen(item);
    }

    public void OnSelectionChanged(int item)
    {
        switch(ourSection)
        {
            case BODY_SECTION.HEAD:
                Player.Instance.SetHeadAccessory(items[item].Key);
                break;
            case BODY_SECTION.BODY:
                Player.Instance.SetBodyAccessory(items[item].Key);
                break;
            default:
            case BODY_SECTION.FOOT:
                Player.Instance.SetFootAccessory(items[item].Key);
                break;
        }
        foreach(var go in instantiated)
        {
            go.SetActive(true);
        }
        instantiated[item].SetActive(false);
    }

    private void SetItems(KeyValuePair<string, GameObject>[] items)
    {
        this.items = items;
        if(this.instantiated != null)
        {
            foreach(var go in this.instantiated)
            {
                go.transform.SetParent(null);
                Destroy(go);
            }
        }
        this.instantiated = new GameObject[items.Length];

        for(int i = 0; i < items.Length; i++)
        {
            GameObject go = (GameObject)Instantiate(UIContainer, Vector2.zero, Quaternion.identity);
            go.transform.SetParent(contentPane.transform, true);
            go.GetComponent<Image>().sprite = items[i].Value.GetComponentsInChildren<SpriteRenderer>(true)[0].sprite;
            this.instantiated[i] = go;
        }
    }
}
