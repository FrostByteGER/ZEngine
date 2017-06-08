using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceShipActor : SpriteActor
	{
		public uint Healthpoints { get; set; } = 1;


		public SpaceShipActor(Sprite sprite, Level level) : base(sprite, level)
		{
		}




	}
}