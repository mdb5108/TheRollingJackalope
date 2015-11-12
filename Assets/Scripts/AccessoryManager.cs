using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class AccessoryManager : MonoBehaviour
{
    [System.Serializable]
    public struct NameAndObject
    {
        public string name;
        public AccessoryItem obj;
    }

    [System.Serializable]
    public struct AccessoryItem
    {
        public GameObject obj;
        public uint price;
    }

    [SerializeField]
    private NameAndObject[] HeadAccessories;
    [SerializeField]
    private NameAndObject[] BodyAccessories;
    [SerializeField]
    private NameAndObject[] FootAccessories;

    private Dictionary<string, AccessoryItem> DictHeadAccessories;
    private Dictionary<string, AccessoryItem> DictBodyAccessories;
    private Dictionary<string, AccessoryItem> DictFootAccessories;

    private static AccessoryManager _instance;
    public static AccessoryManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = ((GameObject)Instantiate(Resources.Load("AccessoryManager"), Vector3.zero, Quaternion.identity)).GetComponent<AccessoryManager>();
            }

            return _instance;
        }
    }

    public void Awake()
    {
        DictHeadAccessories = new Dictionary<string, AccessoryItem>();
        foreach(var no in HeadAccessories)
        {
            DictHeadAccessories.Add(no.name, no.obj);
        }
        DictBodyAccessories = new Dictionary<string, AccessoryItem>();
        foreach(var no in BodyAccessories)
        {
            DictBodyAccessories.Add(no.name, no.obj);
        }
        DictFootAccessories = new Dictionary<string, AccessoryItem>();
        foreach(var no in FootAccessories)
        {
            DictFootAccessories.Add(no.name, no.obj);
        }
    }

    public KeyValuePair<string, AccessoryItem>[] GetHeadAccessories()
    {
        return DictHeadAccessories.ToArray();
    }
    public KeyValuePair<string, AccessoryItem>[] GetBodyAccessories()
    {
        return DictBodyAccessories.ToArray();
    }
    public KeyValuePair<string, AccessoryItem>[] GetFootAccessories()
    {
        return DictFootAccessories.ToArray();
    }

    public AccessoryItem GetHeadAccessory(string name)
    {
        return DictHeadAccessories[name];
    }
    public AccessoryItem GetBodyAccessory(string name)
    {
        return DictBodyAccessories[name];
    }
    public AccessoryItem GetFootAccessory(string name)
    {
        return DictFootAccessories[name];
    }
}
