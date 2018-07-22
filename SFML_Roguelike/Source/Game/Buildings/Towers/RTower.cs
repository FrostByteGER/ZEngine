using SFML_Engine.Engine.Game;
using SFML_Roguelike.Source.Game.Player;

namespace SFML_Roguelike.Source.Game.Buildings.Towers
{
	public abstract class RTower : RBuilding
	{

		public TDTowerState TowerState { get; set; } = TDTowerState.Idle;
		public TDTowerBaseComponent TowerBase { get; set; }
		protected RTower(Level level) : base(level)
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
			var pc = LevelReference.FindPlayer<RPlayerController>(0);
			pc.Gold += (uint)returnedGold;
			LevelReference.DestroyActor(this);
		}
	}
}