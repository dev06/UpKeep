using UnityEngine;
using System.Collections;


namespace Game
{
	public class EventManager : MonoBehaviour
	{

		public delegate void StateChange(StateManager.State state);
		public static StateChange SetState;



	}

}
