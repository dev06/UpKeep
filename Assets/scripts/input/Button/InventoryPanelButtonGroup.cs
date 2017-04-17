using UnityEngine;
using System.Collections;
using Game;
using system;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UI;

namespace UpkeepInput
{
	public class InventoryPanelButtonGroup : ButtonGroup {

		public enum ButtonId
		{
			EQUIP,
			DROP,
			USE,
		}

		public ButtonId buttonId;


		void OnEnable()
		{

			EventManager.OnSlotSelect += OnSlotSelect;
		}

		void OnDisable()
		{
			EventManager.OnSlotSelect -= OnSlotSelect;
		}
		void Start ()
		{
			base.Start();

		}

		public override void OnPointerEnter(PointerEventData data)
		{
			base.OnPointerEnter(data);

		}

		public override void OnPointerExit(PointerEventData data)
		{
			base.OnPointerExit(data);

		}

		void Update()
		{

		}

		public override void OnPointerClick(PointerEventData data)
		{
			base.OnPointerClick(data);
			RegisterClick();
		}

		private void RegisterClick()
		{
			base.RegisterClick();

			switch (buttonId)
			{
				case ButtonId.EQUIP:
				{

					if (Slot.selectedSlot != null)
					{
						ItemManager.EquipItem(Slot.selectedSlot.item);
					}

					UpdateText();

					break;
				}

				case ButtonId.DROP:
				{
					if (Slot.selectedSlot != null)
					{
						ItemManager.DropItem(Slot.selectedSlot.item);
					}
					break;
				}
				case ButtonId.USE:
				{
					ItemManager.UseItem(Slot.selectedSlot.item);
					break;
				}
			}
		}


		void OnSlotSelect(Slot s)
		{
			if (Slot.selectedSlot == null) { return; }
			if (Slot.selectedSlot.GetItem() == null) { return; }
			UpdateText();
		}

		public void UpdateText()
		{

			if (buttonId == ButtonId.EQUIP) {


				Text text = transform.GetChild(0).GetComponent<Text>();


				if (QuickSlot.ActiveSlot.GetItem() == null)
				{
					text.text = "Equip";
				} else
				{
					if (QuickSlot.ActiveSlot.GetItem().objectID != Slot.selectedSlot.GetItem().objectID)
					{
						text.text = "Equip";
					} else
					{
						text.text = "Unequip";
					}
				}
			}
		}

	}
}
