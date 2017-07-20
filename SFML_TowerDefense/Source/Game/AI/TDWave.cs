using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.Core;
using SFML_TowerDefense.Source.Game.Units;

namespace SFML_TowerDefense.Source.Game.AI
{
	public class TDWave : TDActor
	{
		public float SpawnSpeed { get; set; } = 1;
		public int Amount { get; set; } = 1;
		public TDUnit UnitType { get; set; }

		public TDWave(Level level) : base(level)
		{
			
		}
	}
}