using UnityEngine;
using System.Collections;

namespace Game
{
	public class Mob : Entity
	{

		protected float health;
		protected float maxHealth;
		protected float damage;

		protected float speed;
		protected float walkingSpeed;
		protected float sprintingSpeed;
		protected bool isSprinting;
		protected bool isJumping;
		protected float jumpingHeight;
		protected float jumpForce;

		protected bool hostile;
		protected GameObject model;


		protected void Start()
		{
			base.Start();
		}


		protected bool isDead()
		{
			return health < 0;
		}


		protected void Update()
		{
			base.Update();
		}


		protected virtual void Move()
		{
			speed = isSprinting == false ? walkingSpeed : sprintingSpeed;
		}


		protected void PrepareJump()
		{
			jumpForce = -jumpingHeight;
			isJumping = true;
			StopCoroutine("StartJump");
			StartCoroutine("StartJump");

		}

		protected IEnumerator StartJump()
		{
			while (jumpForce < jumpingHeight)
			{
				jumpForce += Time.deltaTime * 5.0f;
				yield return new WaitForSeconds(Time.deltaTime);
			}

			isJumping = false;

		}

	}

}
