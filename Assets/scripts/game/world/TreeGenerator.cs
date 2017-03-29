using UnityEngine;
using System.Collections;

public class TreeGenerator : MonoBehaviour {

	public int size = 50;
	public GameObject prefab;
	void Start ()
	{
		Terrain activeTerrain = Terrain.activeTerrain;
		TerrainData terrainData = activeTerrain.terrainData;
		// for (int y = 0; y < size ; y++)
		// {
		// 	for (int x = 0; x < size; x++)
		// 	{

		// 		float py = Mathf.PerlinNoise(x / 10.0f, y / 10.0f);
		// 		float xx = Random.Range(0, terrainData.size.x);
		// 		float zz = Random.Range(0, terrainData.size.z);
		// 		Vector3 position = new Vector3(py * terrainData.size.x, py, py * terrainData.size.z);
		// 		// float x = Random.Range(0, terrainData.size.x);
		// 		// float y = 0;
		// 		// float z = Random.Range(0, terrainData.size.z);
		// 		// Vector3 position = new Vector3(x, 0, z);
		// 		// y = activeTerrain.SampleHeight(position);
		// 		// position = new Vector3(x, y, z);

		// 		Instantiate(prefab, position, Quaternion.Euler(new Vector3(-90, Random.Range(-360, 360), 0)));



		// 		//	Debug.Log(Mathf.PerlinNoise(x / 10.0f, y / 10.0f));
		// 	}
		// }

		float width = terrainData.heightmapWidth;
		float length = terrainData.heightmapHeight;
		float[,] heights = terrainData.GetHeights(0, 0, Mathf.RoundToInt(width), Mathf.RoundToInt(length));
		for (int i = 0; i < Mathf.RoundToInt(width); i++)
			for (int j = 0; j <  Mathf.RoundToInt(length); j++) {
				heights[i, j] = Mathf.PerlinNoise(i / scale, j / scale) * heightScale;

			}

		terrainData.SetHeights(0, 0, heights);
		// for (int x = 0; x < terrainData.size.x / 15; x++)
		// {
		// 	for (int z = 0; z < terrainData.size.z / 15; z++)
		// 	{
		// 		float py = Mathf.PerlinNoise(x / scale, z / scale);
		// 		float xOffset = Random.Range(10, 16);
		// 		float zOffset = Random.Range(10, 16);
		// 		Vector3 position = new Vector3(x * xOffset + terrainData.size.x / 30, 0, z * zOffset + terrainData.size.z / 30);
		// 		float y = activeTerrain.SampleHeight(position);
		// 		position = new Vector3(x * xOffset + terrainData.size.x / 30, y +  Vector3.up.y, z * zOffset + terrainData.size.z / 30);
		// 		if (py < .4f)
		// 		{
		// 			GameObject clone = (GameObject)Instantiate(prefab, position, Quaternion.Euler(new Vector3(-90, Random.Range(-360, 360), 0))) as GameObject;
		// 			clone.transform.SetParent(transform);
		// 		}


		// 	}
		// }


	}

	public float scale = 1.0f;
	public float heightScale = .1f;
	void Update () {

	}
}
