using UnityEngine;
using System.Collections;

public class Player : Character {

	private Rigidbody2D myRigidbody;

	public float gyroForce = 50;
	public GUIText scoreText;

    private static readonly float MAX_SPEED = 20;

    public bool respondToInput = true;

    public GameObject HeadAnchor;
    public GameObject BodyAnchor;
    public GameObject FootAnchor;

    private GameObject HeadAccessory;
    private GameObject BodyAccessory;
    private GameObject FootAccessory;

    private static Player _instance;
    public static Player Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }

	void Start ()
	{
        //base.Start();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		//Debug.Log (Input.acceleration.x + "," + Input.acceleration.y);
        if(respondToInput)
        {
            myRigidbody.AddForce (new Vector2(Input.acceleration.x*gyroForce, Input.acceleration.y*gyroForce));
            myRigidbody.AddForce (new Vector2(Input.GetAxis("Horizontal")*gyroForce,Input.GetAxis("Vertical")*gyroForce));
        }

        //Clamp player speed so bouncing doesn't get out of control
        if(myRigidbody.velocity.SqrMagnitude() > MAX_SPEED*MAX_SPEED)
            myRigidbody.velocity = myRigidbody.velocity.normalized*MAX_SPEED;
	}



    public void SetHeadAccessory(string name)
    {
        if(HeadAccessory != null)
            Destroy(HeadAccessory);
        HeadAccessory = AccessoryManager.Instance.GetHeadAccessory(name);
        HeadAccessory = (GameObject)Instantiate(HeadAccessory, HeadAccessory.transform.position, HeadAccessory.transform.rotation);
        HeadAccessory.transform.SetParent(HeadAnchor.transform);
    }
    public void SetBodyAccessory(string name)
    {
        if(BodyAccessory != null)
            Destroy(BodyAccessory);
        BodyAccessory = AccessoryManager.Instance.GetBodyAccessory(name);
        BodyAccessory = (GameObject)Instantiate(BodyAccessory, BodyAccessory.transform.position, BodyAccessory.transform.rotation);
        BodyAccessory.transform.SetParent(BodyAnchor.transform);
    }
    public void SetFootAccessory(string name)
    {
        if(FootAccessory != null)
            Destroy(FootAccessory);
        FootAccessory = AccessoryManager.Instance.GetFootAccessory(name);
        FootAccessory = (GameObject)Instantiate(FootAccessory, FootAccessory.transform.position, FootAccessory.transform.rotation);
        FootAccessory.transform.SetParent(FootAnchor.transform);
    }
}
