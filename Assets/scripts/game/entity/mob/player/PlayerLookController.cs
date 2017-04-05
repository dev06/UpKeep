using UnityEngine;
using System.Collections;
using UpkeepInput;

namespace Game
{
	[RequireComponent (typeof(Player))]
	public class PlayerLookController : MonoBehaviour {


		public float mouseXSensitvity = 2.0f;
		public float mouseYSensitvity = 2.0f;
		private float clampMin = -65f;
		private float clampMax = 65f;
		private Player player;
		private GameInputManager gameInputManager;
		private float rotationX;
		private float rotationY;
		private Vector2 lookRotation;

		public void Initialize()
		{
			player = GetComponent<Player>();
			lookRotation = Vector2.zero;
			gameInputManager = GameInputManager.Instance;
		}

		public Vector2 GetLookRotation()
		{
			rotationX = gameInputManager.input.look.x * mouseXSensitvity;
			rotationY -= gameInputManager.input.look.y * mouseYSensitvity;
			rotationY = Mathf.Clamp(rotationY, clampMin, clampMax);
			lookRotation.x = rotationX;
			lookRotation.y = rotationY;
			return lookRotation;
		}

	}

}
