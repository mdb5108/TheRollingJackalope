using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockableItem : MonoBehaviour
{
    public enum SHOW_STATE { UNLOCKED, UNLOCKED_ON, LOCKED, LOCKED_ON };
    private SHOW_STATE ourState;

    [SerializeField] private Image image;
    [SerializeField] private Image locked;

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetState(SHOW_STATE state)
    {
        ourState = state;
        switch(ourState)
        {
            case SHOW_STATE.UNLOCKED:
                image.enabled  = true;
                locked.enabled = false;
                break;
            case SHOW_STATE.UNLOCKED_ON:
                image.enabled  = false;
                locked.enabled = false;
                break;
            case SHOW_STATE.LOCKED:
                image.enabled  = true;
                locked.enabled = true;
                break;
            case SHOW_STATE.LOCKED_ON:
                image.enabled  = true;
                locked.enabled = true;
                break;
        }
    }
}
