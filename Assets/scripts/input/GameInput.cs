using UnityEngine;
using System.Collections;
using Game;
using system;
namespace UpkeepInput
{
	public class GameInput
	{

		private system.Cursor cursor;
		public float mouseX;
		public float mouseY;

		public GameInput()
		{
			cursor = new system.Cursor();
			cursor.HideCursor();
		}

		public void UpdateInput()
		{
			if (StateManager.Instance.state != StateManager.State.PAUSE)
			{
				mouseX = UnityEngine.Input.GetAxis("Mouse X");
				mouseY = UnityEngine.Input.GetAxis("Mouse Y");

				cursor.HideCursor();


			} else
			{
				mouseX = 0;
				mouseY = 0;
				if (!cursor.isVisible)
				{
					cursor.ShowCursor();
				}
			}

			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				StateManager.Instance.SetState(StateManager.Instance.state == StateManager.State.PAUSE ? StateManager.State.GAME : StateManager.State.PAUSE);
			}
		}
	}

}
