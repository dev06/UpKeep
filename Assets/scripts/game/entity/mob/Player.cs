using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using system;

namespace Game
{
	[RequireComponent (typeof(CharacterController))]
	public class Player : Mob
	{

		private CameraBob cameraBob;
		private CharacterController cc;
		private float rotateY = 0;
		private float rotateX = 0;
		private float mouseXSensitvity;
		private float mouseYSensitvity;
		private Vector3 movement;



		private void Start()
		{
			base.Start();
			Initialize();

		}

		private void Initialize()
		{
			cc = GetComponent<CharacterController>();
			cameraBob = Camera.main.GetComponent<CameraBob>();
			animator = GetComponent<Animator>();
			movement = Vector3.zero;


			mobChar.SetAll(5.0f, 10.0f, 100.0f, 100.0f, 10.0f, 3.0f, 2.0f);

			damage = 30.0f;
			damageRate = 1.0f;

			maxHealth = 100.0f;
			health = maxHealth;

			maxStamina = 100.0f;
			stamina = maxStamina;

			mobActionState = MobActionState.IDLE;
			walkingSpeed = GetFloat("WalkingSpeed");
			sprintingSpeed = GetFloat("SprintingSpeed");
			mouseXSensitvity = 2.0f;
			mouseYSensitvity = 2.0f;
			jumpingHeight = .8f;
			jumpForce = 1.0f;

		}



		private void Update()
		{
			base.Update();
			Look();
			Jump();
			Move();
			Punch();
		}

		private void FixedUpdate()
		{

		}


		protected override void Move()
		{
			base.Move();
			speed = isSprinting == false ? walkingSpeed : sprintingSpeed;
			float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
			float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
			Vector3 force = new Vector3(x, jumpForce * Physics.gravity.y * Time.deltaTime, z);
			movement = force;
			force = transform.rotation * force;

			UpdateStamina();

			if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
			{
				cameraBob.Bob(1.0f);
			}
			cc.Move(force);

		}

		protected void Look()
		{
			rotateX = Input.GetAxis("Mouse X") *  mouseXSensitvity;

			transform.Rotate(0, rotateX, 0);

			rotateY -= Input.GetAxis("Mouse Y") * mouseYSensitvity;
			rotateY = Mathf.Clamp(rotateY, -50, 50);

			Camera.main.transform.localRotation = Quaternion.Euler(rotateY, 0, 0);
		}


		protected override void UpdateMobActionState()
		{
			base.UpdateMobActionState();

			if (Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.z) > 0)
			{
				SetMobActionState(MobActionState.WALK);
				animator.speed = 2;
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

		protected void Jump()
		{
			if (Input.GetKey(KeyCode.Space) && cc.isGrounded)
			{
				PrepareJump();
			}
		}

		protected void Punch()
		{
			if (Input.GetMouseButtonDown(0))
			{
				animator.SetBool("punch", true);
				if (other != null)
				{
					DoDamage(other, this);
				}
			} else
			{
				animator.SetBool("punch", false);
			}
		}

		private void UpdateStamina()
		{
			float stamina = GetFloat("Stamina");
			float staminaDep = GetFloat("StaminaDepletionRate");
			float staminaRep = GetFloat("StaminaRepletionRate");

			if (isSprinting)
			{
				if (stamina > 0)
				{
					SetFloat("Stamina", stamina - Time.deltaTime * staminaDep);
				}
			} else
			{

				if (stamina < GetFloat("MaxStamina") && !Input.GetKey(KeyCode.LeftShift))
				{
					SetFloat("Stamina", stamina + Time.deltaTime * staminaRep);
				}
			}

			isSprinting = (stamina > 0 && Input.GetKey(KeyCode.LeftShift));
		}




		protected override void OnCollisionStay(Collision collision)
		{
			base.OnCollisionStay(collision);

			if (collision.gameObject.GetComponent<Mob>() != null)
			{

				Mob mob = collision.gameObject.GetComponent<Mob>();


				if (mob != this && inContact == true)
				{
					damageTimer += Time.deltaTime;
					if (damageTimer > damageRate)
					{
						DoDamage(this, mob);
						//Health -= mob.Damage;
						damageTimer = 0;
					}
				}
			}
		}

	}
}
