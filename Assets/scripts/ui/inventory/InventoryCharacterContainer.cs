using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
using UpkeepInput;
namespace UI
{
	public class InventoryCharacterContainer : MonoBehaviour {


		public InventoryPanelButtonGroup buttonGroup;
		public InventoryActionContainer actionContainer;
		public GameObject QuickSlotContainer;
		public QuickSlot[] quickSlots = new QuickSlot[2];

		Player player;

		void OnEnable()
		{
			EventManager.OnEquipItem += OnEquipItem;
			EventManager.OnObjectPickup += OnObjectPickup;
			EventManager.OnDropItem += OnDropItem;
			EventManager.OnQuickSlotPressed += OnQuickSlotPressed;
			EventManager.OnUseItem += OnUseItem;
		}

		void OnDisable()
		{
			EventManager.OnEquipItem -= OnEquipItem;
			EventManager.OnObjectPickup -= OnObjectPickup;
			EventManager.OnDropItem -= OnDropItem;
			EventManager.OnQuickSlotPressed -= OnQuickSlotPressed;
			EventManager.OnUseItem -= OnUseItem;
		}

		void Start()
		{

			PopulateQuickSlot();
			player = FindObjectOfType<Player>();
			buttonGroup = FindObjectOfType<InventoryPanelButtonGroup>();
			actionContainer = FindObjectOfType<InventoryActionContainer>();
		}

		void PopulateQuickSlot()
		{
			for (int i = 0; i < quickSlots.Length; i++)
			{
				quickSlots[i] = QuickSlotContainer.transform.GetChild(i).GetComponent<QuickSlot>();
			}
		}

		void Update()
		{

			if (player.gameInputManager.GetKeyDown(KeyCode.Alpha1))
			{
				QuickSlot.ActiveSlot = quickSlots[0];
				player.EquipItem(QuickSlot.ActiveSlot.GetItem());
				actionContainer.UpdateItemUseContainer(QuickSlot.ActiveSlot.GetItem());


			} else if (player.gameInputManager.GetKeyDown(KeyCode.Alpha2))
			{
				QuickSlot.ActiveSlot = quickSlots[1];
				player.EquipItem(QuickSlot.ActiveSlot.GetItem());
				actionContainer.UpdateItemUseContainer(QuickSlot.ActiveSlot.GetItem());

			}


			if (player.gameInputManager.GetKeyDown(GameInputManager.ITEM_USE_KEYCODE))
			{
				if (QuickSlot.ActiveSlot.GetItem() is Consumable)
				{
					ItemManager.UseItem(QuickSlot.ActiveSlot.GetItem());
				}
			}
		}

		void OnObjectPickup(Game.Object obj)
		{
			if (!(obj is Item)) { return; }

			Item pickedItem = (Item)obj;

			if (GetNextSlot() != null)
			{
				GetNextSlot().SetItem(pickedItem);
				player.EquipItem(QuickSlot.ActiveSlot.GetItem());
			}
		}


		void OnEquipItem(Item item)
		{
			if (item == null) {

				return;
			}

			if (QuickSlot.ActiveSlot.GetItem() == item)
			{
				player.EquipItem(null);
				QuickSlot.ActiveSlot.SetItem(null);
				buttonGroup.UpdateText();
				return;
			}


			for (int i = 0; i < quickSlots.Length; i++)
			{
				if (quickSlots[i].GetItem() == item && quickSlots[i].GetItem().objectQuantity <= 1)
				{
					quickSlots[i].SetItem(null);

				}
			}

			QuickSlot.ActiveSlot.SetItem(item);
			player.EquipItem(QuickSlot.ActiveSlot.GetItem());
			actionContainer.UpdateItemUseContainer(QuickSlot.ActiveSlot.GetItem());
			buttonGroup.UpdateText();
		}


		void OnDropItem(Item item)
		{
			for (int i = 0; i < quickSlots.Length; i++)
			{
				if (quickSlots[i].GetItem() == null) { continue; }
				if (quickSlots[i].GetItem().objectID == item.objectID && quickSlots[i] == QuickSlot.ActiveSlot)
				{
					quickSlots[i].SetItem(null);
				}

				if (item.objectQuantity <= 0)
				{
					if (quickSlots[i].GetItem() != null && item != null)
					{
						if (quickSlots[i].GetItem().objectID == item.objectID)
						{
							quickSlots[i].SetItem(null);
						}
					}
				}
			}
			buttonGroup.UpdateText();
			actionContainer.UpdateItemUseContainer(QuickSlot.ActiveSlot.GetItem());


		}

		void OnUseItem(Item item)
		{
			if (item is Consumable)
			{


				for (int i = 0; i < quickSlots.Length; i++)
				{
					if (quickSlots[i].GetItem() == null) { continue; }

					if (item.objectQuantity >= 1)
					{
						if (quickSlots[i].GetItem().objectID == item.objectID && quickSlots[i] == QuickSlot.ActiveSlot)
						{
							quickSlots[i].SetItem(null);
						}
					} else
					{
						if (quickSlots[i].GetItem().objectID == item.objectID)
						{
							quickSlots[i].SetItem(null);
							break;
						}
					}

				}



				buttonGroup.UpdateText();
				actionContainer.UpdateItemUseContainer(QuickSlot.ActiveSlot.GetItem());
				player.EquipItem(QuickSlot.ActiveSlot.GetItem());
			}
		}


		void OnQuickSlotPressed(Item item)
		{
			player.EquipItem(QuickSlot.ActiveSlot.GetItem());
			buttonGroup.UpdateText();


		}

		QuickSlot GetNextSlot()
		{
			for (int i = 0; i < quickSlots.Length; i++)
			{
				if (quickSlots[i].IsEmpty())
				{
					return quickSlots[i];
				}
			}
			return null;
		}
	}

}
