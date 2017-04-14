using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
using UpkeepInput;
namespace UI
{
	public class InventoryCharacterContainer : MonoBehaviour {



		public GameObject QuickSlotContainer;
		public QuickSlot[] quickSlots = new QuickSlot[2];



		void OnEnable()
		{
			EventManager.OnUseItem += OnEquipItem;
		}

		void OnDisable()
		{
			EventManager.OnUseItem -= OnEquipItem;
		}

		void Start()
		{
			for (int i = 0; i < quickSlots.Length; i++)
			{
				quickSlots[i] = QuickSlotContainer.transform.GetChild(i).GetComponent<QuickSlot>();
			}
		}


		void OnEquipItem(Item item)
		{
			for (int i = 0; i < quickSlots.Length; i++)
			{
				if (quickSlots[i].IsEmpty())
				{
					quickSlots[i].SetItem(item);
					break;
				}
			}
		}


	}

}
