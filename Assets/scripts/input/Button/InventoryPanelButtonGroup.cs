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
			PRIMARY,
			SECONDARY,
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

		public override void OnPointerClick(PointerEventData data)
		{
			base.OnPointerClick(data);
			RegisterClick();
			UpdateEquipButton();
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
						ItemManager.UseItem(Slot.selectedSlot.item);
					}
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

				case ButtonId.PRIMARY:
				{
					if (Slot.selectedSlot != null)
					{
						if (EventManager.OnUpdateQuickItemSlot != null)
						{
							EventManager.OnUpdateQuickItemSlot(Slot.selectedSlot.item, buttonId);
						}

					}

					break;
				}

				case ButtonId.SECONDARY:
				{
					if (Slot.selectedSlot != null)
					{
						if (EventManager.OnUpdateQuickItemSlot != null)
						{
							EventManager.OnUpdateQuickItemSlot(Slot.selectedSlot.item, buttonId);
						}
					}

					break;
				}
			}
		}


		void OnSlotSelect(Slot s)
		{
			UpdateEquipButton();
		}

		void UpdateEquipButton()
		{
			if (buttonId == ButtonId.EQUIP)
			{
				Slot s = Slot.selectedSlot;
				if (s == null) return;
				if (s.GetItem() == null) return;
				Item itemInHand = s.GetItem();

				Text text = transform.GetChild(0).GetComponent<Text>();

				if (itemInHand is Consumable)
				{
					text.text = "Consume";
					return;
				}

				if (ItemManager.currentItemInHand == null || ItemManager.currentItemInHand != itemInHand)
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
