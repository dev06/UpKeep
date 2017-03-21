using UnityEngine;
using System.Collections;
using Game;
namespace UI
{
	public class HealthUI : MonoBehaviour {

		public Transform foregroundQuad;
		public Mob mob;

		private float defaultScale;
		private float velocity;
		private float xScale;
		void Start ()
		{
			defaultScale = foregroundQuad.localScale.x;
		}


		void FixedUpdate ()
		{
			xScale = Mathf.SmoothDamp(xScale, (mob.Health * defaultScale) / mob.MaxHealth, ref velocity, .05f);
			foregroundQuad.transform.localScale = new Vector3(xScale , foregroundQuad.transform.localScale.y, foregroundQuad.transform.localScale.z);
			transform.LookAt(Camera.main.transform.position);
		}
	}

}
