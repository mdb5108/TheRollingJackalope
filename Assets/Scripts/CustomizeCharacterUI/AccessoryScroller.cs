using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Collections;
using System.Collections.Generic;

public class AccessoryScroller : MonoBehaviour
{
    [SerializeField]
    private GameObject UIContainer;

    private static readonly Vector2 ITEM_BASE =       new Vector2(0, -1);
    private static readonly Vector2 ITEM_SEPERATION = new Vector2(1f, 0);

    KeyValuePair<string, GameObject>[] headItems;
    KeyValuePair<string, GameObject>[] bodyItems;
    KeyValuePair<string, GameObject>[] footItems;

    [SerializeField]
    private HorizontalScrollSnap HeadScroller;
    [SerializeField]
    private GameObject headContentPane;
    [SerializeField]
    private HorizontalScrollSnap BodyScroller;
    [SerializeField]
    private GameObject bodyContentPane;
    [SerializeField]
    private HorizontalScrollSnap FootScroller;
    [SerializeField]
    private GameObject footContentPane;

    // Use this for initialization
    void Start ()
    {
        HeadScroller.OnSelectionChanged = OnSelectionChanged;
        BodyScroller.OnSelectionChanged = OnSelectionChanged;
        FootScroller.OnSelectionChanged = OnSelectionChanged;

        SetItems(CustomizeCharacterManager.BODY_SECTION.HEAD, AccessoryManager.Instance.GetHeadAccessories());
        SetItems(CustomizeCharacterManager.BODY_SECTION.BODY, AccessoryManager.Instance.GetBodyAccessories());
        SetItems(CustomizeCharacterManager.BODY_SECTION.FOOT, AccessoryManager.Instance.GetFootAccessories());
    }

    public void OnSelectionChanged(int item)
    {
        switch(CustomizeCharacterManager.Instance.GetCurrentSection())
        {
            case CustomizeCharacterManager.BODY_SECTION.HEAD:
                Player.Instance.SetHeadAccessory(headItems[item].Key);
                break;
            case CustomizeCharacterManager.BODY_SECTION.BODY:
                Player.Instance.SetBodyAccessory(bodyItems[item].Key);
                break;
            default:
            case CustomizeCharacterManager.BODY_SECTION.FOOT:
                Player.Instance.SetFootAccessory(footItems[item].Key);
                break;
        }
    }

    public void SetBodyActive(CustomizeCharacterManager.BODY_SECTION section)
    {
        switch(section)
        {
            case CustomizeCharacterManager.BODY_SECTION.HEAD:
                HeadScroller.gameObject.SetActive(true);
                BodyScroller.gameObject.SetActive(false);
                FootScroller.gameObject.SetActive(false);
                break;
            case CustomizeCharacterManager.BODY_SECTION.BODY:
                HeadScroller.gameObject.SetActive(false);
                BodyScroller.gameObject.SetActive(true);
                FootScroller.gameObject.SetActive(false);
                break;
            case CustomizeCharacterManager.BODY_SECTION.FOOT:
                HeadScroller.gameObject.SetActive(false);
                BodyScroller.gameObject.SetActive(false);
                FootScroller.gameObject.SetActive(true);
                break;
            default:
                Debug.LogError("Body Section given was not found!");
                break;
        }
    }

    private void SetItems(CustomizeCharacterManager.BODY_SECTION section, KeyValuePair<string, GameObject>[] items)
    {
        GameObject contentPane;
        switch(section)
        {
            case CustomizeCharacterManager.BODY_SECTION.HEAD:
                this.headItems = items;
                contentPane = headContentPane;
                break;
            case CustomizeCharacterManager.BODY_SECTION.BODY:
                this.bodyItems = items;
                contentPane = bodyContentPane;
                break;
            default:
            case CustomizeCharacterManager.BODY_SECTION.FOOT:
                this.footItems = items;
                contentPane = footContentPane;
                break;
        }

        for(int i = 0; i < items.Length; i++)
        {
            GameObject go = (GameObject)Instantiate(UIContainer, Vector2.zero, Quaternion.identity);
            go.transform.SetParent(contentPane.transform, true);
            go.GetComponent<Image>().sprite = items[i].Value.GetComponentsInChildren<SpriteRenderer>(true)[0].sprite;
        }
    }
}
