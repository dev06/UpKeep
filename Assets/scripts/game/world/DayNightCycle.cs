using UnityEngine;
using System.Collections;
namespace Game
{
	public class DayNightCycle : MonoBehaviour
	{


		public float startHour = 12f;
		public float TotalHour = 24.0f;
		public float daySpeed = 0.1f;

		private float time;
		private Vector3 sunRotation;


		public static float Day = 1.0f;
		public static float Hour;
		public static float TotalDay;


		private Color ambientColor;
		private float ambientIntensity;

		void Start()
		{
			sunRotation = Vector3.zero;
			TotalDay = TotalHour;
			time = TotalHour / (TotalHour / startHour);
			ambientColor = new Color(0, 0, 0, 1);
		}


		void Update ()
		{
			if (GameController.Instance.isGamePaused == false)
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
				ambientIntensity = Sin(rotation);
				ambientIntensity = Mathf.Clamp(ambientIntensity, 0, 1);
				SetAmbientColor(ambientIntensity);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(sunRotation), Time.deltaTime * 10f);
			}
		}


		private void SetAmbientColor(float intensity)
		{


			ambientColor.r = intensity;
			ambientColor.g = intensity;
			ambientColor.b = intensity;
			RenderSettings.ambientSkyColor = ambientColor;
			RenderSettings.ambientGroundColor = ambientColor;
			float equator = Mathf.Clamp(intensity, .15f, .4f);
			RenderSettings.ambientEquatorColor = new Color(equator, equator, equator);
		}



		float Sin(float mydeg)
		{
			return  Mathf.Sin((mydeg * Mathf.PI) / 180);
		}


		public void SetSpeed(float speed)
		{
			this.daySpeed = speed;
		}

		public void SetTime(float time)
		{
			this.time = time;
		}
	}
}
