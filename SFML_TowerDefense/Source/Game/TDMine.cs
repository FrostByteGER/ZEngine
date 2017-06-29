using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Player;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMine : TDBuilding
	{

		public float MineTime = 0;
		public float MineSpeed = 5;

		public int MineAmount = 1;

		public TDResource Resouce;

		public TDPlayerController Owner;

		public TDMine(Level level) : base(level)
		{
		}

		public void MineRecouse()
		{
			if (Resouce.ResourceAmount > 0)
			{
				int amount = Resouce.Mine(MineAmount);

				// TODO Give Owner Gold
				//Owner
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (MineTime <= 0)
			{
				MineRecouse();
				MineTime += MineSpeed;
			}
			MineTime -= deltaTime;
		}
	}
}