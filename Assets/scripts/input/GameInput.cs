using UnityEngine;
using System.Collections;
using Game;
using system;
namespace UpkeepInput
{
	public class GameInput
	{

		private system.Cursor cursor;
		public Vector2 look = Vector2.zero;
		public  Vector2 move = Vector2.zero;

		public GameInput()
		{
			cursor = new system.Cursor();
			cursor.HideCursor();
		}

		public void UpdateInput()
		{
			if (StateManager.Instance.state != StateManager.State.PAUSE)
			{
				look.x = UnityEngine.Input.GetAxis("Mouse X");
				look.y = UnityEngine.Input.GetAxis("Mouse Y");
				move.x = UnityEngine.Input.GetAxis("Horizontal");
				move.y = UnityEngine.Input.GetAxis("Vertical");

				cursor.HideCursor();


			} else
			{
				look.x = 0;
				look.y = 0;
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
