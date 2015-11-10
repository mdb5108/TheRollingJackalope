using UnityEngine;
using UnityEngine.UI.Extensions;
using System.Collections;

public class BodySectionSelector : MonoBehaviour
{

    public void OnSelectionChanged(int section)
    {
        CustomizeCharacterManager.Instance.SetBodySection((CustomizeCharacterManager.BODY_SECTION)section);
    }

    // Use this for initialization
    void Start ()
    {
        GetComponent<VerticalScrollSnap>().OnSelectionChanged = OnSelectionChanged;
    }
}
