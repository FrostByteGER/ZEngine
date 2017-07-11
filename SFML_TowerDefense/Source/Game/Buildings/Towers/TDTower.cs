using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public abstract class TDTower : TDBuilding
	{

		public TDTowerState TowerState { get; set; } = TDTowerState.Idle;
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
	}
}