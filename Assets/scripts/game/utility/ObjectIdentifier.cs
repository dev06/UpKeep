using UnityEngine;
using System.Collections;
using GameUtility;

namespace Game
{
	[RequireComponent (typeof(Rigidbody))]
	public class ObjectIdentifier : MonoBehaviour {


		public Game.Object obj;
		public int objectIdentifier;

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
	}
}
