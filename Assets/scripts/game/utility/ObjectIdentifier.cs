using UnityEngine;
using System.Collections;
using GameUtility;

namespace Game
{
	[RequireComponent (typeof(Rigidbody))]
	public class ObjectIdentifier : MonoBehaviour {


		public Game.Object obj;
		public int objectIdentifier;
		public Transform parent;

		void Start ()
		{
			SetObject(ObjectDatabase.GetObject(objectIdentifier));
		}

		public Game.Object GetObject()
		{
			return obj;
		}

		public void SetObject(Game.Object obj)
		{
			this.obj = obj;
		}

		public Transform GetParent()
		{
			return parent;
		}

		public void SetParent(Transform parent) {
			this.parent = parent;
		}
	}
}
