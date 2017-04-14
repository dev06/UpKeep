using UnityEngine;
using System.Collections;


namespace Game
{
	[RequireComponent (typeof(Player))]
	public class PlayerWeaponController : MonoBehaviour {


		private Player player;

		public Weapon equippedWeapon;

		public void Initialize()
		{
			player = GetComponent<Player>();
		}


		public void EquipWeapon(Weapon w, Transform itemInHand)
		{
			SetEquippedWeapon(w);
			ObjectSpawnerController.SpawnObjectInHand(w.objectID, itemInHand);
		}

		public void UnequipWeapon()
		{
			SetEquippedWeapon(null);
		}



		public void Attack(Transform transform)
		{
			if (equippedWeapon == null) { return; }
			equippedWeapon.GetEWeaponObject().Attack(equippedWeapon, transform);

			if (EventManager.OnFireWeapon != null)
			{
				EventManager.OnFireWeapon(equippedWeapon);
			}
		}




		public void SetEquippedWeapon(Weapon w)
		{
			this.equippedWeapon = w;
		}


		public Weapon GetWeapon()
		{
			return equippedWeapon;
		}



	}

}
