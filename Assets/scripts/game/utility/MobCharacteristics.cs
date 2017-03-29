using UnityEngine;


namespace Game
{
	public class MobCharacteristics
	{
		public float health;
		public float stamina;
		public float walkingSpeed;
		public float spritingSpeed;
		public float maxHealth;
		public float maxStamina;
		public float meleeDamage;
		public float staminaDepletionRate;
		public float staminaRepletionRate;
		public float hunger;
		public float thirst;


		public MobCharacteristics()
		{

		}

		public float GetFloat(string id)
		{
			switch (id)
			{
				case "Health" : return health;
				case "Stamina" : return stamina;
				case "WalkingSpeed": return walkingSpeed;
				case "SprintingSpeed": return spritingSpeed;
				case "MaxHealth": return maxHealth;
				case "MaxStamina": return maxStamina;
				case "MeleeDamage": return meleeDamage;
				case "StaminaDepletionRate": return staminaDepletionRate;
				case "StaminaRepletionRate": return staminaRepletionRate;
				case "Hunger": return hunger;
				case "Thirst": return thirst;
			}

			return -1;
		}


		public void SetFloat(string id, float value)
		{
			switch (id)
			{
				case "Health" : health = health > 0 ? value : 0 ; break;
				case "Stamina" : stamina = stamina > 0 ? value : 0; break;
				case "WalkingSpeed": walkingSpeed = value; break;
				case "SprintingSpeed": spritingSpeed = value; break;
				case "MaxHealth": maxHealth = value; break;
				case "MaxStamina": maxStamina = value; break;
				case "MeleeDamage": meleeDamage = value; break;
				case "StaminaDepletionRate": staminaDepletionRate = value; break;
				case "StaminaRepletionRate": staminaRepletionRate = value; break;
				case "Hunger": hunger = value; break;
				case "Thirst": thirst = value; break;
			}
		}

		public void SetAll(float walkingSpeed = 0,
		                   float spritingSpeed = 0,
		                   float maxHealth = 0,
		                   float maxStamina = 0,
		                   float meleeDamage = 0,
		                   float staminaDepletionRate = 0,
		                   float staminaRepletionRate = 0,
		                   float hunger = 0,
		                   float thirst = 0)
		{
			this.walkingSpeed = walkingSpeed;
			this.spritingSpeed = spritingSpeed;
			this.maxHealth = maxHealth;
			this.maxStamina = maxStamina;
			this.meleeDamage = meleeDamage;
			this.staminaRepletionRate = staminaRepletionRate;
			this.staminaDepletionRate = staminaDepletionRate;
			this.hunger = hunger;
			this.thirst = thirst;
			health = maxHealth;
			stamina = maxStamina;
		}
	}
}