using UnityEngine;
using System.Collections;
namespace Game
{
	public class DayNightCycle : MonoBehaviour
	{


		public float TotalHour = 24.0f;
		public float daySpeed = 0.1f;

		private float time;
		private Vector3 sunRotation;


		public static float Day = 1.0f;
		public static float Hour;
		public static float TotalDay;


		void Start()
		{
			sunRotation = Vector3.zero;
			TotalDay = TotalHour;
		}

		void Update ()
		{

			time += Time.deltaTime * daySpeed;
			if (time > TotalHour)
			{
				Day++;
				time = 0;
			}



			float rotation = ((int)(time * 360f) / TotalHour) - 90;
			sunRotation.x = rotation;
			Hour = time;
			transform.rotation = Quaternion.Euler(sunRotation);
		}


	}

}
