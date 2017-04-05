using UnityEngine;
using System.Collections;

namespace Game
{

	[RequireComponent (typeof(NavMeshAgent))]
	public class zombie : Mob
	{

		public float memory = 10.0f;


		private Transform target;
		private NavMeshAgent agent;
		private bool targetInMemory;
		private bool targetOutside;
		private float memoryTimer;
		private int walkHash = Animator.StringToHash("isWalking");


		void Start()
		{
			base.Start();
			Initialize();
		}

		void Initialize()
		{
			animator = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
			target = FindObjectOfType<Player>().transform.FindChild("Target").transform;
			mobChar.SetHealth(100);
			StartCoroutine(Track());
		}

		void FixedUpdate()
		{
			if (gameController.isGamePaused) return;

			if (targetOutside && targetInMemory)
			{
				memoryTimer += Time.deltaTime;
				if (memoryTimer > memory)
				{
					targetInMemory = false;
					memoryTimer = 0;
					targetOutside = false;
				}
			}

		}

		IEnumerator Track()
		{
			float delay = .25f;

			while (agent != null && target != null)
			{
				float distanceFromTarget = Vector3.Distance(target.position, transform.position);


				if (distanceFromTarget < 10)
				{
					targetInMemory = true;
					targetOutside = false;
					memoryTimer = 0;

					if (distanceFromTarget < 2)
					{
						CheckForMeleeAttack();
					}

				} else
				{
					targetOutside = true;
				}


				if (targetInMemory)
				{
					ResumeAgent();
				} else
				{
					PauseAgent();
				}

				if (gameController.isGamePaused)
				{
					PauseAgent();
				}

				yield return new WaitForSeconds(delay);
			}

		}


		private void ResumeAgent()
		{
			if (agent != null)
			{
				agent.Resume();
				agent.SetDestination(target.position);
				animator.SetBool(walkHash, true);
			}
		}

		private void PauseAgent()
		{
			if (agent != null)
			{
				agent.Stop();
				animator.SetBool(walkHash, false);
			}
		}


		private void CheckForMeleeAttack()
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray.origin, transform.forward * 1,  out hitInfo))
			{
				GameObject hitObject = hitInfo.transform.gameObject;
				if (hitObject.GetComponent<Mob>() != null)
				{
					Mob mob = (Mob)hitObject.GetComponent<Mob>();
					if (mob == this) return;
					mob.DoDamage(10);
				}
			}
		}
	}
}