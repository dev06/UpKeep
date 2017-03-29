using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
namespace UI
{
	public class TimeDisplay : MonoBehaviour {

		private Text text;
		void Start ()
		{
			if (GetComponent<Text>() != null)
			{
				text = GetComponent<Text>();
			}
		}

		void FixedUpdate ()
		{
			if (text == null) { return; }
			text.text = " Day " + DayNightCycle.Day + "\n" + " Hour " + (int)(DayNightCycle.Hour) + " / " + (int)(DayNightCycle.TotalDay);
		}
	}

}
