using UnityEngine;
using System.Collections;


namespace Game
{
	public class ItemInHand : MonoBehaviour
	{

		private Item item;

		public void SetItem(Item item)
		{
			this.item = item;
			if (this.item == null)
			{
				if (transform.childCount > 0) {
					Destroy(transform.GetChild(0).gameObject);
				}
			}

		}

		public Item GetItem()
		{
			return item;
		}

		public Transform GetTransform()
		{
			if (transform.childCount > 0)
			{
				return transform.GetChild(0).transform;
			}

			return null;
		}



	}

}
