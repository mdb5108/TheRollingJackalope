using UnityEngine;
using System.Collections;

public class FreezeRotation : MonoBehaviour {

	private Quaternion initRotation;

	void Start ()
	{
		initRotation = transform.rotation;
	}

	void LateUpdate ()
	{
		transform.rotation = initRotation;
	}
}
