using UnityEngine;
using System.Collections;

namespace Game
{
	public class GameController : MonoBehaviour {

		public static GameController Instance;




		public bool isGamePaused;

		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}

		}

		void Start ()
		{

		}


		void Update ()
		{
			isGamePaused =  StateManager.Instance.state == StateManager.State.PAUSE;
		}




	}

}
