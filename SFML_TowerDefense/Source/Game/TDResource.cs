using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game
{
	public class TDResource : TDFieldActor
	{

		public int ResourceAmount = 100;

		public TDResource(Level level) : base(level)
		{
		}

		public int Mine(int Amount)
		{
			int end = ResourceAmount;

			if (ResourceAmount - Amount < 0)
			{
				end = ResourceAmount;
				ResourceAmount = 0;
				return end;
			}

			ResourceAmount -= Amount;

			return Amount;
		}
	}
}