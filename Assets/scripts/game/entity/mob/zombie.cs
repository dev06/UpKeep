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
		private float noiseDistance = 1;
		private float attackDistance = .8f;
		private float defaultDistance;
		private int walkHash = Animator.StringToHash("playerInProximity");


		void OnEnable()
		{
			EventManager.OnFireWeapon += OnFireWeapon;
		}

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
			defaultDistance = 10f;
		}

		void FixedUpdate()
		{
			if (gameController.isGamePaused) { return; }

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

			if (noiseDistance > 1)
			{
				noiseDistance -= Time.deltaTime * (noiseDistance / 2 );
			}
		}

		IEnumerator Track()
		{
			float delay = .001f;

			while (agent != null && target != null)
			{
				float distanceFromTarget = Vector3.Distance(target.position, transform.position);


				if (distanceFromTarget < defaultDistance + noiseDistance)
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


		public override void DoDamage(float damage)
		{
			base.DoDamage(damage);

			if (isDead())
			{
				GetComponent<CapsuleCollider>().enabled = false;
				GetComponent<NavMeshAgent>().enabled = false;
				GetComponent<Animator>().enabled = false;
				GetComponent<MeshCollider>().enabled = false;
				StopAllCoroutines();
				EventManager.OnFireWeapon -= OnFireWeapon;


				StartCoroutine("DestroyObject" , 10);

			}
		}


		private void ResumeAgent()
		{

			if (agent == null) { return; }
			if (agent != null)
			{
				agent.Resume();
				agent.SetDestination(target.position);
				animator.SetBool(walkHash, true);
			}
		}

		private void PauseAgent()
		{
			if (agent == null) { return; }
			if (agent != null)
			{
				agent.Stop();
				animator.SetBool(walkHash, false);
			}
		}


		private void CheckForMeleeAttack()
		{
			if (DebugController.DEBUG_MODE) { return; }
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray.origin, transform.forward,  out hitInfo , attackDistance))
			{
				GameObject hitObject = hitInfo.transform.gameObject;
				if (hitObject.GetComponent<Mob>() != null)
				{
					Mob mob = (Mob)hitObject.GetComponent<Mob>();
					if (mob == this) { return; }
					mob.DoDamage(10 * Time.deltaTime);
				}
			}
		}


		private void OnFireWeapon(Weapon weapon)
		{
			noiseDistance +=  weapon.noise;
		}

		void OnDisable()
		{
			StopAllCoroutines();
		}

		IEnumerator DestroyObject(float delay)
		{
			yield return new WaitForSeconds(delay);

			while (true)
			{
				if (Vector3.Distance(transform.position, target.transform.position) > 10)
				{
					Destroy(gameObject);
				}

				yield return new WaitForSeconds(1);
			}

		}


	}
}