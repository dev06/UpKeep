using UnityEngine;
using System.Collections;

namespace system
{
	public class Cursor : MonoBehaviour {


		void Awake ()
		{

			ShowCursor(false);
		}




		private void ShowCursor(bool b)
		{
			UnityEngine.Cursor.visible = b;
			Screen.lockCursor = !b;
		}


	}

}
