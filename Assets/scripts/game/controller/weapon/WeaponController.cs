using UnityEngine;
using System.Collections;

namespace Game
{
	/// <summary>
	/// OBSOLETE : DO NOT USE
	/// </summary>
	public class WeaponController : MonoBehaviour {

		public static Weapon EquippedWeapon;

		public static void EquipWeapon(Weapon weapon, Transform itemInHand)
		{
			ObjectSpawnerController.SpawnObjectInHand(weapon.objectID, itemInHand);
			EquippedWeapon = weapon;
		}

		public static void Attack(Transform parent)
		{
			if (EquippedWeapon != null)
			{
				EquippedWeapon.eWeaponObject.TriggerMuzzle();
				Ray ray = new Ray(parent.position, parent.forward);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray.origin, parent.forward * EquippedWeapon.range, out hitInfo))
				{
					GameObject hitObject = hitInfo.transform.gameObject;

					if (hitObject.GetComponent<Mob>() != null)
					{
						Mob mob = (Mob)hitObject.GetComponent<Mob>();
						mob.DoDamage(EquippedWeapon.damage);
					}

					if (hitObject.GetComponent<Rigidbody>() != null)
					{
						hitObject.GetComponent<Rigidbody>().velocity = parent.forward * 100.0f;
					}
				}
			}
		}
	}
}
