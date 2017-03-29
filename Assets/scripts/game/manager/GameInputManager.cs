using UnityEngine;
using System.Collections;
using Game;
namespace UpkeepInput
{
	public class GameInputManager : MonoBehaviour {

		public static GameInputManager Instance;

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
