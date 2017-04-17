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
		public PlayerLookController lookController;
		public Transform lookPoint;
		public GameObject bulletHole;
		private CameraBob cameraBob;
		public StateManager stateManager;
		public GameInputManager gameInputManager;




		void OnEnable()
		{
			EventManager.OnUseItem += OnUseItem;
			EventManager.OnDropItem += OnDropItem;
			EventManager.OnObjectPickup += OnObjectPickup;
		}

		void OnDisable()
		{
			EventManager.OnUseItem -= OnUseItem;
			EventManager.OnDropItem -= OnDropItem;
			EventManager.OnObjectPickup -= OnObjectPickup;
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
			gameInputManager = GameInputManager.Instance;
			cameraBob = Camera.main.GetComponent<CameraBob>();
			animator = GetComponent<Animator>();

			mobChar.SetAll(3.0f, 6.0f, 100.0f, 100.0f, 10.0f, 3.0f, 2.0f, 100.0f, 100.0f);


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




		private void Update()
		{



			if (gameController.isGamePaused) { return; }

			UpdateMovement();

			UpdateLook();

			UpdatePlayerVital();

			UpdatePlayerAction();

			if (gameInputManager.GetMouseButton(0) && stateManager.state != StateManager.State.INVENTORY && !movementController.IsSprinting())
			{

				weaponController.Attack(Camera.main.transform);
				cameraController.TriggerRecoil();

			}
		}


		private void LateUpdate()
		{

			actionController.LateUpdatePlayerAction();
		}


		private void UpdateMovement()
		{
			movementController.CheckForFallDamage();

			if (gameInputManager.GetKey(GameInputManager.JUMP_KEYCODE)  && characterController.isGrounded)
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
			if (item == null) { return; }
			if (item.objectType == ObjectType.Consumable)
			{
				InventoryManager.Instance.RemoveItem(item);
				UseConsumable((Consumable)item);
				return;
			}

			// if (itemInHand.GetItem() == null)
			// {
			// 	EquipItem(item);
			// }

			//EquipItem(item);
			// if (itemInHand.GetItem() == null)
			// {
			// 	EquipItem(item);

			// } else
			// {
			// 	if (item.objectID != itemInHand.GetItem().objectID)
			// 	{
			// 		UnequipItem();

			// 		EquipItem(item);

			// 	} else
			// 	{
			// 		UnequipItem();
			// 	}
			// }

			ItemManager.currentItemInHand = itemInHand.GetItem();
		}



		void OnDropItem(Item item)
		{
			ObjectSpawnerController.SpawnObjectRelativeTo(item.objectID, transform);

			if (item is Weapon)
			{
				cameraController.SetRecoilWeaponValue(null);
				weaponController.SetEquippedWeapon(null);
			}


			itemInHand.SetItem(null);
		}

		void OnObjectPickup(Game.Object obj)
		{
			// Actually puts the weapon in hand if the hand is empty
			// if (obj is Weapon && itemInHand.GetItem() == null)
			// {
			// 	Weapon w = (Weapon)obj;
			// 	weaponController.EquipWeapon(w, itemInHand.transform);
			// 	cameraController.SetRecoilWeaponValue(w);
			// 	itemInHand.SetItem((Item)obj);
			// }
			// ItemManager.currentItemInHand = itemInHand.GetItem();
		}



		void UseConsumable(Consumable item)
		{

			vitalController.SetHunger(GetFloat("Hunger") + item.hungerGain);
			vitalController.SetStamina(GetFloat("Stamina") + item.staminaGain);
			vitalController.SetHealth(GetFloat("Health") + item.healthGain);
			vitalController.SetThirst(GetFloat("Thirst") + item.thirstGain);
		}

		public void EquipItem(Item item)
		{
			UnequipItem();

			if (item == null) {
				itemInHand.SetItem(item);
				return;
			}

			if (item is Weapon)
			{
				Weapon weapon = (Weapon) item;
				weaponController.EquipWeapon(weapon, itemInHand.transform);
				cameraController.SetRecoilWeaponValue(weapon);
			} else
			{
				ItemManager.EquipItemInHand(item, itemInHand.transform);
			}

			itemInHand.SetItem(item);
		}

		public void UnequipItem()
		{
			if (itemInHand.GetItem() == null) { return; }

			if (itemInHand.GetItem() is Weapon)
			{
				weaponController.UnequipWeapon();
				cameraController.SetRecoilWeaponValue(null);
			}

			itemInHand.SetItem(null);


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
