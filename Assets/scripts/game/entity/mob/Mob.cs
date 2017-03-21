using UnityEngine;
using System.Collections;

namespace Game
{
	[RequireComponent (typeof(Animator))]
	public class Mob : Entity
	{
		protected enum MobActionState
		{
			WALK,
			SPRINT,
			IDLE
		}

		protected float health;
		protected float maxHealth;
		protected float stamina;
		protected float maxStamina;

		protected float damage;

		protected float speed;
		protected float walkingSpeed;
		protected float sprintingSpeed;
		protected bool isSprinting;
		protected bool isJumping;
		protected bool inContact;
		protected float jumpingHeight;
		protected float jumpForce;
		protected MobActionState mobActionState;
		protected bool hostile;
		protected GameObject model;
		protected Animator animator;
		protected Mob other;

		protected float damageTimer;
		protected float damageRate;


		protected void Start()
		{
			base.Start();

		}




		protected bool isDead()
		{
			return health <= 0;
		}


		protected void Update()
		{
			base.Update();
		}


		protected virtual void Move()
		{
			speed = isSprinting == false ? walkingSpeed : sprintingSpeed;
			Stamina = isSprinting ? Stamina - Time.deltaTime * 5.0f : (Stamina < 100) ? Stamina + Time.deltaTime * 2.0f : Stamina;

			UpdateMobActionState();

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


		public void DoDamage(Mob toMob, Mob other)
		{


			toMob.Health -= other.Damage;
			if (toMob.Health <= 0 )
			{
				Destroy(toMob.gameObject);
			}



		}


		protected virtual void UpdateMobActionState()
		{

		}

		/// <summary>
		/// Sets the actions state of the mob [WALK, IDLE, SPRINT, ETC.]
		/// </summary>
		/// <param name="mas"></param>
		protected virtual void SetMobActionState(MobActionState mas)
		{
			mobActionState = mas;
		}




		protected void OnCollisionEnter(Collision collision)
		{

			if (collision.gameObject.GetComponent<Mob>() != null)
			{

				Mob mob = collision.gameObject.GetComponent<Mob>();

				if (mob != this)
				{
					inContact = true;
					other = mob;
				}
			}

		}


		protected virtual void OnCollisionStay(Collision collision)
		{

		}

		protected void OnCollisionExit(Collision collision)
		{

			if (collision.gameObject.GetComponent<Mob>() != null)
			{

				Mob mob = collision.gameObject.GetComponent<Mob>();

				if (mob != this)
				{
					inContact = false;
					other = null;
					damageTimer = 0;

				}
			}

		}



		public float Health
		{
			get {return health; }
			set { if (health >= 0) { health = value; } }
		}

		public float MaxHealth
		{
			get {return maxHealth; }

		}


		public float Stamina
		{
			get {return stamina; }
			set { if ((int)stamina >= 0) { stamina = value; } }
		}

		public float MaxStamina
		{
			get {return maxStamina; }

		}

		public float Damage
		{
			get { return damage; }
		}

	}
}


