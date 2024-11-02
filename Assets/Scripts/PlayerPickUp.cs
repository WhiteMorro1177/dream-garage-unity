using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
	public float pickupDistance = 15f;
	public float baseThrowStrength = 10;

	GameObject camera;
	GameObject objectInHand;
	bool isHandsEmpty;

	private void Start()
	{
		isHandsEmpty = true;
		camera = transform.parent.gameObject;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			PickUp();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Drop();
		}
	}

	void PickUp()
	{
		RaycastHit hit;

		if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, pickupDistance))
		{
			if (isHandsEmpty)
			{
				if (hit.transform.tag == "Pickable")
				{
					PlaceObjectInHands(hit, Vector3.zero);
				}
				if (hit.transform.tag == "BigPickable")
				{
					Vector3 objectPositionInHands = new Vector3(-0.275f, -0.1f, 0f);
					PlaceObjectInHands(hit, objectPositionInHands);
				}
			}
		}
	}

	void PlaceObjectInHands(RaycastHit hit, Vector3 localPosition)
	{
		objectInHand = hit.transform.gameObject;
		objectInHand.GetComponent<Rigidbody>().isKinematic = true;

		objectInHand.transform.parent = transform;
		objectInHand.transform.localPosition = localPosition;
		objectInHand.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		objectInHand.layer = LayerMask.NameToLayer("Items");
		isHandsEmpty = false;
	}

	void Drop()
	{
		objectInHand.transform.parent = null;
		objectInHand.GetComponent<Rigidbody>().isKinematic = false;
		objectInHand.GetComponent<Rigidbody>().useGravity = true;

		// calculate throw strength

		Rigidbody objectInHandRB = objectInHand.GetComponent<Rigidbody>();
		float throwStrength = baseThrowStrength * 500 * Time.deltaTime * objectInHandRB.mass;

		objectInHand.GetComponent<Rigidbody>().AddForce(camera.transform.forward.normalized * throwStrength);
		objectInHand.layer = LayerMask.NameToLayer("Default");
		objectInHand = null;
		isHandsEmpty = true;
	}
}
