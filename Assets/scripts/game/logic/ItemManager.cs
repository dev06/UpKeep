using UnityEngine;
using System.Collections;

namespace Game
{

	/// <summary>
	/// ItemManager : Manages in game items. Used for Using or Dropping Items
	/// </summary>
	public class ItemManager
	{

		public static Item currentItemInHand;

		/// <summary>
		/// Uses an item
		/// </summary>
		/// <param name="item"></param>
		public static void EquipItem(Item item)
		{

			if (EventManager.OnEquipItem != null)
			{
				EventManager.OnEquipItem(item);
			}


		}


		/// <summary>
		/// Drops an Item
		/// </summary>
		/// <param name="item"></param>
		public static void DropItem(Item item)
		{
			InventoryManager.Instance.RemoveItem(item);

			if (EventManager.OnDropItem != null)
			{
				EventManager.OnDropItem(item);
			}


		}


		public static void UseItem(Item item)
		{




			if (EventManager.OnUseItem != null)
			{
				EventManager.OnUseItem(item);
			}



		}


		public static void EquipItemInHand(Item item, Transform itemInHand)
		{
			ObjectSpawnerController.SpawnObjectInHand(item.objectID, itemInHand);
		}




	}
}
