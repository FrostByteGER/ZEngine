using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Physics;
using SFML_TowerDefense.Source.Game.Player;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public abstract class TDTower : TDBuilding
	{

		public TDTowerState TowerState { get; set; } = TDTowerState.Idle;
		public TDTowerBaseComponent TowerBase { get; set; }
		protected TDTower(Level level) : base(level)
		{
			
		}

		protected override void InitializeActor()
		{
			CreateTower();
			// Call last as we still add components.
			base.InitializeActor();
		}

		protected abstract void CreateTower();

		public void ScrapTower()
		{
			var returnedGold = Cost * ScrapMultiplier;
			var pc = LevelReference.FindPlayer<TDPlayerController>(0);
			pc.Gold += (uint)returnedGold;
			LevelReference.DestroyActor(this);
		}
	}
}