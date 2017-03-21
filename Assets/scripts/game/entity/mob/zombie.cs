using UnityEngine;


namespace Game
{

	public class zombie : Mob
	{


		public Transform target;

		private Vector3 movement;
		private CharacterController cc;
		private NavMeshAgent agent;


		void Start()
		{
			base.Start();
			Initialize();
		}


		private void Initialize()
		{
			animator = GetComponent<Animator>();
			cc = GetComponent<CharacterController>();
			agent = GetComponent<NavMeshAgent>();
			maxHealth = 100.0f;
			health = maxHealth;
			movement = Vector3.zero;
			walkingSpeed = 3.0f;
			damageRate = 1.0f;
			damage = 1.0f;
		}


		void Update()
		{
			base.Update();
			Move();
		}

		bool b;
		protected override void Move()
		{
			base.Move();


			if (Vector3.Distance(transform.position, target.position) < 30.0f)
			{
				if (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance)
				{
					agent.Resume();
					agent.SetDestination(target.position);
				}

				animator.SetBool("playerInProximity", true);
			} else
			{
				agent.Stop();
				animator.SetBool("playerInProximity", false);
			}
		}

	}
}