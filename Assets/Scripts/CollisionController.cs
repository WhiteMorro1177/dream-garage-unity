using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
	Animator doorAnimator;

	private void Start()
	{
		doorAnimator = GetComponentInChildren<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			doorAnimator.SetBool("open", true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			doorAnimator.SetBool("open", false);
		}
	}
}
