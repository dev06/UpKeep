using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Game
{
	[RequireComponent (typeof(CharacterController))]
	public class Player : Mob
	{

		private CharacterController cc;
		private float rotateY = 0;
		private float rotateX = 0;
		private float mouseXSensitvity;
		private float mouseYSensitvity;


		private void Start()
		{
			base.Start();
			Initialize();
		}

		private void Initialize()
		{
			cc = GetComponent<CharacterController>();
			walkingSpeed = 5.0f;
			sprintingSpeed = 10.0f;
			mouseXSensitvity = 2.0f;
			mouseYSensitvity = 2.0f;
			jumpingHeight = 1.2f;
			jumpForce = 1.0f;

		}

		private void Update()
		{
			base.Update();
			Look();
			Jump();
		}

		private void FixedUpdate()
		{
			Move();
		}


		protected override void Move()
		{
			base.Move();
			isSprinting = Input.GetKey(KeyCode.LeftShift);
			float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
			float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
			Vector3 movement = new Vector3(x, jumpForce * Physics.gravity.y * Time.deltaTime, z);
			movement = transform.rotation * movement;
			cc.Move(movement);

		}


		protected void Look()
		{
			rotateX = Input.GetAxis("Mouse X") *  mouseXSensitvity;


			transform.Rotate(0, rotateX, 0);


			rotateY -= Input.GetAxis("Mouse Y") * mouseYSensitvity;
			rotateY = Mathf.Clamp(rotateY, -60, 60);
			Camera.main.transform.localRotation = Quaternion.Euler(rotateY, 0, 0);

		}

		protected void Jump()
		{
			if (Input.GetKey(KeyCode.Space) && !isJumping)
			{
				PrepareJump();
			}
		}



	}
}
