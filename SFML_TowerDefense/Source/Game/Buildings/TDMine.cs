using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Player;

namespace SFML_TowerDefense.Source.Game.Buildings
{
	public class TDMine : TDBuilding
	{

		public float MineTime { get; set; } = 0;
		public float MineSpeed { get; set; } = 5;

		public uint MineAmount { get; set; } = 1;

		public TDResource ResourceField { get; set; }

		public TDPlayerController Owner { get; set; }

		public TDMine(Level level) : base(level)
		{
		}

		public void MineResource()
		{
			if (ResourceField == null || Owner == null) return;
			if (ResourceField.ResourceAmount > 0)
			{
				Owner.Gold += ResourceField.Mine(MineAmount);
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			if (MineTime <= 0)
			{
				MineResource();
				MineTime += MineSpeed;
			}
			MineTime -= deltaTime;
		}
	}
}