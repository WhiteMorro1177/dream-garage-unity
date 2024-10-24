using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
	public float pickupDistance = 15f;
	public float throwStrength = 10;

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
			if (hit.transform.tag == "Pickable")
			{
				if (isHandsEmpty)
				{
					objectInHand = hit.transform.gameObject;
					objectInHand.GetComponent<Rigidbody>().isKinematic = true;

					objectInHand.transform.parent = transform;
					objectInHand.transform.localPosition = Vector3.zero;
					objectInHand.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
					isHandsEmpty = false;
				}
			}
		}
	}

	void Drop()
	{
		objectInHand.transform.parent = null;
		objectInHand.GetComponent<Rigidbody>().isKinematic = false;
		objectInHand.GetComponent<Rigidbody>().useGravity = true;
		objectInHand.GetComponent<Rigidbody>().AddForce(camera.transform.forward.normalized * throwStrength * 500 * Time.deltaTime);
		objectInHand = null;
		isHandsEmpty = true;
	}
}
