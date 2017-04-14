using UnityEngine;
using System.Collections;

namespace Game
{
	public class GameController : MonoBehaviour {

		public static GameController Instance;

		public StateManager stateManager;

		public bool isGamePaused;

		public StateManager.State gameState;

		//public DebugController debugController;

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}


			stateManager = StateManager.Instance;
		}

		void Start ()
		{
			//	debugController  = new DebugController();
		}


		void Update ()
		{
			isGamePaused =  GetGameState() == StateManager.State.PAUSE;

			//	debugController.Update();

		}



		public StateManager.State GetGameState()
		{
			if (stateManager == null) { stateManager = StateManager.Instance; }
			return stateManager.state;
		}


		public bool IsState(StateManager.State state)
		{
			return GetGameState() == state;
		}


	}

}
