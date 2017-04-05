﻿using UnityEngine;
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


			if (movementController.IsSprinting())
			{
				SetStamina(playerStamina - (Time.deltaTime * 40.0f));
			} else
			{
				SetStamina(playerStamina + (Time.deltaTime * 40.0f));
			}
		}


		public void SetHunger(float hunger)
		{
			if (hunger < 0) hunger = 0;
			if (hunger > 100) hunger = 100;

			player.SetFloat("Hunger", hunger);
		}

		public void SetHealth(float health)
		{
			if (health < 0) health = 0;
			if (health > 100) health = 100;

			player.SetFloat("Health", health);
		}

		public void SetStamina(float stamina)
		{

			if (stamina < 0) stamina = 0;
			if (stamina > 100) stamina = 100;
			player.SetFloat("Stamina", stamina);
		}

	}

}
