using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtility;
using UI;
namespace Game
{
	public class InventoryManager : MonoBehaviour
	{

		public static InventoryManager Instance;
		public List<Slot> inventorySlots = new List<Slot>();
		public Slot selectedSlot;
		public int row = 5;
		public int column = 5;
		private GameObject SlotPrefab;
		public Transform itemContainer;
		public GameObject descriptionContainer;

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}


			Initialize();
			GenerateInventoryUI();
		}


		private void Initialize()
		{
			SlotPrefab = (GameObject)Resources.Load("prefab/ui/inventory/Slot");
		}



		private void GenerateInventoryUI()
		{
			float xSpacing = 1.0f;
			float ySpacing = 1.0f;

			float slotSize = 100f - (100f * (1f - xSpacing));

			int column = InventoryManager.Instance.column;
			int row = InventoryManager.Instance.row;

			float xOffset = (column - 1) * xSpacing * slotSize;
			float yOffset = (row - 1) * ySpacing * slotSize;
			for (int x = 0; x < column; x++)
			{
				for (int y = 0; y < row; y++)
				{
					Vector3 position = new Vector3(x  * xSpacing, y  * ySpacing, 1);
					GameObject slot = (GameObject)Instantiate(SlotPrefab, position, Quaternion.identity);
					slot.transform.SetParent(itemContainer.transform);
					slot.GetComponent<RectTransform>().sizeDelta = new Vector3(slotSize, slotSize, 1);
					slot.GetComponent<RectTransform>().localScale = new Vector3(.95f, .95f, 1);
					slot.transform.localPosition = new Vector3(x * slotSize * xSpacing - xOffset / 2, y * slotSize * ySpacing - yOffset / 2, 1);
					slot.GetComponent<Slot>().descriptionContainer = descriptionContainer;
					inventorySlots.Add(slot.GetComponent<Slot>());
				}
			}


		}



		public void AddItem(Item item)
		{
			ItemObject itemObject = ToItemObject(item);
			for (int i = 0; i < inventorySlots.Count; i++)
			{
				Slot slot = inventorySlots[i];
				if (slot.isEmpty()) // is slot empty?
				{
					slot.SetItem(itemObject); // yes, so add new item
					break;
				} else
				{
					int slotItemID = slot.GetSlotItem().id;
					if (item.id == slotItemID)
					{

						slot.UpdateSlotItemQuantity(1);
						break;
					}
				}
			}
		}

		public void RemoveItem(ItemObject item, int quantity = 1)
		{
			for (int i = 0; i < inventorySlots.Count; i++)
			{
				Slot slot = inventorySlots[i];
				if (!slot.isEmpty())
				{
					if (slot.GetSlotItem().id == item.id)
					{
						if (slot.GetSlotItem().quantity > 0)
						{
							slot.UpdateSlotItemQuantity(-quantity);
						} else
						{
							slot.SetItem(null);
						}
					}
				}
			}
		}




		public ItemObject ToItemObject(Item item)
		{

			for (int i = 0; i < MasterVar.ItemObjectList.Length; i++)
			{
				if (item.id == MasterVar.ItemObjectList[i].id)
				{
					return MasterVar.ItemObjectList[i];
				}
			}

			return null;
		}
	}
}
