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

				case StateManager.State.DEBUG:
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

			if (UnityEngine.Input.GetKeyDown(GameInputManager.PAUSE_KEYCODE))
			{
				StateManager.Instance.SetState(StateManager.Instance.state == StateManager.State.PAUSE ? StateManager.State.GAME : StateManager.State.PAUSE);
			}
			if (UnityEngine.Input.GetKeyDown(GameInputManager.INVENTORY_ACTIVE_KEYCODE))
			{
				StateManager.Instance.SetState(StateManager.Instance.state == StateManager.State.INVENTORY ? StateManager.State.GAME : StateManager.State.INVENTORY);
			}

			if (UnityEngine.Input.GetKeyDown(GameInputManager.PICKUPITEM_KEYCODE))
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

					ObjectIdentifier objIdentifier = focusedObject.objectIdentifier;
					if (objIdentifier.GetParent() != null)
					{
						Destroy(objIdentifier.GetParent().gameObject);
					}
					Destroy(focusedObject.objectIdentifier.transform.gameObject);
				}
			}

			if (UnityEngine.Input.GetKeyDown(GameInputManager.DEBUG_KEYCODE))
			{
				StateManager.Instance.SetState(StateManager.Instance.state == StateManager.State.DEBUG ? StateManager.State.GAME : StateManager.State.DEBUG);
			}


			if (UnityEngine.Input.GetKeyDown(GameInputManager.DROPITEM_KEYCODE))
			{
				if (ItemManager.currentItemInHand != null)
				{
					ItemManager.DropItem(ItemManager.currentItemInHand);
				}
			}

		}

		public bool GetMouseButton(int mouse)
		{
			if (StateManager.Instance.IsState(StateManager.State.DEBUG)) { return false; }
			return Input.GetMouseButtonDown(mouse);
		}

		public bool GetKeyDown(KeyCode key)
		{
			if (StateManager.Instance.IsState(StateManager.State.DEBUG)) { return false; }
			return Input.GetKeyDown(key);
		}

		public bool GetKeyUp(KeyCode key)
		{
			if (StateManager.Instance.IsState(StateManager.State.DEBUG)) { return false; }
			return Input.GetKeyUp(key);
		}

		public bool GetKey(KeyCode key)
		{
			if (StateManager.Instance.IsState(StateManager.State.DEBUG)) { return false; }
			return Input.GetKey(key);
		}
	}
}
