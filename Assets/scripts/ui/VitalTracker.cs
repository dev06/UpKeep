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
					foreground.fillAmount = Mathf.SmoothDamp(foreground.fillAmount, mob.Health / mob.MaxHealth, ref velocity, .1f);
					text.text = (int)(foreground.fillAmount * mob.MaxHealth) + " % ";
					break;
				}

				case TrackingVital.STAMINA:
				{
					foreground.fillAmount = Mathf.SmoothDamp(foreground.fillAmount, mob.Stamina / mob.MaxStamina, ref velocity, .1f);
					text.text = (int)(foreground.fillAmount * mob.MaxStamina) + " % ";
					break;
				}
			}
		}
	}

}
