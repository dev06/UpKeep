using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;

namespace UI
{
	[RequireComponent (typeof(CanvasGroup))]
	public class InventoryActionContainer : MonoBehaviour {

		private CanvasGroup canvasGroup;

		private CanvasGroup ItemUseContainer;
		void OnEnable()
		{
			EventManager.OnSlotSelect += OnSlotSelect;
			EventManager.OnUpdateInventoryUI += OnUpdateInventoryUI;
			EventManager.OnQuickSlotPressed += OnQuickSlotPressed;
		}

		void OnDisable()
		{
			EventManager.OnSlotSelect -= OnSlotSelect;
			EventManager.OnUpdateInventoryUI -= OnUpdateInventoryUI;
			EventManager.OnQuickSlotPressed -= OnQuickSlotPressed;
			Hide(canvasGroup);
			Hide(ItemUseContainer);
		}



		void Start ()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			ItemUseContainer = transform.GetChild(1).GetComponent<CanvasGroup>();
			Hide(canvasGroup);
			Hide(ItemUseContainer);
		}

		void OnSlotSelect(Slot slot)
		{
			if (slot != null)
			{
				if (slot.isEmpty())
				{
					Hide(canvasGroup);
				} else
				{
					Show(canvasGroup);
				}
				UpdateItemUseContainer(slot.GetItem());
			} else
			{
				Hide(canvasGroup);
			}


		}

		void OnQuickSlotPressed(Item item)
		{

			UpdateItemUseContainer(item);
		}

		public	void UpdateItemUseContainer(Item item)
		{

			if (item == null || !(item is Consumable))
			{
				Hide(ItemUseContainer);
			} else
			{
				if (item is Consumable)
				{
					Show(ItemUseContainer);
				}
			}
		}


		void OnUpdateInventoryUI(Item item)
		{
			if (item == null)
			{
				Hide(canvasGroup);
			}

		}

		void Hide(CanvasGroup group)
		{
			group.alpha = 0;
			group.blocksRaycasts = false;
		}

		void Show(CanvasGroup group)
		{
			group.alpha = 1;
			group.blocksRaycasts = true;
		}
	}

}
