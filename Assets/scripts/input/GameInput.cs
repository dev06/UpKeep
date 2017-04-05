using UnityEngine;
using System.Collections;
using Game;
using system;
using UI;
namespace UpkeepInput
{
	public class GameInput : GameInputManager
	{

		private system.Cursor cursor;
		public Vector2 look = Vector2.zero;
		public Vector2 move = Vector2.zero;
		private FocusedObject focusedObject;

		public GameInput()
		{
			cursor = new system.Cursor();
			cursor.HideCursor();
		}

		public void UpdateInput()
		{



			switch (StateManager.Instance.state)
			{
				case StateManager.State.GAME:
				{
					look.x = UnityEngine.Input.GetAxis("Mouse X");
					look.y = UnityEngine.Input.GetAxis("Mouse Y");
					move.x = UnityEngine.Input.GetAxis("Horizontal");
					move.y = UnityEngine.Input.GetAxis("Vertical");
					cursor.HideCursor();
					break;
				}

				case StateManager.State.PAUSE:
				{
					look.x = 0;
					look.y = 0;
					move.x = 0;
					move.y = 0;
					if (!cursor.isVisible)
					{
						cursor.ShowCursor();
					}
					break;
				}

				case StateManager.State.INVENTORY:
				{
					look.x = 0;
					look.y = 0;
					move.x = 0;
					move.y = 0;
					if (!cursor.isVisible)
					{
						cursor.ShowCursor();
					}
					break;
				}
			}

			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				StateManager.Instance.SetState(StateManager.Instance.state == StateManager.State.PAUSE ? StateManager.State.GAME : StateManager.State.PAUSE);
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
			{
				StateManager.Instance.SetState(StateManager.Instance.state == StateManager.State.INVENTORY ? StateManager.State.GAME : StateManager.State.INVENTORY);
			}

			if (UnityEngine.Input.GetKeyDown(KeyCode.F))
			{
				if (focusedObject == null)
				{
					focusedObject = FindObjectOfType<FocusedObject>();
				}

				if (focusedObject.objectIdentifier != null)
				{
					if (EventManager.OnObjectPickup != null)
					{
						EventManager.OnObjectPickup(focusedObject.objectIdentifier.GetObject());
					}

					Destroy(focusedObject.objectIdentifier.transform.gameObject);
				}
			}
		}
	}

}
