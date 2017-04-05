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
		public Texture2D[] textures;
		void Start()
		{
			CreateTerrain();
		}

		void CreateTerrain()
		{
			TerrainData data = new TerrainData();
			data.size = new Vector3(100, 100, 100);
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


			data.SetHeights(0 , 0, GetHeightMap(data));
			//	float[, ,] splatData = new float[data.alphamapWidth, data.alphamapHeight, data.alphamapLayers];
			//	splatData[0, 0, 0] = .5f;
			//	data.SetAlphamaps(0, 0, splatData);


		}


		private float[,] GetHeightMap(TerrainData data)
		{
			float[,] heightMap = data.GetHeights(0, 0, data.heightmapWidth, data.heightmapWidth);
			for (int z = 0; z < data.heightmapHeight; z++)
			{
				for (int x = 0; x < data.heightmapWidth; x++)
				{
					float noise = 0.0f;
					float gain = 1.0f;
					for (int i = 0; i < octaves; i++)
					{
						noise += Mathf.PerlinNoise(x * gain / frequency, z * gain / frequency) * amplitude / gain;
						gain = 2f * gain;
					}


					heightMap[z, x] = noise;


				}

			}

			return heightMap;
		}




	}

}
