using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.TileMap;

namespace SFML_TowerDefense.Source.Game.AI
{
	public class TDWaypoint : TDFieldActor
	{
		public TDWaypoint NextWaypoint { get; set; }

		public TDWaypoint(Level level) : base(level)
		{
			SetRootComponent(new ActorComponent());
		}
	}
}