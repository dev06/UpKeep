using UnityEngine;
using System.Collections;


namespace Game
{
	public class ItemAreaSpawner : MonoBehaviour {


		public GameObject objectSpawner;

		public Color color = new Color(0, .7f, 0	, 1);


		public int count = 1;

		void Start ()
		{

			float dx = (transform.position.x + transform.localScale.x / 2) - (transform.position.x - transform.localScale.x / 2);
			float dy = (transform.position.y + transform.localScale.y / 2) - (transform.position.y - transform.localScale.y / 2);
			float dz = (transform.position.z + transform.localScale.z / 2) - (transform.position.z - transform.localScale.z / 2);


			for (int i = 0; i < count; i++)
			{

				float xa = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
				float ya = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
				float za = Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2);


				Vector3 position = new Vector3(xa, ya, za);

				GameObject clone = (GameObject)Instantiate(objectSpawner, position, Quaternion.identity) as GameObject;
				clone.transform.SetParent(transform);

			}

		}

		void OnDrawGizmosSelected()
		{
			Gizmos.color = color;
			Gizmos.DrawWireCube(transform.position, transform.localScale);

		}

	}

}
