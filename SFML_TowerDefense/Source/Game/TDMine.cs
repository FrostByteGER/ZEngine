using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Player;

namespace SFML_TowerDefense.Source.Game
{
	public class TDMine : TDBuilding
	{

		public float MineTime { get; set; } = 0;
		public float MineSpeed { get; set; } = 5;

		public int MineAmount { get; set; } = 1;

		public TDResource Resouce { get; set; }

		public TDPlayerController Owner { get; set; }

		public TDMine(Level level) : base(level)
		{
		}

		public void MineRecouse()
		{
			if (Resouce == null || Owner == null) return;
			if (Resouce.ResourceAmount > 0)
			{
				Owner.Money += Resouce.Mine(MineAmount);
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