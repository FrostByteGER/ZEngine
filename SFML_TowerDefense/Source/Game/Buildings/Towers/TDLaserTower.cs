using SFML.Graphics;
using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game.Buildings.Towers
{
	public class TDLaserTower : TDTower
	{
		public TDLaserTower(Level level) : base(level)
		{
		}

		protected override void CreateTower()
		{
			TowerBase = new TDTowerBaseComponent(new Sprite(new Texture("")));
			SetRootComponent(TowerBase);
			
		}
	}
}