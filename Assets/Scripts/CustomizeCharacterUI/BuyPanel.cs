using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;

public class BuyPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button actionButton;
    [SerializeField] private Text priceText;

    Action confirmed;

    public void PopUp(Sprite item, uint price, bool actionEnabled, Action confirm)
    {
        itemImage.sprite = item;

        confirmed = confirm;

        panel.SetActive(true);
        actionButton.interactable = actionEnabled;

        priceText.text = price.ToString();
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
