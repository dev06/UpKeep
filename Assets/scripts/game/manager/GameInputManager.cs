using UnityEngine;
using System.Collections;
using Game;
namespace UpkeepInput
{
	public class GameInputManager : MonoBehaviour {

		public static GameInputManager Instance;

		public static KeyCode SPRINT_KEYCODE = KeyCode.LeftShift;
		public static KeyCode AIM_KEYCODE = KeyCode.Mouse1;
		public static KeyCode FIRE_KEYCODE = KeyCode.Mouse0;
		public static KeyCode PUNCH_KEYCODE = KeyCode.Mouse0;



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
	}

}
