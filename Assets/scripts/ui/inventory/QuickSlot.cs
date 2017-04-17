using UnityEngine;
using System.Collections;
using Game;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace UI
{
	public class QuickSlot : MonoBehaviour, IPointerClickHandler
	{

		public static QuickSlot ActiveSlot;

		public Color ActiveSlotColor;
		private Color defaultColor;

		public enum QuickSlotID
		{
			PRIMARY,
			SECONDARY
		}

		public QuickSlotID slotId;
		private Image image;
		private Image background;
		public Item item;


		void Start()
		{
			background = transform.GetChild(0).GetComponent<Image>();

			image = transform.GetChild(1).GetComponent<Image>();

			if (slotId == QuickSlotID.PRIMARY)
			{
				ActiveSlot = this;
			}

			defaultColor = background.color;
		}


		void Update()
		{

			if (ActiveSlot == this)
			{
				background.color = ActiveSlotColor;
			} else
			{
				background.color = defaultColor;
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
			if (item == null) { return; }
			image.sprite = item.objectSprite;
		}


		public Item GetItem() {
			return item;
		}



		public virtual void OnPointerClick(PointerEventData data)
		{
			ActiveSlot = this;
			if (EventManager.OnQuickSlotPressed != null)
			{
				EventManager.OnQuickSlotPressed(GetItem());
			}
		}
	}

}

