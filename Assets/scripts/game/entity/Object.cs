using UnityEngine;
using System.Collections;

namespace Game
{


	public enum ObjectType
	{
		None,
		Consumable,
		Weapon,
	}


	public class Object
	{

		public int objectID;
		public string objectName;
		public string packagePath;
		public ObjectType objectType;
		public GameObject objectPrefab;
	}


	public class Item : Object
	{
		public Sprite objectSprite;
		public string objectDescription;
		public int objectQuantity;
	}



}
