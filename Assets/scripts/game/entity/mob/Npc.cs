using UnityEngine;
using System.Collections;


namespace Game
{

	public class Npc : Mob
	{



		private float timer;
		private float xVel;
		private float zVel;
		private float xMovement;
		private float zMovement;
		private float currX;
		private float currZ;
		private CharacterController cc;


		private void Start()
		{
			base.Start();
			Initialize();
		}

		private void Initialize()
		{
			walkingSpeed = 5.0f;
			jumpForce = 0.0f;
			jumpingHeight = 1.2f;
			timer = Random.Range(1.5f, 3.0f);
			cc = GetComponent<CharacterController>();
		}

		private void Update()
		{
			base.Update();

			ChangeDirection();

		}

		private void FixedUpdate()
		{

			Move();

		}

		protected override void Move()
		{
			base.Move();

			currX = xMovement * Time.deltaTime * walkingSpeed;
			currZ = zMovement * Time.deltaTime * walkingSpeed;

			Vector3 movement = new Vector3(currX, jumpForce * Physics.gravity.y * Time.deltaTime, currZ);
			cc.Move(movement);
		}

		private void ChangeDirection()
		{
			timer += Time.deltaTime;

			if (timer > Random.Range(1.5f, 3.0f))
			{
				UpdateMovementDirection();
				Jump();

				timer = 0;
			}
		}

		private void UpdateMovementDirection()
		{
			xMovement = Random.Range(-1 , 2);
			zMovement = Random.Range(-1 , 2);
		}

		protected void Jump()
		{
			if (!isJumping)
			{
				PrepareJump();
			}
		}



	}
}