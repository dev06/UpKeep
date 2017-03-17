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

	}

}
