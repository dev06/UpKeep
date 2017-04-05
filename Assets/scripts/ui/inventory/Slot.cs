using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Game;
using UnityEngine.UI;
namespace UI
{

	public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {


		public static Slot selectedSlot;
		public bool hovering;
		public Item item;

		private Image objectImage;
		private static DescriptionContainer container;



		void Start()
		{
			container = FindObjectOfType<DescriptionContainer>();
		}

		void OnDisable()
		{

			if (selectedSlot != null)
			{
				selectedSlot = null;
			}

			if (container != null)
			{
				container.Hide();
			}
		}


		public virtual void OnPointerClick(PointerEventData data)
		{
			selectedSlot = this;

			if (selectedSlot.isEmpty())
			{
				container.Hide();
				selectedSlot = null;
			} else
			{
				container.Show();
				container.UpdateContents(selectedSlot.item);
			}
			if (container != null && selectedSlot != null)
			{
				container.UpdateContents(selectedSlot.item);
			}
			if (EventManager.OnSlotSelect != null)
			{
				EventManager.OnSlotSelect(selectedSlot);
			}


		}


		public virtual void OnPointerEnter(PointerEventData data)
		{
			hovering = true;

			if (isEmpty() == false && container != null && selectedSlot == null)
			{
				container.UpdateContents(item);
				container.Show();
			}
		}

		public virtual void OnPointerExit(PointerEventData data)
		{
			hovering = false;

			if (selectedSlot == null)
			{
				if (container != null)
				{
					container.Hide();
				}
			}
		}


		public void SetSlotObject(Item item)
		{
			this.item = item;
			UpdateSlotContents();
		}


		public void SetSlotObjectQuantity(int quantity)
		{
			if (item == null) return;
			item.objectQuantity += quantity;

			if (container != null)
			{
				container.UpdateContents(item);
			}


			if (item.objectQuantity < 1)
			{
				SetSlotObject(null);
				UpdateSlotContents();
			}
		}


		public void UpdateSlotContents()
		{
			if (item != null)
			{
				if (objectImage == null) objectImage = transform.GetChild(0).GetComponent<Image>();
				objectImage.sprite = item.objectSprite;

			}
			else
			{
				objectImage.sprite = null;
				if (container != null)
				{
					container.Hide();
				}
			}


			if (EventManager.OnUpdateInventoryUI != null)
			{
				EventManager.OnUpdateInventoryUI(item);
			}

			objectImage.enabled = !isEmpty();

		}

		public bool isEmpty() {
			return item == null;
		}
	}

}
