using SFML_Engine.Engine.Game;
using SFML_TowerDefense.Source.Game.TileMap;

namespace SFML_TowerDefense.Source.Game.Units
{
	public class TDWaypoint : TDFieldActor
	{
		public TDWaypoint NextWaypoint { get; set; }
		public uint TargetNexus { get; set; } = 0;

		public TDWaypoint(Level level) : base(level)
		{
			SetRootComponent(new ActorComponent());
			//var mineSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("OreRefinery")));
			//SetRootComponent(mineSprite);
			//Origin = mineSprite.Origin;
		}
	}
}