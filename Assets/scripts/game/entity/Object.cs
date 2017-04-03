using UnityEngine;
using System.Collections;

namespace Game
{


	public enum ObjectType
	{
		None,
		Consumable,
	}


	public class Object: MonoBehaviour
	{

		public int objectID;
		public string objectName;
		public string packagePath;
		public ObjectType objectType;

	}

	public class Consumable : Object
	{

	}

}
