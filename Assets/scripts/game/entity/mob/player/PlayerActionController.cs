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
		private Item previousHeldItem;
		private bool IsLayerWeightDampActive;
		private float punchingSpeed = 12f;
		private float punchDamage = 15;

		public PlayerActionState actionState;
		public PlayerMovementController movementController;


		private int walkHash = Animator.StringToHash("walk");
		private int sprintHash = Animator.StringToHash("sprinting");

		private int aimHash = Animator.StringToHash("aim");
		private int holdItem1Hand = Animator.StringToHash("hold_item_1_hand");
		private int holdWeapon2Hand = Animator.StringToHash("hold_weapon_2_hand");



		public void Initialize()
		{
			player = GetComponent<Player>();
			movementController = player.movementController;
			animator = player.GetAnimator();
			gameInputManager = GameInputManager.Instance;
		}

		public void UpdatePlayerAction()
		{


			if (DebugController.DEBUG_MODE == false)
			{

				Item item = player.itemInHand.GetItem();
				bool hasItem = player.itemInHand.GetItem() != null;
				bool isFireArm = hasItem && (player.itemInHand.GetItem() is Weapon);
				bool aiming = Input.GetKey(GameInputManager.AIM_KEYCODE);

				if (hasItem)
				{

					if (previousHeldItem != null)
					{
						if (previousHeldItem.objectID != player.itemInHand.GetItem().objectID)
						{
							if (!IsLayerWeightDampActive) {

								StartCoroutine("LayerWeight");
							}
						}
					} else
					{
						if (!IsLayerWeightDampActive) {

							StartCoroutine("LayerWeight");
						}
					}
				}

				if (previousHeldItem != null && player.itemInHand.GetItem() == null)
				{
					if (!IsLayerWeightDampActive) {

						StartCoroutine("LayerWeight");
					}
				}

				TriggerPunch(player.itemInHand.GetItem());
				previousHeldItem = player.itemInHand.GetItem();
				animator.SetBool(walkHash, player.movementController.IsMoving());
				animator.SetBool(sprintHash, player.movementController.IsSprinting());
				animator.SetBool(aimHash, aiming);

				if (item is Weapon)
				{
					Weapon w = (Weapon)item;
					player.cameraController.SetFOV(aiming ? w.aimFOV : 70);
				}


			}
		}


		IEnumerator LayerWeight()
		{
			IsLayerWeightDampActive = true;
			float value = 0;

			if (previousHeldItem != null)
			{
				int l = FindAnimationLayer(previousHeldItem.objectID);
				animator.SetLayerWeight(l, 0);
			}


			if (player.itemInHand.GetItem() != null)
			{
				int layer = FindAnimationLayer(player.itemInHand.GetItem().objectID);
				while (value < 1)
				{
					value += Time.deltaTime * 5.0f;
					animator.SetLayerWeight(layer, value);
					yield return new WaitForSeconds(Time.deltaTime);
				}
				animator.SetLayerWeight(layer, 1);

			} else
			{
				yield return  null;
			}




			IsLayerWeightDampActive = false;
		}

		public void LateUpdatePlayerAction()
		{

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


		private void TriggerPunch(Item item)
		{

			if (player.gameInputManager.GetKeyDown(GameInputManager.PUNCH_KEYCODE) && !player.GetGameController().isGamePaused && !player.GetGameController().IsState(StateManager.State.INVENTORY))
			{
				RegisterPunch(player.lookPoint);
				animator.SetTrigger("punch_tg");

				if (item == null)
				{
					animator.SetLayerWeight(1, 1);
				} else
				{
					animator.SetLayerWeight(1, 0);
				}
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

				try
				{
					if ((zombie)hitObject.GetComponent<Mob>() != null)
					{
						zombie z = (zombie)hitObject.GetComponent<Mob>();
						z.DoDamage(punchDamage);
					}
				} catch (System.Exception e)
				{

				}


			}
		}


		private int FindAnimationLayer(int objectID)
		{

			for (int i = 2 ; i < animator.layerCount; i++)
			{
				string layerName = animator.GetLayerName(i);
				try
				{
					string[] data = layerName.Split('_');
					if (int.Parse(data[1]) == objectID)
					{
						return i;
					}
				} catch (System.Exception e)
				{

				}
			}

			return -1;
		}


		public void SetActionState(PlayerActionState state)
		{
			this.actionState = state;
		}
	}

}
