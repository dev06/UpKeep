using UnityEngine;
using System.Collections;
using UpkeepInput;
using UI;
namespace Game
{
	public class EventManager : MonoBehaviour
	{

		public delegate void StateChange(StateManager.State state);
		public static StateChange SetState;



		public delegate void ObjectPickup(Game.Object obj);
		public static ObjectPickup OnObjectPickup;


		public delegate void SlotSelect(Slot slot);
		public static SlotSelect OnSlotSelect;


		public delegate void InventoryUI(Item item);
		public static InventoryUI OnUpdateInventoryUI;


		public delegate void ManageItem(Item item);
		public static ManageItem OnUseItem;
		public static ManageItem OnDropItem;


		public delegate void ManageQuickItemSlot(Item item, InventoryPanelButtonGroup.ButtonId buttonId);
		public static ManageQuickItemSlot OnUpdateQuickItemSlot;


		public delegate void ManageWeapon(Weapon weapon);
		public static ManageWeapon OnFireWeapon;







	}

}
