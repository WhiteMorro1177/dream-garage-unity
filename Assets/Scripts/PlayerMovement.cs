using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;
	public float speed = 10f;
	public float gravity = -9.81f;

	public float playerHeight;
	public LayerMask whatIsGround;

	bool grounded;
	Vector3 velocity;

	void Update()
	{
		float X = Input.GetAxis("Horizontal");
		float Z = Input.GetAxis("Vertical");

		Vector3 moveDirection = transform.forward * Z + transform.right * X;

		controller.Move(moveDirection * speed * Time.deltaTime);

		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);

		// ground check
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, LayerMask.GetMask("Default"));

		if (grounded)
		{
			velocity.y = -2f;
		}
	}
}
