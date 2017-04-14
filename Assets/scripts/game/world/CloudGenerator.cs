using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUtility;
namespace Game
{
	public class CloudGenerator : MonoBehaviour
	{

		public Transform parent;
		public int mapSize = 5;
		public float scale = 100;
		public float spread = 1000;
		public float squareSizeOffset = 1;
		public float rotation = 180;

		public float baseHeight = 100;
		public float maxHeight = 100;

		private GameObject prefab;

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


			for (int z = 0; z < mapSize; z++)
			{
				for (int x = 0; x < mapSize; x++)
				{
					float xPos = Random.Range(-spread, spread);
					float zPos = Random.Range(-spread, spread);
					float perlinValue = Mathf.PerlinNoise(x, z) * xPos;
					float perlinValueZ = Mathf.PerlinNoise(x, z) * zPos;
					float cloudHeight = Random.Range(baseHeight, maxHeight);
					GameObject clone = Instantiate(prefab, new Vector3(perlinValue, cloudHeight, perlinValueZ) , Quaternion.Euler(new Vector3(rotation, 0, 0))) as GameObject;
					float s = Random.Range(0 , scale);

					clone.transform.localScale = new Vector3(s + Random.Range(0 , squareSizeOffset), 1, s + Random.Range(0 , squareSizeOffset));
					clouds.Add(clone);
					clone.transform.SetParent(parent);
				}
			}

		}
	}

}
