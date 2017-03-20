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
		private Vector3 movement;
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
			movement = Vector3.zero;
			animator = GetComponent<Animator>();
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
			movement.x = currX;
			movement.y = jumpForce * Physics.gravity.y * Time.deltaTime;
			movement.z = currZ;
			Quaternion rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement) , Time.deltaTime * 10.0f);
			transform.rotation = Quaternion.Euler(new Vector3(0 , rotation.eulerAngles.y, 0));
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

			//	transform.LookAt(transform.forward);
		}

		protected void Jump()
		{
			if (cc.isGrounded)
			{
				PrepareJump();
			}
		}

		protected override void UpdateMobActionState()
		{
			base.UpdateMobActionState();

			if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.z) > 0)
			{
				SetMobActionState(MobActionState.WALK);
				animator.speed = 1;
			} else if (isSprinting)
			{
				SetMobActionState(MobActionState.SPRINT);
				animator.speed = 6;
			} else
			{

				SetMobActionState(MobActionState.IDLE);
			}


			animator.SetBool("isWalking", mobActionState == MobActionState.WALK);
		}



	}
}