using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Game;
using UnityEngine.UI;
namespace UI
{

	public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {



		public bool hovering;
		private ItemObject item;
		private Image itemSprite;
		public GameObject descriptionContainer;

		private DescriptionContainer container;
		void Start()
		{
			container = FindObjectOfType<DescriptionContainer>();
			itemSprite = transform.GetChild(0).GetComponent<Image>();

		}


		public virtual void OnPointerClick(PointerEventData data)
		{
			if (container != null)
			{
				if (!isEmpty())
				{
					InventoryManager.Instance.selectedSlot = this;
					if (EventManager.OnSlotSelect != null)
					{
						EventManager.OnSlotSelect(InventoryManager.Instance.selectedSlot);
						container.SetItemObject(item);
					}
				}
			}
		}


		public virtual void OnPointerEnter(PointerEventData data)
		{
			hovering = true;

			if (container != null)
			{

				if (InventoryManager.Instance.selectedSlot == null)
				{
					if (!isEmpty())
					{
						container.Show();
						container.SetItemObject(item);
					}

				}

			}

		}

		public virtual void OnPointerExit(PointerEventData data)
		{
			hovering = false;
			if (InventoryManager.Instance.selectedSlot == null)
			{
				container.Hide();
				container.SetItemObject(null);

			}
		}


		public void UpdateSlotItemQuantity(int quantity)
		{
			item.quantity += quantity;
			if (item.quantity <= 0)
			{
				container.Hide();
				container.SetItemObject(null);
				InventoryManager.Instance.selectedSlot = null;
				SetItem(null);
			}
		}


		public bool isEmpty()
		{
			return item == null;
		}

		public void SetItem(ItemObject item)
		{
			this.item = item;

			if (itemSprite == null)
			{
				itemSprite = transform.GetChild(0).GetComponent<Image>();
			}
			if (item != null)
			{
				itemSprite.enabled = true;
				itemSprite.sprite = item.sprite;
				UpdateSlotItemQuantity(1);

			}
			else
			{
				itemSprite.enabled = false;
			}
		}

		public ItemObject GetSlotItem()
		{
			return item;
		}

	}

}
