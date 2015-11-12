using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private Text text;

    // Update is called once per frame
    void Update ()
    {
        text.text = SavedGameManager.Instance.GetData().currency.ToString();
    }
}
