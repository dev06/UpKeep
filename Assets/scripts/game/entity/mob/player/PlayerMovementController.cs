using UnityEngine;
using System.Collections;
using UpkeepInput;

namespace Game
{
	/// <summary>
	/// Class Handles movement for player.
	/// Includes jumping, camerabob and taking fall damage
	/// </summary>
	[RequireComponent (typeof(Player))]
	public class PlayerMovementController : MonoBehaviour {


		private CharacterController characterController;
		private CameraController cameraController;
		private MobCharacteristics mobChar;
		private Player player;
		private GameInputManager gameInputManager;
		private Vector3 movement;
		private float jumpForce = 1f;
		private float jumpingHeight = .8f;
		private bool isMoving;
		private bool isJumping;
		private bool isSprinting;
		private float fallingDamageMultiplier = 2.0f;
		private float fallingAccleration;
		private float fallingTimer;
		private float fallingDamage;
		private float speed;
		private float walkingSpeed;
		private float sprintingSpeed;
		private float gravity;
		private bool freeze;
		public void Initialize()
		{
			player = GetComponent<Player>();
			characterController = player.characterController;
			cameraController = player.cameraController;
			movement = Vector3.zero;
			gameInputManager = GameInputManager.Instance;
			mobChar = player.GetCharacteristics();
			walkingSpeed = mobChar.GetFloat("WalkingSpeed");
			sprintingSpeed = mobChar.GetFloat("SprintingSpeed");
			gravity = 5.0f;
		}


		public void Jump()
		{
			if (DebugController.DEBUG_MODE) { return; }
			PrepareJump();
		}

		protected void PrepareJump()
		{
			jumpForce = -jumpingHeight;
			isJumping = true;
			StopCoroutine("StartJump");
			StartCoroutine("StartJump");

		}

		protected IEnumerator StartJump()
		{
			while (jumpForce < jumpingHeight)
			{
				jumpForce += Time.deltaTime * gravity;
				yield return new WaitForSeconds(Time.deltaTime);
			}

			isJumping = false;
		}


		public Vector3 GetMovement()
		{

			fallingAccleration = IsAirborne() ? fallingAccleration + Time.deltaTime : 1;
			speed = IsSprinting() ? sprintingSpeed : walkingSpeed;
			movement.x = gameInputManager.input.move.x * Time.deltaTime * this.speed;
			movement.y = jumpForce * Physics.gravity.y * Time.deltaTime * fallingAccleration;
			movement.z = gameInputManager.input.move.y * Time.deltaTime * this.speed;
			if (freeze) { return new Vector3(0, movement.y, 0); }
			isMoving = Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.z) > 0;
			if (player.gameInputManager.GetKeyDown(GameInputManager.SPRINT_KEYCODE) && player.GetFloat("Stamina") > 0 && IsMoving() && !IsAirborne())
			{
				isSprinting = true;
			}
			if (player.GetFloat("Stamina") <= 0 || player.gameInputManager.GetKeyUp(GameInputManager.SPRINT_KEYCODE) || !IsMoving()) { isSprinting = false; }

			return movement;
		}

		public void BobCamera()
		{
			if (cameraController == null) { return; }
			if (IsMoving())
			{
				cameraController.Bob(1.0f);
			} else
			{
				cameraController.ResetBob();
			}

		}

		public void CheckForFallDamage()
		{
			if (IsAirborne())
			{
				fallingTimer += Time.deltaTime;

				fallingDamage = fallingTimer > 1.0f ? (int)(fallingTimer * fallingAccleration) : 0;

			} else
			{
				player.SetFloat("Health", player.GetFloat("Health") - GetFallDamage());
				fallingDamage = fallingTimer = 0;
			}
		}

		private float GetFallDamage()
		{
			return fallingDamage * fallingAccleration * fallingDamageMultiplier;
		}

		public bool IsAirborne()
		{
			if (characterController == null) { return false; }
			return !characterController.isGrounded;
		}


		public bool IsMoving()
		{
			return isMoving;
		}

		public bool IsJumping()
		{
			return isJumping;
		}

		public bool IsSprinting()
		{
			return isSprinting;
		}


		public void SetWalkingSpeed(float speed)
		{
			this.walkingSpeed = speed;
		}

		public void SetJumpingHeight(float jumpingHeight)
		{
			this.jumpingHeight = jumpingHeight;
		}

		public void SetGravity(float gravity)
		{
			this.gravity = gravity;
		}

		public void FreezeMovement(bool b)
		{
			this.freeze = b;
		}
	}

}
