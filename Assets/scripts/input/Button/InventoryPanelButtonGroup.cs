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
			DROP
		}

		public ButtonId buttonId;

		void Start () {
			base.Start();
		}

		void Update () {

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
			}
		}
	}
}
