using UnityEngine;
using System.Collections;
using GameUtility;

namespace Game
{
	public class ObjectSpawnerController : MonoBehaviour
	{

		/// <summary>
		/// Spawns an object in world with specific parent
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="parent"></param>
		public static void SpawnObjectInWorld(int objectID, Transform parent)
		{
			GameObject prefab = ObjectDatabase.GetObject(objectID).objectPrefab;

			if (prefab != null)
			{
				GameObject clone = Instantiate(prefab) as GameObject;
				clone.transform.SetParent(parent);
				clone.transform.localPosition = parent.transform.position;
				clone.transform.localRotation = parent.transform.rotation;
				clone.transform.localScale = parent.transform.localScale;
			}
		}


		/// <summary>
		/// Spawns an object with specific ID in hand. parent = hand
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="parent"></param>
		public static void SpawnObjectInHand(int objectID, Transform parent)
		{
			GameObject prefab = ObjectDatabase.GetObject(objectID).objectPrefab;

			if (prefab != null)
			{
				GameObject clone = Instantiate(prefab) as GameObject;
				Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
				clone.transform.SetParent(parent);

				rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |
				                        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				Transform inHand = clone.transform.GetChild(0);
				clone.transform.localPosition = inHand.transform.localPosition;
				clone.transform.localScale = inHand.transform.localScale;
				clone.transform.localRotation = inHand.transform.localRotation;
			}

		}

		/// <summary>
		/// Spawns object relative to the "relation"
		/// </summary>
		/// <param name="objectID"></param>
		/// <param name="relation"></param>
		/// <param name="null"></param>
		public static void SpawnObjectRelativeTo(int objectID, Transform relation, Transform parent = null)
		{
			Game.Object obj = ObjectDatabase.GetObject(objectID);
			GameObject prefab = obj.objectPrefab;
			float fowardDistance = Random.Range(2.0f, 2.5f);
			float sideDistance = Random.Range(-.5f, .5f);
			Vector3 forwardOffset = (relation.forward * fowardDistance) + (Vector3.right * sideDistance);
			if (prefab != null)
			{
				GameObject clone = Instantiate(prefab) as GameObject;
				clone.gameObject.name = obj.objectName;
				clone.transform.position = relation.position + forwardOffset;
				clone.transform.SetParent(parent);
			}
		}
	}
}
