using UnityEngine;
using System.Collections;

namespace system
{
	public class FPS : MonoBehaviour {


		public  static float fps;


		private void Update()
		{
			fps = 1.0f / Time.deltaTime;
		}
	}
}

