using UnityEngine;
using System.Collections;
using UpkeepInput;

namespace Game
{
	public enum PlayerActionState
	{
		None,
		Idle,
		Walk,
		Aim,
		Hold,
		Walk_Aim,
		Walk_Hold,
	}

	[RequireComponent (typeof(Player))]
	public class PlayerActionController : MonoBehaviour {


		private Player player;
		private GameInputManager gameInputManager;
		private Animator animator;
		private float layerWeight;
		private float layerWeightVel;

		public PlayerActionState actionState;
		public PlayerMovementController movementController;


		private int walkHash = Animator.StringToHash("walk");
		private int aimHash = Animator.StringToHash("aim");


		public void Initialize()
		{
			player = GetComponent<Player>();
			movementController = player.movementController;
			animator = player.GetAnimator();
			gameInputManager = GameInputManager.Instance;
		}

		public void UpdatePlayerAction()
		{
			bool hasItem = player.itemInHand.GetItem() != null;
			bool aiming = Input.GetKey(GameInputManager.AIM_KEYCODE);

			animator.SetBool(walkHash, player.movementController.IsMoving());
			animator.SetBool(aimHash, aiming && hasItem && !player.movementController.IsSprinting());
			animator.SetBool("sprinting", player.movementController.IsSprinting() && hasItem);
			animator.SetLayerWeight(1, hasItem ? DampWeight(1) : DampWeight(0));
		}

		private bool IsState(PlayerActionState state)
		{
			return actionState == state;
		}


		public float DampWeight(float targetValue)
		{
			layerWeight = Mathf.SmoothDamp(layerWeight, targetValue, ref layerWeightVel, Time.deltaTime * 5.0f);
			return layerWeight;
		}


		public void SetActionState(PlayerActionState state)
		{
			this.actionState = state;
		}
	}

}
