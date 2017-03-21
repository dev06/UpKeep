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
			if (mob == null || Camera.main == null) return;
			float health = mob.GetFloat("Health");
			float maxHealth = mob.GetFloat("MaxHealth");
			xScale = Mathf.SmoothDamp(xScale, (health * defaultScale) / maxHealth, ref velocity, .05f);
			foregroundQuad.transform.localScale = new Vector3(xScale , foregroundQuad.transform.localScale.y, foregroundQuad.transform.localScale.z);
			transform.LookAt(Camera.main.transform.position);
		}
	}

}
