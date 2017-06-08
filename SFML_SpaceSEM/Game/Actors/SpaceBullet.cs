using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;

namespace SFML_SpaceSEM.Game.Actors
{
	public class SpaceBullet : SpriteActor
	{
		public uint Damage { get; set; } = 1;
		public SpaceBullet(Sprite sprite, Level level) : base(sprite, level)
		{
		}
	}
}