using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;

namespace UI
{
	[RequireComponent (typeof(CanvasGroup))]
	public class InventoryActionContainer : MonoBehaviour {

		private CanvasGroup canvasGroup;

		void OnEnable()
		{
			EventManager.OnSlotSelect += OnSlotSelect;
			EventManager.OnUpdateInventoryUI += OnUpdateInventoryUI;
		}

		void OnDisable()
		{
			EventManager.OnSlotSelect -= OnSlotSelect;
			EventManager.OnUpdateInventoryUI -= OnUpdateInventoryUI;
			Hide();
		}



		void Start ()
		{
			canvasGroup = GetComponent<CanvasGroup>();
			Hide();
		}

		void OnSlotSelect(Slot slot)
		{
			if (slot != null)
			{
				if (slot.isEmpty())
				{
					Hide();
				} else
				{
					Show();
				}
			} else
			{
				Hide();
			}
		}


		void OnUpdateInventoryUI(Item item)
		{
			if (item == null)
			{
				Hide();
			}
		}

		void Hide()
		{
			canvasGroup.alpha = 0;
			canvasGroup.blocksRaycasts = false;
		}

		void Show()
		{
			canvasGroup.alpha = 1;
			canvasGroup.blocksRaycasts = true;
		}
	}

}
