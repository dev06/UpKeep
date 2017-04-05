using UnityEngine;
using System.Collections;


namespace Game
{
	public class Weapon : Item
	{

		public float damage;
		public float range;

		public Weapon()
		{

		}

		public Weapon(string damage, string range)
		{
			this.damage = float.Parse(damage);
			this.range = float.Parse(range);
		}

	}

}
