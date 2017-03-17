using UnityEngine;
using System.Collections;

namespace world
{
	public class TerrainGenerator : MonoBehaviour {


		void Start ()
		{
			Mesh mesh = GetComponent<MeshFilter>().mesh;
			Vector3[] vertices = mesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i].y = Mathf.PerlinNoise((vertices[i].x + transform.position.x) / 20.0f, (vertices[i].z + transform.position.z) / 20.0f );
			}

			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
			gameObject.AddComponent<MeshCollider>();
		}

		void Update () {

		}
	}

}
