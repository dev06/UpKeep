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
		public static ManageItem OnEquipItem;


		public delegate void QuickSlot();
		public static QuickSlot OnEquip; // called when primary / secondary button are clicked

		public delegate void ManageQuickSlotItem(Item item);
		public static ManageQuickSlotItem OnQuickSlotPressed; // called when the user clicks on the quick slot icons



		public delegate void ManageWeapon(Weapon weapon);
		public static ManageWeapon OnFireWeapon;







	}

}
