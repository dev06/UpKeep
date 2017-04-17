using UnityEngine;
using System.Collections;
using Game;
namespace UpkeepInput
{
	public class GameInputManager : MonoBehaviour {

		public static GameInputManager Instance;

		public static KeyCode SPRINT_KEYCODE = KeyCode.LeftShift;
		public static KeyCode BOOST_KEYCODE = KeyCode.LeftControl;

		public static KeyCode AIM_KEYCODE = KeyCode.Mouse1;
		public static KeyCode FIRE_KEYCODE = KeyCode.Mouse0;
		public static KeyCode PUNCH_KEYCODE = KeyCode.Mouse0;
		public static KeyCode DEBUG_KEYCODE = KeyCode.BackQuote;
		public static KeyCode JUMP_KEYCODE = KeyCode.Space;
		public static KeyCode ITEM_USE_KEYCODE = KeyCode.Mouse1;
		public static KeyCode PICKUPITEM_KEYCODE = KeyCode.E;
		public static KeyCode INVENTORY_ACTIVE_KEYCODE = KeyCode.Tab;
		public static KeyCode PAUSE_KEYCODE = KeyCode.Escape;




		public GameInput input;



		void Awake()
		{
			if (Instance == null) {
				Instance = this;
			} else
			{
				Destroy(gameObject);
			}
		}
		void Start ()
		{
			input = new GameInput();
		}


		void Update ()
		{
			input.UpdateInput();
		}


		public bool GetMouseButton(int mouse)
		{
			return input.GetMouseButton(mouse);
		}


		public bool GetKeyDown(KeyCode key)
		{
			return input.GetKeyDown(key);
		}

		public bool GetKeyUp(KeyCode key)
		{
			return input.GetKeyUp(key);
		}

		public bool GetKey(KeyCode key)
		{
			return input.GetKey(key);
		}

	}

}
