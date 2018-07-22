using SFML_Engine.Engine.Game;
using SFML_Roguelike.Source.Game.TileMap;

namespace SFML_Roguelike.Source.Game.Units
{
	public class RWaypoint : RFieldActor
	{
		public RWaypoint NextWaypoint { get; set; }
		public uint TargetNexus { get; set; } = 0;

		public RWaypoint(Level level) : base(level)
		{
			SetRootComponent(new ActorComponent());
			//var mineSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("OreRefinery")));
			//SetRootComponent(mineSprite);
			//Origin = mineSprite.Origin;
		}
	}
}