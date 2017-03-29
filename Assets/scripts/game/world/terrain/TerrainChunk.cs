using UnityEngine;
using System.Collections;

public class TerrainChunk
{

	public int x;
	public int z;
	public int width;
	public int length;
	public float scale;

	public TerrainChunk(int x, int z, int width, int length, float scale)
	{
		this.x = x;
		this.z = z;
		this.width = width;
		this.length = length;
		this.scale = scale;
	}


	// public float[,] GetNoise()
	// {
	// 	float[,] noise = new float[width, length];

	// 	for (int x = 0; x < width; x++)
	// 	{
	// 		for (int z = 0; z < length; z++)
	// 		{
	// 			noise[x, z] = Mathf.PerlinNoise( this.x + (x / scale), this.z + (z / scale)) * .5f;
	// 		}
	// 	}

	// 	return noise;
	// }


}
