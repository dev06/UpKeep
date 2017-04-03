using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
namespace UI
{
	public class DescriptionContainer : MonoBehaviour {


		public Text itemName;
		public Text itemDesc;
		public Image itemIcon;
		public Text itemQuantity;
		private ItemObject itemObject;



		void Start ()
		{
			itemIcon = transform.GetChild(0).GetComponent<Image>();
			itemName = transform.GetChild(1).GetComponent<Text>();
			itemDesc = transform.GetChild(3).GetComponent<Text>();
			itemQuantity = transform.GetChild(2).GetComponent<Text>();

		}

		void Update ()
		{
			if (itemObject != null)
			{
				itemName.text = itemObject.name + "";
				itemDesc.text = itemObject.description + "";
				itemQuantity.text = itemObject.quantity + "";
				itemIcon.sprite = itemObject.sprite;


			} else
			{

				itemName.text = "";
				itemDesc.text = "";
				itemQuantity.text = "";
				Hide();
			}

		}

		public void SetItemObject(ItemObject itmObject)
		{
			this.itemObject = itmObject;

		}


		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}


		void OnDisable()
		{
			SetItemObject(null);
			Hide();
		}
	}

}
