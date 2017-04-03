using UnityEngine;
using System.Collections;

using UI;
namespace Game
{
	public class EventManager : MonoBehaviour
	{

		public delegate void StateChange(StateManager.State state);
		public static StateChange SetState;


		public delegate void ItemPickup(Item item);
		public static ItemPickup OnItemPickup;



		public delegate void SlotSelect(Slot slot);
		public static SlotSelect OnSlotSelect;


		public delegate void ManageItem(ItemObject itemObject);
		public static ManageItem OnUseItem;
		public static ManageItem OnDropItem;



	}

}
