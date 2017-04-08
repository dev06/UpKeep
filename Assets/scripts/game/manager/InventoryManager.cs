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
		private int row = 6;
		private int column = 7;
		private GameObject SlotPrefab;
		public Transform itemContainer;
		public GameObject descriptionContainer;


		void OnEnable()
		{
			EventManager.OnObjectPickup += OnObjectPickup;
		}

		void OnDisable()
		{
			EventManager.OnObjectPickup -= OnObjectPickup;
		}

		void OnObjectPickup(Game.Object obj)
		{
			AddItem((Item)obj);
		}

		void OnDropObject(Game.Object obj)
		{
			RemoveItem((Item)obj);
		}

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
			if (itemContainer == null) { return; }
			float xSpacing = 0.95f;
			float ySpacing = 0.95f;

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
					inventorySlots.Add(slot.GetComponent<Slot>());
				}
			}
		}

		public void AddItem(Item item)
		{
			for (int i = 0; i < inventorySlots.Count; i++)
			{
				Slot slot = inventorySlots[i];

				if (slot.isEmpty())
				{
					slot.SetSlotObject(item);
					slot.SetSlotObjectQuantity(1);

					break;
				} else
				{
					if (slot.item.objectID == item.objectID)
					{
						slot.SetSlotObjectQuantity(1);
						break;
					}
				}
			}
		}

		public bool Contains(Item item)
		{
			for (int i = 0; i < inventorySlots.Count; i++)
			{
				if (inventorySlots[i].isEmpty()) { continue; }
				if (inventorySlots[i].item.objectID == item.objectID)
				{
					return true;
				}
			}
			return false;
		}

		public void RemoveItem(Item item)
		{
			for (int i = 0; i < inventorySlots.Count; i++)
			{
				Slot slot = inventorySlots[i];
				if (!slot.isEmpty())
				{
					if (slot.item.objectID == item.objectID)
					{
						slot.SetSlotObjectQuantity(-1);
						break;
					}
				}
			}
		}
	}
}
