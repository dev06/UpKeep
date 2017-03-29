using UnityEngine;
using System.Collections;

public class TerrainCreator {

	public TerrainChunk chunk;

	public TerrainCreator(TerrainChunk chunk, float[,] noise)
	{
		this.chunk = chunk;
		TerrainData terrainData = new TerrainData();


		terrainData.size = new Vector3(chunk.width, chunk.width, chunk.length);



		terrainData.heightmapResolution = 129;
		terrainData.SetHeights(0, 0, noise);
		GameObject go = Terrain.CreateTerrainGameObject( terrainData );
		go.GetComponent<Terrain>().Flush();
		go.transform.position = new Vector3(chunk.x, 0, chunk.z);
	}


}
