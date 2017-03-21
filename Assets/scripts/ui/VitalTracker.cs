using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
namespace UI
{

	public class VitalTracker : MonoBehaviour {

		public enum TrackingVital
		{
			HEALTH,
			STAMINA
		}

		public Mob mob;
		public TrackingVital trackingVital;
		private Image foreground;
		private Text text;
		private float velocity;

		void Start ()
		{
			foreground = transform.GetChild(1).GetComponent<Image>();
			text = transform.GetChild(2).GetComponent<Text>();
		}


		void FixedUpdate ()
		{
			switch (trackingVital)
			{
				case TrackingVital.HEALTH:
				{
					float health = mob.GetFloat("Health");
					float maxHealth = mob.GetFloat("MaxHealth");
					foreground.fillAmount = Mathf.SmoothDamp(foreground.fillAmount, health / maxHealth, ref velocity, .1f);
					text.text = (int)(foreground.fillAmount * maxHealth) + " % ";
					break;
				}

				case TrackingVital.STAMINA:
				{
					float stamina = mob.GetFloat("Stamina");
					float maxStamina = mob.GetFloat("MaxStamina");
					foreground.fillAmount = Mathf.SmoothDamp(foreground.fillAmount, stamina / maxStamina, ref velocity, .1f);
					text.text = (int)(foreground.fillAmount * maxStamina) + " % ";
					break;
				}
			}
		}
	}

}
