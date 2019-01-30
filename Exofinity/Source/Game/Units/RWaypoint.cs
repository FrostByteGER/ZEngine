using Exofinity.Source.Game.TileMap;
using ZEngine.Engine.Game;

namespace Exofinity.Source.Game.Units
{
	public class RWaypoint : RFieldActor
	{
		public RWaypoint NextWaypoint { get; set; }
		public uint TargetNexus { get; set; } = 0;

		public RWaypoint()
		{
			SetRootComponent(new ActorComponent());
			//var mineSprite = new SpriteComponent(new Sprite(level.EngineReference.AssetManager.LoadTexture("OreRefinery")));
			//SetRootComponent(mineSprite);
			//Origin = mineSprite.Origin;
		}
	}
}