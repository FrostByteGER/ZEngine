using SFML_Engine.Engine.Game;

namespace SFML_TowerDefense.Source.Game
{
	public class TDBuilding : TDFieldActor
	{
		public int Cost { get; set; } = 0;


		public TDBuilding(Level level) : base(level)
		{
		}
	}
}