using UnityEngine;
using System.Collections;
using Game;

namespace GameUtility
{
	public class MasterVar
	{
		private const float constantColor = 255f;
		public static Color SkyColor = new Color(92f / constantColor, 175 / constantColor, 255f / constantColor, 1.0f);



		public static ItemObject[] ItemObjectList = new ItemObject[3]
		{
			new ItemObject(0, ItemName.Snackbar, ItemType.Consumable, 0, "Description", Resources.Load<Sprite>("textures/objects/item/cosumable/snackbar"), (GameObject)Resources.Load("prefab/objects/item/cosumable/snackbar")),
			new ItemObject(1, ItemName.Soda, ItemType.Consumable, 0, "This is a soda", Resources.Load<Sprite>("textures/objects/item/cosumable/soda"), (GameObject)Resources.Load("prefab/objects/item/cosumable/soda")),
			new ItemObject(2, ItemName.Gun, ItemType.Weapon, 0, "This is a soda", Resources.Load<Sprite>("textures/objects/item/cosumable/soda"), (GameObject)Resources.Load("prefab/objects/weapon/gun_1"))

		};
	}

}
