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
			Move();
		}


		protected virtual void Move()
		{
			speed = isSprinting == false ? walkingSpeed : sprintingSpeed;



		}

	}

}
