using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;

public class BuyPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button actionButton;

    Action confirmed;

    public void PopUp(Sprite item, bool actionEnabled, Action confirm)
    {
        itemImage.sprite = item;

        confirmed = confirm;

        panel.SetActive(true);
        actionButton.interactable = actionEnabled;
    }

    public void PopDown()
    {
        panel.SetActive(false);
    }

    public void ConfirmAction()
    {
        if(confirmed != null)
            confirmed();
        PopDown();
    }
}
