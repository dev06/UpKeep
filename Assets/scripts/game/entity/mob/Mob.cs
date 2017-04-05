using UnityEngine;
using System.Collections;

namespace Game
{
	[RequireComponent (typeof(Animator))]
	public class Mob : MonoBehaviour
	{



		protected MobCharacteristics mobChar;



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

		public void DoDamage(float damage)
		{
			mobChar.SetFloat("Health", mobChar.GetFloat("Health") - damage);
			if (isDead())
			{
				Destroy(gameObject);
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

	}
}


