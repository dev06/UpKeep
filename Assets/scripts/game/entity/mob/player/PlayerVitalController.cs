using UnityEngine;
using System.Collections;


namespace Game
{
	[RequireComponent (typeof(Player))]
	public class PlayerVitalController : MonoBehaviour {


		private Player player;
		private PlayerMovementController movementController;

		private float playerHealth;
		private float playerStamina;
		private float playerHunger;
		private float playerThirst;


		public void Initialize()
		{
			player = GetComponent<Player>();
			movementController = player.movementController;

		}


		public void UpdatePlayerVital()
		{
			playerHunger = player.GetFloat("Hunger");
			playerStamina = player.GetFloat("Stamina");
			playerHealth = player.GetFloat("Health");
			playerThirst = player.GetFloat("Thirst");


			if (movementController.IsSprinting())
			{
				SetStamina(playerStamina - (Time.deltaTime * 5.0f));
				SetHunger(playerHunger - (Time.deltaTime / 10f));
				SetThirst(playerThirst - (Time.deltaTime / 5.0f));
			} else
			{
				SetStamina(playerStamina + (Time.deltaTime * 7.0f));
				SetHunger(playerHunger - (Time.deltaTime / 15f));
				SetThirst(playerThirst - (Time.deltaTime / 10.0f));

			}
		}


		public void SetHunger(float hunger)
		{
			if (hunger < 0) { hunger = 0; }
			if (hunger > 100) { hunger = 100; }

			player.SetFloat("Hunger", hunger);
		}

		public void SetHealth(float health)
		{
			if (health < 0) { health = 0; }
			if (health > 100) { health = 100; }

			player.SetFloat("Health", health);
		}

		public void SetStamina(float stamina)
		{

			if (stamina < 0) { stamina = 0; }
			if (stamina > 100) { stamina = 100; }
			player.SetFloat("Stamina", stamina);
		}

		public void SetThirst(float thirst)
		{

			if (thirst < 0) { thirst = 0; }
			if (thirst > 100) { thirst = 100; }
			player.SetFloat("Thirst", thirst);
		}


	}

}
