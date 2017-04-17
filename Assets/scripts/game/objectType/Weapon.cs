using UnityEngine;
using System.Collections;


namespace Game
{
	public class Weapon : Item
	{

		public float damage;
		public float range;
		public float recoil;
		public float recoilSpeed;
		public float forwardRecoilMult;
		public float upwardRecoilMult;
		public float noise;
		public float aimFOV;

		public EWeaponObject eWeaponObject;

		public Weapon()
		{

		}

		public Weapon(string damage, string range)
		{
			this.damage = float.Parse(damage);
			this.range = float.Parse(range);
		}


		public EWeaponObject GetEWeaponObject()
		{
			return eWeaponObject;
		}

	}

}
