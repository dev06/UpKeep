using UnityEngine;
using System.Collections;

namespace Game
{

	/// <summary>
	/// ItemManager : Manages in game items. Used for Using or Dropping Items
	/// </summary>
	public class ItemManager
	{
		/// <summary>
		/// Uses an item
		/// </summary>
		/// <param name="item"></param>
		public static void UseItem(Item item)
		{
			if (item.objectType == ObjectType.Consumable)
			{
				// when using an item only remove the item from inventory if that item is consumable
				InventoryManager.Instance.RemoveItem(item);
			}

			if (EventManager.OnUseItem != null)
			{
				EventManager.OnUseItem(item);
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
	}
}
