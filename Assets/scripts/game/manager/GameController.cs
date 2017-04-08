using UnityEngine;
using System.Collections;

namespace Game
{
	public class GameController : MonoBehaviour {

		public static GameController Instance;


		public StateManager stateManager;

		public bool isGamePaused;

		public StateManager.State gameState;

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

		}


		void Update ()
		{
			isGamePaused =  GetGameState() == StateManager.State.PAUSE;
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
