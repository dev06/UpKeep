using UnityEngine;
using System.Collections;

namespace world
{
	public class TerrainGenerator : MonoBehaviour {

		Mesh mesh;
		float height;
		float vel;
		float timer;
		void Start ()
		{

			// mesh =  GetComponent<MeshFilter>().mesh;


			// gameObject.AddComponent<MeshCollider>();


			// Debug.Log(mesh.vertices.Length);
		}

		void Update () {

			// Vector3[] vertices = mesh.vertices;

			// for (int i = 0; i < vertices.Length; i++)
			// {
			// 	vertices[i].y =  Mathf.Sin((i * timer) / 500.0f) * 2.0f ;
			// }


			// timer += Time.deltaTime;

			// if (timer > 1)
			// {


			// }
			// mesh.vertices = vertices;
			// //mesh.RecalculateBounds();
			// //mesh.RecalculateNormals();
		}
	}

}
