using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
namespace UI
{
	[RequireComponent (typeof(Image))]
	public class FocusedItem : MonoBehaviour {

		private Image image;
		private Text text;

		public Item item;

		void Start ()
		{
			image =  GetComponent<Image>();
			text = transform.GetChild(0).transform.GetComponent<Text>();
			Hide();
		}

		void Update()
		{
			if (StateManager.Instance.state == StateManager.State.INVENTORY)
			{
				Hide();
			}
		}


		public void Notify(Item item)
		{
			this.item = item;
			image.enabled = true;
			text.text = "Press [F] to pickup " + item.name;
		}

		public void Hide()
		{
			this.item = null;
			image.enabled = false;
			text.text = "";
		}

	}

}
