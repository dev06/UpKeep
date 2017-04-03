using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;

namespace UI
{
	public class InventoryActionContainer : MonoBehaviour {

		private GameObject container;

		void OnEnable()
		{
			EventManager.OnSlotSelect += OnSlotSelect;
		}

		void OnDisable()
		{
			EventManager.OnSlotSelect -= OnSlotSelect;
			InventoryManager.Instance.selectedSlot = null;
			Hide();
		}



		void Start ()
		{
			container = transform.GetChild(0).gameObject;
			Hide();
		}

		void Update ()
		{

		}

		void OnSlotSelect(Slot slot)
		{
			container.SetActive(true);
		}

		void Hide()
		{
			container.SetActive(false);
		}
	}

}
