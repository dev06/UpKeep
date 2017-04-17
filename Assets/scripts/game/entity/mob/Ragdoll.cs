using UnityEngine;
using System.Collections;

namespace Game
{
	[RequireComponent (typeof(Rigidbody))]
	public class Ragdoll : MonoBehaviour
	{

		public bool EnableRagdoll;
		public bool EnableRagdollOnStart;

		Rigidbody rigidBody;

		void Start ()
		{
			rigidBody = transform.GetComponent<Rigidbody>();
			rigidBody.isKinematic = !EnableRagdollOnStart;
		}

		void FixedUpdate ()
		{
			rigidBody.isKinematic = !EnableRagdoll;
		}
	}

}
