using UnityEngine;


namespace Game
{
	[RequireComponent (typeof(CharacterController),  typeof(NavMeshAgent))]
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

			if (!gameController.isGamePaused)
			{
				base.Move();
				float distance = Vector3.Distance(transform.position, target.position);
				if (distance < 10)
				{
					animator.SetBool("isWalking", true);
					agent.SetDestination(target.position);
					agent.Resume();

					if (distance < 2)
					{
						Ray ray = new Ray(transform.position, transform.forward);
						RaycastHit hit;
						if (Physics.Raycast(ray.origin, transform.forward, out hit, 1))
						{
							GameObject hitObject = hit.transform.gameObject;
							if (hitObject.GetComponent<Mob>() != null)
							{
								Mob mob = (Mob)hitObject.GetComponent<Mob>();
								mob.SetFloat("Health", mob.GetFloat("Health") - (GetFloat("MeleeDamage") * Time.deltaTime));
							}
						}
					}

				} else
				{
					animator.SetBool("isWalking", false);
					agent.Stop();
				}
			}
		}
	}
}