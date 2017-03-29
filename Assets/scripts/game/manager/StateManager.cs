using UnityEngine;
using System.Collections;

namespace Game
{
	//class responsible for switching different states of the game
	//pause state, menu state, game state, setting state, etc;
	public class StateManager : MonoBehaviour {

		/// <summary>
		/// State of the game
		/// </summary>
		public enum State
		{
			NONE,
			GAME,
			PAUSE,
		}

		public static StateManager Instance;

		public State state;


		/// <summary>
		/// Creates the Instance
		/// </summary>
		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			} else
			{
				Destroy(gameObject);
			}
		}

		void Start ()
		{
			SetState(State.GAME);
		}

		/// <summary>
		/// Sets the state for the game
		/// </summary>
		/// <param name="state"></param>
		public void SetState(State state)
		{
			this.state = state;

			if (EventManager.SetState != null)
			{
				EventManager.SetState(state);
			}
		}
	}
}
