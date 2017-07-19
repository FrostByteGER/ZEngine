﻿using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game
{
	public class TDResource : TDFieldActor
	{

		public int ResourceAmount = 100;

		/// <summary>
		/// Is this Resourcefield depleted or not.
		/// </summary>
		public bool Depleted => ResourceAmount == 0;

		public TDResource(Level level) : base(level)
		{
		}

		public int Mine(int amount)
		{
			

			if (ResourceAmount - amount < 0)
			{
				var end = ResourceAmount;
				ResourceAmount = 0;
				return end;
			}

			ResourceAmount -= amount;

			return amount;
		}
	}
}