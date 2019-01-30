using Exofinity.Source.Game.Player;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Buildings.Towers
{
	public abstract class RTower : RBuilding
	{

		public TDTowerState TowerState { get; set; } = TDTowerState.Idle;
		public TDTowerBaseComponent TowerBase { get; set; }
		protected RTower()
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