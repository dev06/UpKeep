using UnityEngine;
using System.Collections;
using GameUtility;
using Game;
public class ObjectSpawner : MonoBehaviour
{

	public Game.Object obj;
	public int objectID = -1;

	void Start()
	{
		GameObject clone = Instantiate(ObjectDatabase.GetObject(objectID).objectPrefab, transform.position, transform.rotation) as GameObject;
		obj = ObjectDatabase.GetObject(objectID);
		clone.transform.SetParent(transform);
		Transform inWorld = clone.transform.GetChild(1);
		clone.transform.localRotation = inWorld.rotation;
	}

}
