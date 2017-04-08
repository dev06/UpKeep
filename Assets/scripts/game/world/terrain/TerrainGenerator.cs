using UnityEngine;
using System.Collections;


namespace World
{
	///[ExecuteInEditMode]
	public class TerrainGenerator : MonoBehaviour
	{
		[Range(0f , 10f)]
		public int octaves = 5;
		public float frequency = 50f;
		public float amplitude = .5f;
		public float scale = 1f;
		public float persistence;
		public float lacunarity;
		public int seed;
		public Vector2 offset;
		public Texture2D[] textures;
		void Start()
		{
			CreateTerrain();
		}

		void CreateTerrain()
		{
			TerrainData data = new TerrainData();
			data.size = new Vector3(500	, 100, 500);
			data.heightmapResolution = 129;
			data.SetDetailResolution(512, 512);

			AddTexture(data);
			GameObject terrain = Terrain.CreateTerrainGameObject(data);
		}

		void AddTexture(TerrainData data)
		{
			SplatPrototype[] tex = new SplatPrototype [textures.Length];
			for (int i = 0; i < textures.Length; i++) {
				tex [i] = new SplatPrototype ();
				tex [i].texture = textures [i];    //Sets the texture
				tex [i].tileSize = new Vector2 (.5f, .5f);    //Sets the size of the texture
			}

			data.splatPrototypes = tex;

		}

		void Update()
		{


			TerrainData data = Terrain.activeTerrain.terrainData;
			data.SetHeights(0 , 0, GetHeightMap(data, seed, scale, octaves, persistence , lacunarity, offset));
		}


		// private float[,] GetHeightMap(TerrainData data)
		// {
		// 	float[,] heightMap = data.GetHeights(0, 0, data.heightmapWidth, data.heightmapWidth);
		// 	for (int z = 0; z < data.heightmapHeight; z++)
		// 	{
		// 		for (int x = 0; x < data.heightmapWidth; x++)
		// 		{
		// 			float noise = 0.0f;
		// 			float gain = 1.0f;

		// 			for (int i = 0; i < octaves; i++)
		// 			{

		// 				noise += Mathf.PerlinNoise(x * gain / frequency, z * gain / frequency) * amplitude / gain;
		// 				gain = 2f * gain;


		// 			}
		// 			heightMap[z, x] = noise;
		// 		}
		// 	}
		// 	return heightMap;
		// }



		private float[,] GetHeightMap(TerrainData data, int seed,  float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
		{
			int mapHeight = data.heightmapWidth;
			int mapWidth = data.heightmapWidth;

			System.Random prng = new System.Random(seed);
			Vector2[] octaveOffsets = new Vector2[octaves];

			for (int i = 0; i < octaves; i++)
			{
				float offsetX = prng.Next(-100000, 100000) + offset.x;
				float offsetY = prng.Next(-100000, 100000) + offset.y;
				octaveOffsets[i] = new Vector2(offsetX, offsetY);
			}

			if (scale <= 0) { scale = .0001f; }
			float[,] heightMap = data.GetHeights(0, 0, mapWidth, mapWidth);


			float maxNoiseHeight = float.MinValue;
			float minNoiseHeight = float.MaxValue;

			for (int y = 0; y < mapHeight; y++)
			{
				for (int x = 0; x < mapWidth; x++)
				{
					amplitude = 1f;
					float frequency = 1f;
					float noiseHeight = 0;

					for (int i = 0; i < octaves; i++)
					{
						float sampleX = (x - mapWidth / 2) / scale * frequency + octaveOffsets[i].x;
						float sampleY = (y - mapHeight / 2) / scale * frequency + octaveOffsets[i].y;

						float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
						noiseHeight += perlinValue * amplitude;

						amplitude *= persistence;
						frequency *= lacunarity;

					}

					if (noiseHeight > maxNoiseHeight)
					{
						maxNoiseHeight = noiseHeight;
					} else if (noiseHeight < minNoiseHeight)
					{
						minNoiseHeight = noiseHeight;
					}


					heightMap[x, y] = noiseHeight;


				}
			}

			for (int y = 0; y < mapHeight; y++)
			{
				for (int x = 0; x < mapWidth; x++)
				{
					heightMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heightMap[x, y]);
				}
			}


			return heightMap;
		}
	}

}
