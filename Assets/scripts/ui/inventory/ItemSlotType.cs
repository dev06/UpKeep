using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
using UpkeepInput;
namespace UI
{
	public class ItemSlotType : MonoBehaviour {


		public static ItemSlotType currentQuickSlot;

		public enum SlotType
		{
			PRIMARY,
			SECONDARY,
		}

		public SlotType slotType;

		public Item slotItem;

		private Image image;



		void Start ()
		{
			image = transform.GetChild(1).GetComponent<Image>();
		}





		public bool IsSlotEmpty()
		{
			return GetItem() == null;
		}

		void SetSprite(Item item)
		{
			image.enabled = item != null;
			if (item == null) return;
			image.sprite = item.objectSprite;
		}

		public void SetItem(Item item)
		{
			this.slotItem = item;
			SetSprite(item);
		}

		Item GetItem()
		{
			return slotItem;
		}
	}
}
