using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using system;
using UpkeepInput;
namespace Game
{

	[RequireComponent (typeof(PlayerWeaponController))]
	[RequireComponent (typeof(PlayerActionController))]
	[RequireComponent (typeof(PlayerVitalController))]
	[RequireComponent (typeof(PlayerLookController))]
	[RequireComponent (typeof(PlayerMovementController))]
	[RequireComponent (typeof(CharacterController))]
	public class Player : Mob
	{


		public CharacterController characterController;
		public Transform back;
		public ItemInHand itemInHand;
		public CameraController cameraController;
		public PlayerMovementController movementController;
		public PlayerVitalController vitalController;
		public PlayerActionController actionController;
		public PlayerWeaponController weaponController;
		public Transform lookPoint;
		private CameraBob cameraBob;
		private PlayerLookController lookController;
		private StateManager stateManager;




		void OnEnable()
		{
			EventManager.OnUseItem += OnUseItem;
			EventManager.OnDropItem += OnDropItem;
		}

		void OnDisable()
		{
			EventManager.OnUseItem -= OnUseItem;
			EventManager.OnDropItem -= OnDropItem;
		}

		private void Start()
		{
			base.Start();
			Initialize();

		}

		private void Initialize()
		{
			characterController = GetComponent<CharacterController>();
			cameraController = FindObjectOfType<CameraController>();
			lookPoint = cameraController.transform;

			cameraBob = Camera.main.GetComponent<CameraBob>();
			animator = GetComponent<Animator>();

			mobChar.SetAll(3.0f, 5.0f, 100.0f, 100.0f, 10.0f, 3.0f, 2.0f, 100.0f, 100.0f);


			stateManager = StateManager.Instance;

			vitalController = GetComponent<PlayerVitalController>();
			lookController = GetComponent<PlayerLookController>();
			movementController = GetComponent<PlayerMovementController>();
			actionController = GetComponent<PlayerActionController>();
			weaponController = GetComponent<PlayerWeaponController>();

			vitalController.Initialize();
			lookController.Initialize();
			movementController.Initialize();
			actionController.Initialize();
			weaponController.Initialize();
			cameraController.Initialize();
			cameraController.SetPlayer(this);
			bulletHole = (GameObject)Resources.Load("BulletHole");
		}



		public GameObject bulletHole;
		private void Update()
		{
			if (gameController.isGamePaused) { return; }

			UpdateMovement();

			UpdateLook();

			UpdatePlayerVital();

			UpdatePlayerAction();

			if (Input.GetMouseButtonDown(0) && stateManager.state != StateManager.State.INVENTORY && !movementController.IsSprinting())
			{
				//	WeaponController.Attack(Camera.main.transform);
				weaponController.Attack(Camera.main.transform);
				cameraController.TriggerRecoil();


			}





		}




		private void UpdateMovement()
		{
			movementController.CheckForFallDamage();

			if (Input.GetKey(KeyCode.Space)  && characterController.isGrounded)
			{
				movementController.Jump();
			}
			movementController.BobCamera();

			Vector3 calcuatedMovement = movementController.GetMovement();

			calcuatedMovement = transform.rotation * calcuatedMovement;

			characterController.Move(calcuatedMovement);
		}


		private void UpdateLook()
		{
			Vector2 lookRotation = lookController.GetLookRotation();
			transform.Rotate(0, lookRotation.x, 0);
			back.localRotation = Quaternion.Euler(lookRotation.y + 90, 0, 0);
		}

		private void UpdatePlayerVital()
		{
			vitalController.UpdatePlayerVital();
		}


		private void UpdatePlayerAction()
		{
			actionController.UpdatePlayerAction();
		}


		void OnUseItem(Item item)
		{
			switch (item.objectType)
			{
				case ObjectType.Consumable:
				{
					UseConsumable((Consumable)item);
					break;
				}

				case ObjectType.Weapon:
				{
					if (itemInHand.GetItem() == null)
					{
						weaponController.EquipWeapon((Weapon)item, itemInHand.transform);
						cameraController.SetRecoilWeaponValue((Weapon)item);
						itemInHand.SetItem(item);
					}
					break;
				}
			}
		}

		void OnDropItem(Item item)
		{
			ObjectSpawnerController.SpawnObjectRelativeTo(item.objectID, transform);
			if (item is Weapon)
			{
				itemInHand.SetItem(null);
				cameraController.SetRecoilWeaponValue(null);
				weaponController.SetEquippedWeapon(null);
			}
		}



		void UseConsumable(Consumable item)
		{
			SetFloat("Hunger", GetFloat("Hunger") + item.hungerGain);
			SetFloat("Stamina", GetFloat("Stamina") + item.staminaGain);
			SetFloat("Health", GetFloat("Health") + item.healthGain);
			SetFloat("Thirst", GetFloat("Thirst") + item.thirstGain);
		}



		public MobCharacteristics GetCharacteristics()
		{
			return mobChar;
		}


		public Animator GetAnimator()
		{
			return animator;
		}

		public Item GetItemInHand()
		{
			return itemInHand.GetItem();
		}

	}
}
