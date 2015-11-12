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

    KeyValuePair<string, AccessoryManager.AccessoryItem>[] items;
    LockableItem[] instantiated;

    [SerializeField]
    private BuyPanel buyPanel;

    [SerializeField]
    private HorizontalScrollSnap scroller;
    [SerializeField]
    private GameObject contentPane;

    [SerializeField]
    private BODY_SECTION ourSection;

    private int selected = 0;

    // Use this for initialization
    void Start ()
    {
        scroller.OnSelectionChanged = OnSelectionChanged;
        KeyValuePair<string, AccessoryManager.AccessoryItem>[] accessories;
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
                if(IsUnlocked(item))
                    Player.Instance.SetHeadAccessory(items[item].Key);
                break;
            case BODY_SECTION.BODY:
                if(IsUnlocked(item))
                    Player.Instance.SetBodyAccessory(items[item].Key);
                break;
            default:
            case BODY_SECTION.FOOT:
                if(IsUnlocked(item))
                    Player.Instance.SetFootAccessory(items[item].Key);
                break;
        }

        SetItemState(selected);
        SetItemState(item, true);
        selected = item;
    }

    private void SetItems(KeyValuePair<string, AccessoryManager.AccessoryItem>[] items)
    {
        this.items = items;
        if(this.instantiated != null)
        {
            foreach(var li in this.instantiated)
            {
                li.gameObject.transform.SetParent(null);
                Destroy(li.gameObject);
            }
        }
        this.instantiated = new LockableItem[items.Length];

        for(int i = 0; i < items.Length; i++)
        {
            GameObject go = (GameObject)Instantiate(UIContainer, Vector2.zero, Quaternion.identity);
            go.transform.SetParent(contentPane.transform, true);
            LockableItem li = go.GetComponent<LockableItem>();
            li.SetImage(items[i].Value.obj.GetComponentsInChildren<SpriteRenderer>(true)[0].sprite);
            this.instantiated[i] = li;
            SetItemState(i);
        }
    }

    private void SetItemState(int item, bool selected = false)
    {
        if(!selected)
            instantiated[item].SetState(IsUnlocked(item) ? LockableItem.SHOW_STATE.UNLOCKED : LockableItem.SHOW_STATE.LOCKED);
        else
            instantiated[item].SetState(IsUnlocked(item) ? LockableItem.SHOW_STATE.UNLOCKED_ON : LockableItem.SHOW_STATE.LOCKED_ON);
    }

    public void BuySelected()
    {
        Dictionary<string, bool> unlocked;
        switch(ourSection)
        {
            case BODY_SECTION.HEAD:
                unlocked = SavedGameManager.Instance.GetData().headUnlocked;
                Player.Instance.SetHeadAccessory(items[selected].Key);
                break;
            case BODY_SECTION.BODY:
                unlocked = SavedGameManager.Instance.GetData().bodyUnlocked;
                Player.Instance.SetBodyAccessory(items[selected].Key);
                break;
            default:
            case BODY_SECTION.FOOT:
                unlocked = SavedGameManager.Instance.GetData().footUnlocked;
                Player.Instance.SetFootAccessory(items[selected].Key);
                break;
        }
        unlocked[items[selected].Key] = true;
        instantiated[selected].SetState(LockableItem.SHOW_STATE.UNLOCKED_ON);
        SavedGameManager.Instance.GetData().currency -= this.items[selected].Value.price;
    }

    private bool IsUnlocked(int item)
    {
        Dictionary<string, bool> unlocked;
        switch(ourSection)
        {
            case BODY_SECTION.HEAD:
                unlocked = SavedGameManager.Instance.GetData().headUnlocked;
                break;
            case BODY_SECTION.BODY:
                unlocked = SavedGameManager.Instance.GetData().bodyUnlocked;
                break;
            default:
            case BODY_SECTION.FOOT:
                unlocked = SavedGameManager.Instance.GetData().footUnlocked;
                break;
        }
        bool ret = false;
        unlocked.TryGetValue(this.items[item].Key, out ret);
        return ret;
    }

    public void OpenSelected()
    {
        if(!IsUnlocked(selected))
        {
            var currency = SavedGameManager.Instance.GetData().currency;
            var price = this.items[selected].Value.price;
            CustomizeCharacterManager.Instance.TurnInteractible(false);
            buyPanel.PopUp(this.items[selected].Value.obj.GetComponentsInChildren<SpriteRenderer>(true)[0].sprite,
                           price,
                           price <= currency,
                           BuySelected);
        }
    }
}
