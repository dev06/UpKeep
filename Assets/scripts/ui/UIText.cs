using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UI
{
	[RequireComponent (typeof(Text))]
	public class UIText : MonoBehaviour {

		public bool active = true;
		private Text text;
		void Start ()
		{
			text = GetComponent<Text>();
		}


	}

}
