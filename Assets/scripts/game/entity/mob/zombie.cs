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

			mobChar.SetAll(5.0f, 10.0f, 100.0f, 100.0f, 10.0f);
			maxHealth = 100.0f;
			health = maxHealth;
			movement = Vector3.zero;
			damageRate = 1.0f;
		}


		void Update()
		{
			base.Update();
			Move();
		}

		protected override void Move()
		{
			base.Move();

			if (target == null) return;
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