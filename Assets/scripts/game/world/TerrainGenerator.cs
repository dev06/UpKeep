using UnityEngine;
using System.Collections;
using System;
namespace world
{
	public class TerrainGenerator : MonoBehaviour {

		// public float Length = 50f;
		// public float Height = 50f;

		// public int Resolution = 129;


		// public Texture2D GrassTexture;
		// public Texture2D RockTexture;

		// public GameObject WaterPrefab;


		// // Use this for initialization
		// void Start () {
		// 	Generate();
		// 	PlaceWaterPrefab();
		// }


		// public void PlaceWaterPrefab()
		// {
		// 	GameObject WaterObject = Instantiate(WaterPrefab);
		// 	WaterObject.transform.localScale = new Vector3(Length / 10f, 0f, Length / 10f);
		// 	WaterObject.transform.position = new Vector3(Length / 2f, 18f, Length / 2f);
		// }

		// TerrainData TerrainData = new TerrainData();
		// public void Generate()
		// {
		// 	//	TerrainData TerrainData = new TerrainData();
		// 	TerrainData.alphamapResolution = Resolution;

		// 	//Set heights
		// 	TerrainData.heightmapResolution = Resolution;
		// 	TerrainData.SetHeights(0, 0, MakeHeightmap());
		// 	TerrainData.size = new Vector3(Length, Height, Length);

		// 	//Set textures
		// 	SplatPrototype Grass = new SplatPrototype();
		// 	SplatPrototype Rock = new SplatPrototype();

		// 	Grass.texture = GrassTexture;
		// 	Grass.tileSize = new Vector2(4f, 4f);
		// 	Rock.texture = RockTexture;
		// 	Rock.tileSize = new Vector2(4f, 4f);

		// 	TerrainData.splatPrototypes = new SplatPrototype[] { Grass, Rock };
		// 	TerrainData.RefreshPrototypes();
		// 	TerrainData.SetAlphamaps(0, 0, MakeSplatmap(TerrainData));


		// 	//Create terrain
		// 	GameObject TerrainObject = Terrain.CreateTerrainGameObject(TerrainData);
		// 	TerrainObject.GetComponent<Terrain>().Flush();
		// }


		// public float[,] MakeHeightmap()
		// {
		// 	float[,] Heightmap = new float[Resolution, Resolution];

		// 	for (int x = 0; x < Resolution; x++)
		// 		for (int z = 0; z < Resolution; z++)
		// 		{
		// 			Heightmap[x, z] = GetNormalizedHeight((float)x, (float)z);
		// 		}

		// 	return Heightmap;
		// }

		// public float scale = 1.0f;
		// public float[, ,] MakeSplatmap(TerrainData TerrainData)
		// {
		// 	float[, ,] Splatmap = new float[Resolution, Resolution, 2];

		// 	for (int x = 0; x < Resolution; x++)
		// 		for (int z = 0; z < Resolution; z++)
		// 		{
		// 			float NormalizedX = (float)x / ((float)Resolution - 1f);
		// 			float NormalizedZ = (float)z / ((float)Resolution - 1f);

		// 			float Steepness = TerrainData.GetSteepness(NormalizedX, NormalizedZ) / 90f;

		// 			Splatmap[z, x, 0] = 1f - Steepness;
		// 			Splatmap[z, x, 1] = Steepness;
		// 		}

		// 	return Splatmap;
		// }


		// public float GetNormalizedHeight(float x, float z)
		// {
		// 	return Mathf.Clamp(Mathf.PerlinNoise(x * 0.17f, z * 0.05f), 0f, 0.4f) * 0.95f + Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 0.05f;
		// }


		// // Update is called once per frame
		// void Update () {
		// 	TerrainData.SetHeights(0, 0, MakeHeightmap());
		// 	Terrain ter = Terrain.activeTerrain;
		// 	ter.terrainData = TerrainData;

		// }

		int chunkX = 2;
		int chunkZ = 2;
		int chunkSize = 50;
		int resolution = 129;

		void Start()
		{
			//	TerrainData terrainData = new TerrainData();
			//	terrainData.heightmapResolution = resolution ;
			float[,] noise = new float[resolution , resolution];
			for (int i = 0; i < noise.GetLength(0); i++)
			{
				for (int j = 0; j < noise.GetLength(1); j++)
				{
					noise[i, j] =  Mathf.PerlinNoise(i / 15.0f, j / 15.0f);
				}
			}

			// terrainData.SetHeights(0, 0, GetSubnoise(noise, 0, 0));

			// terrainData.size = new Vector3(chunkSize * chunkX, chunkSize , chunkSize * chunkZ);





			//	TerrainChunk chunk = new TerrainChunk(0, 0, chunkSize * chunkX, chunkSize * chunkZ, 15f);

			//	TerrainCreator creator = new TerrainCreator(chunk, GetSubnoise(noise, 0, 0));

			for (int x = 0; x < chunkX; x++)
			{
				for (int z = 0; z < chunkZ; z++)
				{

					TerrainChunk chunk = new TerrainChunk(x * chunkSize, z * chunkSize, chunkSize, chunkSize , 15f);

					float[,] subnoise = GetSubnoise(noise, x, z);


					TerrainCreator creator = new TerrainCreator(chunk, subnoise);


				}
			}

			// for (int i = 0; i < terrainChunk.GetLength(0) * chunkSize; i++)
			// {
			// 	for (int j = 0; j <  terrainChunk.GetLength(1) * chunkSize; j++)
			// 	{
			// 		noise[i, j] =  Mathf.PerlinNoise(i / 5.0f, j / 5.0f);


			// 	}
			// }
		}

		float[,] GetSubnoise(float[,] noise, int x, int z)
		{
			float[,] ret = new float[chunkSize, chunkSize];


			// for (int xx = x * chunkSize; xx < (x * chunkSize + chunkSize); xx++) {
			// 	for (int zz = z * chunkSize; zz < (z * chunkSize + chunkSize) ; zz++)
			// 	{
			// 		try
			// 		{	ret[xx, zz] = Mathf.PerlinNoise(xx / 15.0f, zz / 15.0f);

			// 		} catch (Exception e)
			// 		{
			// 			Debug.Log(xx  + " " + zz);
			// 		}

			// 	}
			// }

			// for (int xx = 0; xx < chunkSize; xx++) {
			// 	for (int zz = 0; zz < chunkSize ; zz++)
			// 	{
			// 		try
			// 		{	ret[xx, zz] = Mathf.PerlinNoise(xx / 15.0f, zz / 15.0f);

			// 		} catch (Exception e)
			// 		{
			// 			Debug.Log(xx  + " " + zz);
			// 		}

			// 	}
			// }
			int sx = x * chunkSize;
			int sz = z * chunkSize;

			int ex = sx + chunkSize;
			int ez = sz + chunkSize;


			for (int xx = sx ; xx < ex; xx++) {
				for (int zz = sz ; zz < ez ; zz++)
				{
					ret[xx - (chunkSize * x), zz - (chunkSize * z)] = noise[xx, zz];

					//	Debug.Log(ret[xx - (chunkSize * x), zz - (chunkSize * z)]);
				}
			}


			return ret;
		}

		void Update() {

		}

	}

}
