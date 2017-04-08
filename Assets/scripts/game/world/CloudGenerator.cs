using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtility;
namespace Game
{
	public class CloudGenerator : MonoBehaviour
	{

		private GameObject prefab;


		public Vector2 scaleX;
		public Vector2 scaleZ;

		public Vector2 positionX;
		public Vector2 positionZ;
		public Vector2 positionY;

		List<GameObject> clouds = new List<GameObject>();

		void Start()
		{




			prefab = GameResource.Cloud;

			Generate();

		}

		void Generate()
		{
			foreach (GameObject go in clouds)
			{
				Destroy(go);
			}

			clouds.Clear();


			for (int i = 0; i < 40; i++)
			{

				System.Random prng = new System.Random();
				float x = Random.Range(positionX.x, positionX.y);
				float y = Random.Range(positionY.x, positionY.y);
				float z = Random.Range(positionZ.x, positionZ.y);
				float sx = Random.Range(scaleX.x, scaleX.y);
				float sz = Random.Range(scaleZ.x, scaleZ.y);
				GameObject clone = Instantiate(prefab, new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
				clone.transform.localScale = new Vector3(sx, 1, sz);
				clouds.Add(clone);
			}
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Generate();
			}
		}


	}

}
