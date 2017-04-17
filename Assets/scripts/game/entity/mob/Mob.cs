using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
	[RequireComponent (typeof(Animator))]
	public class Mob : MonoBehaviour
	{



		protected MobCharacteristics mobChar;

		public List<Ragdoll> bodyparts;

		protected Animator animator;

		protected GameController gameController;



		protected void Start()
		{
			Initialize();
		}


		protected void Initialize()
		{
			mobChar = new MobCharacteristics();
			gameController = FindObjectOfType<GameController>();

		}

		public virtual void DoDamage(float damage)
		{
			mobChar.SetFloat("Health", mobChar.GetFloat("Health") - damage);
			if (isDead())
			{


				if (bodyparts.Count > 0)
				{
					foreach (Ragdoll rd in bodyparts)
					{
						rd.EnableRagdoll = true;
					}
				}



				//Destroy(gameObject);
			}
		}

		public bool isDead()
		{
			return mobChar.GetFloat("Health") <= 0;
		}


		public float GetFloat(string id)
		{
			return mobChar.GetFloat(id);
		}

		public void SetFloat(string id, float value)
		{
			mobChar.SetFloat(id, value);
		}


		public GameController GetGameController()
		{
			return gameController;
		}
	}

}


