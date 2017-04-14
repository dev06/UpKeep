using UnityEngine;
using System.Collections;
using Game;
using UnityEngine.UI;

namespace UI
{
	public class QuickSlot : MonoBehaviour
	{

		public static QuickSlot ActiveSlot;

		public enum QuickSlotID
		{
			PRIMARY,
			SECONDARY
		}

		public QuickSlotID slotId;
		private Image image;
		public Item item;

		void Start()
		{
			image = transform.GetChild(1).GetComponent<Image>();

			if (slotId == QuickSlotID.PRIMARY)
			{
				ActiveSlot = this;
			}
		}

		public bool IsEmpty()
		{
			return item == null;
		}


		public void SetItem(Item item)
		{
			this.item = item;
			image.enabled = item != null;
			if (item == null) return;
			image.sprite = item.objectSprite;
		}


	}

}

