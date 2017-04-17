using UnityEngine;
using System.Collections;

namespace Game
{
	public class CameraBob : MonoBehaviour {

		public float xSpeed = 6f;

		public float ySpeed = 7f;
		public float amplitude = .25f;

		float timer;
		void Start () {

		}


		void Update ()
		{

		}

		public void Bob(float multiplier)
		{
			timer += Time.deltaTime;

			if (timer > Mathf.PI * 2f)
			{
				timer = timer - Mathf.PI * 2f;
			}

			float xCam = Mathf.Sin(timer * xSpeed) * amplitude;
			float yCam = -Mathf.Abs((Mathf.Cos(timer *  ySpeed) * amplitude) / 2.0f);
			//	transform.localPosition = new Vector3(xCam * multiplier,  1 + (yCam * multiplier), transform.localPosition.z);
		}
	}

}
