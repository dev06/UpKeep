using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtility;
namespace Game
{
	public class ItemObjectController : MonoBehaviour {





		private ItemObjectSpawner[] spawnLocations;
		private Player player;

		void OnEnable()
		{
			EventManager.OnUseItem += OnUseItem;
			EventManager.OnDropItem += OnDropItem;
		}

		void OnDisable()
		{
			EventManager.OnUseItem -= OnUseItem;
			EventManager.OnDropItem -= OnDropItem;

		}


		void Start ()
		{
			spawnLocations = FindObjectsOfType<ItemObjectSpawner>();
			player = FindObjectOfType<Player>();
			SpawnItemObjects();
		}


		private void SpawnItemObjects()
		{
			for (int i = 0; i < spawnLocations.Length; i++)
			{
				if (spawnLocations[i].item == null)
				{
					int id = Random.Range(0, MasterVar.ItemObjectList.Length + 1);
					//Game.SpawnItemObject.SpawnItem(id, spawnLocations[i].transform);
					Game.SpawnItemObject.SpawnItemInWorld(id, spawnLocations[i].transform);
				}
			}
		}



		void OnUseItem(ItemObject item)
		{




			if (item.type == ItemType.Consumable)
			{
				switch (item.id)
				{
					case 0:
					{
						player.SetFloat("Hunger", player.GetFloat("Hunger") +  10f);
						break;
					}

					case 1:
					{
						player.SetFloat("Stamina", player.GetFloat("Stamina") + 10f);
						break;
					}

					InventoryManager.Instance.RemoveItem(item);
				}
			} else if (item.type == ItemType.Weapon)
			{
				switch (item.id)
				{
					case 2:
					{
						if (player.itemInHand.itemType == ItemType.None)
						{
							Game.SpawnItemObject.SpawnItemInHand(item.id, player.itemInHand.transform);
							player.itemInHand.itemType = item.type;
						}

						break;
					}
				}

			}
		}


		void OnDropItem(ItemObject item)
		{
			Game.SpawnItemObject.SpawnItemInRelationTo(item.id, player.transform);
			InventoryManager.Instance.RemoveItem(item);
			if (item.type == ItemType.Weapon)
			{
				if (player.itemInHand.transform.childCount > 0) {
					Destroy(player.itemInHand.transform.GetChild(0).gameObject);
				}
				player.itemInHand.itemType = ItemType.None;
			}
		}
	}
}
