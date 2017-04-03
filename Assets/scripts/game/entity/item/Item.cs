using UnityEngine;
using System.Collections;

namespace Game
{

	public enum ItemType
	{
		None,
		Consumable,
		Weapon,
	}

	public enum ItemName
	{
		None,
		Snackbar,
		Soda,
		Gun,
	}

	public class Item : Entity
	{
		public int id;

		public bool atLocalPosition;
		public Vector3 localPosition;


		void FixedUpdate()
		{
			if (atLocalPosition)
			{
				transform.localPosition = localPosition;
			}
		}
	}

	public class ItemObject
	{

		public GameObject prefab;
		public Sprite sprite;
		public ItemName name;
		public ItemType type;
		public string description;
		public int quantity;
		public int id;


		public ItemObject() {}

		public ItemObject(int id, ItemName itemName, ItemType itemType, int quantity, string description, Sprite sprite, GameObject prefab = null)
		{
			this.id = id;
			this.name = itemName;
			this.type = itemType;
			this.quantity = quantity;
			this.description = description;
			this.sprite = sprite;
			this.prefab = prefab;
		}
	}

}
