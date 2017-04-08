using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game;
namespace GameUtility
{
	public class ObjectDatabase
	{
		public static List<Game.Object> objectList = new List<Game.Object>();


		public static void CreateConsumableObject(List<KeyValuePair<string, string>> pair)
		{
			Consumable consumableObject = new Consumable();
			consumableObject.objectID = int.Parse(GetValue(pair, "ID"));
			consumableObject.objectType = (ObjectType) System.Enum.Parse(typeof(ObjectType), GetValue(pair, "Type"));
			consumableObject.objectName = GetValue(pair, "Name");
			consumableObject.packagePath = GetValue(pair, "Location");
			consumableObject.objectDescription = GetValue(pair, "Description");
			consumableObject.objectQuantity = int.Parse(GetValue(pair, "Quantity"));
			consumableObject.hungerGain = float.Parse(GetValue(pair, "HungerGain"));
			consumableObject.healthGain = float.Parse(GetValue(pair, "HealthGain"));
			consumableObject.thirstGain = float.Parse(GetValue(pair, "ThirstGain"));
			consumableObject.staminaGain = float.Parse(GetValue(pair, "StaminaGain"));
			consumableObject.objectPrefab = (GameObject)Resources.Load(consumableObject.packagePath + "/prefab");
			consumableObject.objectSprite = Resources.Load<Sprite>(consumableObject.packagePath + "/sprite");
			if (consumableObject.objectQuantity > 0) { InventoryManager.Instance.AddItem(consumableObject); }
			objectList.Add(consumableObject);
		}


		public static void CreateWeaponObject(List<KeyValuePair<string, string>> pair)
		{
			Weapon weapon = new Weapon();
			weapon.objectID = int.Parse(GetValue(pair, "ID"));
			weapon.objectType = (ObjectType) System.Enum.Parse(typeof(ObjectType), GetValue(pair, "Type"));
			weapon.objectName = GetValue(pair, "Name");
			weapon.packagePath = GetValue(pair, "Location");
			weapon.objectDescription = GetValue(pair, "Description");
			weapon.damage = float.Parse(GetValue(pair, "Damage"));
			weapon.range = float.Parse(GetValue(pair, "Range"));
			weapon.recoil = float.Parse(GetValue(pair, "Recoil"));
			weapon.recoilSpeed = float.Parse(GetValue(pair, "RecoilSpeed"));
			weapon.forwardRecoilMult = float.Parse(GetValue(pair, "ForwardRecoilMult"));
			weapon.upwardRecoilMult = float.Parse(GetValue(pair, "UpwardRecoilMult"));
			weapon.noise = float.Parse(GetValue(pair, "Noise"));

			weapon.objectPrefab = (GameObject)Resources.Load(weapon.packagePath + "/prefab");
			weapon.objectSprite = Resources.Load<Sprite>(weapon.packagePath + "/sprites" +  "/sprite");
			objectList.Add(weapon);

		}


		public static Game.Object GetObject(int id)
		{
			//			if (id > objectList.Count || id < 0) { return objectList[0]; }

			for (int i = 0; i < objectList.Count; i++)
			{
				if (objectList[i].objectID == id)
				{
					return objectList[i];
				}
			}
			return null;
		}



		public static string GetValue(List<KeyValuePair<string, string>> pair, string key)
		{
			for (int i = 0; i < pair.Count; i++)
			{
				if (pair[i].Key == key)
				{
					return pair[i].Value;
				}
			}

			return "";
		}
	}



}
