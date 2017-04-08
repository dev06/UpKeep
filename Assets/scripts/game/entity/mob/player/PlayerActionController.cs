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
		private bool isPunching;
		private float punchingSpeed = 12f;
		private float punchDamage = 15;

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

			TriggerPunch();
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


		private void TriggerPunch()
		{
			if (Input.GetKeyDown(GameInputManager.PUNCH_KEYCODE) && !player.GetGameController().isGamePaused && !player.GetGameController().IsState(StateManager.State.INVENTORY))
			{
				if (!isPunching)
				{
					if (player.GetItemInHand() == null)
					{
						StartCoroutine("AnimatePunch");
						RegisterPunch(player.lookPoint);
					}
				}
			}
		}


		IEnumerator AnimatePunch()
		{
			float value = .1f;
			float timer = 0;
			bool complete = false;
			isPunching = true;

			while (!complete)
			{
				timer += Time.deltaTime * punchingSpeed;
				value = Mathf.Sin(timer) ;
				if (value < 0)
				{
					complete = true;
					isPunching = false;
					break;
				}

				animator.SetLayerWeight(2, value);
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}


		private void RegisterPunch(Transform lookPoint)
		{
			Ray ray = new Ray(lookPoint.position, lookPoint.forward);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray.origin, lookPoint.forward, out hitInfo, 1))
			{
				GameObject hitObject = hitInfo.transform.gameObject;

				if (hitObject == null)
				{
					return;
				}


				if ((zombie)hitObject.GetComponent<Mob>() != null)
				{
					zombie z = (zombie)hitObject.GetComponent<Mob>();
					z.DoDamage(punchDamage);
				}
			}
		}




		public void SetActionState(PlayerActionState state)
		{
			this.actionState = state;
		}
	}

}
