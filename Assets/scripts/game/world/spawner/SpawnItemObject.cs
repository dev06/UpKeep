using UnityEngine;
using System.Collections;
using GameUtility;
namespace Game
{
	public class SpawnItemObject : MonoBehaviour
	{



		public static void SpawnItem(int id, Transform parent = null)
		{
			Vector3 offset = Vector3.zero;
			Vector3 scale = Vector3.zero;
			Transform transform;
			for (int i = 0; i < MasterVar.ItemObjectList.Length; i++)
			{
				if (MasterVar.ItemObjectList[i].id == id)
				{
					GameObject clone = (GameObject)Instantiate(MasterVar.ItemObjectList[i].prefab, parent.transform.position, parent.transform.rotation) as GameObject;
					scale = clone.transform.localScale;
					clone.transform.SetParent(parent);
					clone.transform.GetComponent<Item>().atLocalPosition = true;
					clone.transform.GetComponent<Item>().localPosition = new Vector3(0, 0, 0);
					if (clone.transform.childCount > 0)
					{
						transform = clone.transform.GetChild(0).transform;
						clone.transform.localPosition = transform.localPosition;
						clone.transform.localScale = transform.localScale;
						clone.transform.localRotation = transform.localRotation;
					} else
					{
						clone.transform.localPosition = parent.transform.position;
						clone.transform.localScale = scale;
						clone.transform.localRotation = parent.transform.localRotation;
					}
				}
			}
		}
		// public static void SpawnItemInWorld(int id, Transform relationTo)
		// {
		// 	Vector3 offset = relationTo.forward * (Random.Range(2.0f, 2.2f)) + Vector3.right * (Random.Range(-1.0f, 1.0f));

		// 	for (int i = 0; i < MasterVar.ItemObjectList.Length; i++)
		// 	{
		// 		if (MasterVar.ItemObjectList[i].id == id)
		// 		{
		// 			GameObject clone = (GameObject)Instantiate(MasterVar.ItemObjectList[i].prefab, relationTo.position, relationTo.rotation) as GameObject;
		// 			clone.transform.GetComponent<Item>().localPosition = Vector3.zero;
		// 			clone.transform.localPosition = relationTo.position + offset;
		// 			clone.transform.GetComponent<Item>().atLocalPosition = false;
		// 		}
		// 	}
		// }



		/// <summary>
		/// Spawns item with specific id in player's hand
		/// Uses the 0th Gameobject as transform
		/// </summary>
		/// <param name="id"></param>
		/// <param name="hand"></param>
		public static void SpawnItemInHand(int id, Transform hand)
		{

			for (int i = 0; i < MasterVar.ItemObjectList.Length; i++)
			{
				if (MasterVar.ItemObjectList[i].id == id)
				{
					GameObject clone = (GameObject)Instantiate(MasterVar.ItemObjectList[i].prefab) as GameObject;
					Vector3 localPosition = clone.transform.GetChild(0).transform.localPosition;
					Vector3 localScale = clone.transform.GetChild(0).transform.localScale;
					Quaternion localRotation = clone.transform.GetChild(0).transform.localRotation;
					clone.transform.SetParent(hand);
					clone.transform.localPosition = localPosition;
					clone.transform.localScale = localScale;
					clone.transform.localRotation = localRotation;
				}
			}
		}

		public static void SpawnItemInWorld(int id, Transform parent)
		{

			for (int i = 0; i < MasterVar.ItemObjectList.Length; i++)
			{
				if (MasterVar.ItemObjectList[i].id == id)
				{
					GameObject clone = (GameObject)Instantiate(MasterVar.ItemObjectList[i].prefab) as GameObject;
					if (clone.transform.childCount == 0) { return; }
					Vector3 localPosition = clone.transform.GetChild(1).transform.localPosition;
					Vector3 localScale = clone.transform.GetChild(1).transform.localScale;
					Quaternion localRotation = clone.transform.GetChild(1).transform.localRotation;
					clone.transform.SetParent(parent);
					clone.transform.localPosition = localPosition;
					clone.transform.localScale = localScale;
					clone.transform.localRotation = localRotation;
				}
			}
		}


		public static void SpawnItemInRelationTo(int id, Transform relationTo)
		{

			for (int i = 0; i < MasterVar.ItemObjectList.Length; i++)
			{
				if (MasterVar.ItemObjectList[i].id == id)
				{
					GameObject clone = (GameObject)Instantiate(MasterVar.ItemObjectList[i].prefab) as GameObject;
					if (clone.transform.childCount == 0) { return; }

					clone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ
					        | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ  ;
					Vector3 localScale = clone.transform.GetChild(1).transform.localScale;
					Quaternion localRotation = clone.transform.GetChild(1).transform.localRotation;
					clone.transform.position = relationTo.position + (relationTo.forward * 2.0f);
					clone.transform.localScale = localScale;
					clone.transform.localRotation = localRotation;
				}
			}
		}

	}

}
