using UnityEngine;
using System.Collections;
using GameUtility;
using Game;

public class ObjectSpawner : MonoBehaviour
{



	public enum SpawnObject
	{
		Fixed,
		Random
	}

	public enum SpawnFrequency
	{
		Random,
		AlwaysSpawn
	}

	public bool enableObjectPhysics = true;
	public SpawnObject spawnObject;
	public SpawnFrequency spawnFrequency;
	public Game.Object obj;
	public int objectID = -1;

	void Start()
	{
		float frequency = spawnFrequency == SpawnFrequency.AlwaysSpawn ? 1 : Random.Range(0f, 1f);
		float value = .5f;



		if (frequency >= value)
		{
			int randomValue =  Random.Range(0, ObjectDatabase.objectList.Count);
			int spawnID = spawnObject == SpawnObject.Random ? randomValue : objectID;
			obj = ObjectDatabase.GetObject(spawnID);
			GameObject clone = Instantiate(obj.objectPrefab, transform.position, transform.rotation) as GameObject;


			clone.transform.SetParent(transform);
			clone.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
			clone.transform.position = transform.position + Vector3.up * .5f;

			clone.GetComponent<ObjectIdentifier>().SetParent(transform);

			if (clone.GetComponent<Rigidbody>() != null) {

				if (enableObjectPhysics)
				{
					Rigidbody rigidBody = clone.GetComponent<Rigidbody>();
					rigidBody.constraints = RigidbodyConstraints.None;
				} else
				{
					clone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
					        RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				}
			}

		} else
		{
			Destroy(gameObject);
		}
	}
}
