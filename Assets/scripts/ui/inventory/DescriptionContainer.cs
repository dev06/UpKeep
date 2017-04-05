using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
namespace UI
{
	[RequireComponent (typeof(CanvasGroup))]
	public class DescriptionContainer : MonoBehaviour {

		public Text itemName;
		public Text itemDesc;
		public Image itemIcon;
		public Text itemQuantity;

		private CanvasGroup canvasGroup;

		void Start ()
		{
			itemIcon = transform.GetChild(0).GetComponent<Image>();
			itemName = transform.GetChild(1).GetComponent<Text>();
			itemDesc = transform.GetChild(3).GetComponent<Text>();
			itemQuantity = transform.GetChild(2).GetComponent<Text>();
			canvasGroup = GetComponent<CanvasGroup>();
			Hide();
		}

		public void UpdateContents(Item item)
		{
			if (item == null) return;
			itemIcon.sprite = item.objectSprite;
			itemDesc.text = item.objectDescription;
			itemName.text = item.objectName;
			itemQuantity.text = item.objectQuantity + "";
		}


		public void Hide()
		{
			canvasGroup.alpha = 0;
			canvasGroup.blocksRaycasts = false;
		}

		public void Show()
		{
			canvasGroup.alpha = 1;
			canvasGroup.blocksRaycasts = true;
		}
	}

}
