using UnityEngine;

namespace Game
{
	public class Consumable : Item
	{

		public float healthGain;
		public float staminaGain;
		public float thirstGain;
		public float hungerGain;

		public Consumable()
		{

		}

		public Consumable(float healthGain, float hungerGain, float staminaGain, float thirstGain)
		{
			this.healthGain = healthGain;
			this.hungerGain = hungerGain;
			this.staminaGain = staminaGain;
			this.thirstGain = thirstGain;
		}

		public Consumable(string healthGain, string hungerGain, string staminaGain, string thirstGain, string description, int quantity)
		{
			this.healthGain = float.Parse(healthGain);
			this.hungerGain = float.Parse(hungerGain);
			this.staminaGain = float.Parse(staminaGain);
			this.thirstGain = float.Parse(thirstGain);
			this.objectDescription = description;
			this.objectQuantity = quantity;
		}


	}
}
