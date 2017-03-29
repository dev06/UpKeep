using UnityEngine;
using System.Collections;

namespace system
{
	public class Cursor {


		public bool isVisible;
		public Cursor()
		{

		}


		public void ShowCursor()
		{
			UnityEngine.Cursor.visible = true;

			UnityEngine.Cursor.lockState = CursorLockMode.None;
			isVisible = true;
		}

		public void HideCursor()
		{
			UnityEngine.Cursor.lockState = CursorLockMode.Locked;
			UnityEngine.Cursor.visible = false;

			isVisible = false;
		}


	}

}
