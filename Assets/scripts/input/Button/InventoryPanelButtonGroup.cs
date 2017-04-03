using UnityEngine;
using System.Collections;
using Game;
using system;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
					if (EventManager.OnUseItem != null)
					{
						if (InventoryManager.Instance.selectedSlot != null)
						{
							EventManager.OnUseItem(InventoryManager.Instance.selectedSlot.GetSlotItem());
						}
					}
					break;
				}

				case ButtonId.DROP:
				{

					if (EventManager.OnDropItem != null)
					{
						if (InventoryManager.Instance.selectedSlot != null)
						{
							EventManager.OnDropItem(InventoryManager.Instance.selectedSlot.GetSlotItem());
						}
					}
					break;
				}
			}
		}

	}

}
