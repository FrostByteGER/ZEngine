using SFML.Graphics;
using VelcroPhysics.Dynamics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Physics;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Graphics
{
	public class SpriteActor : PhysicsActor
	{

		public SpriteComponent SpriteComp { get; private set; }

		public SpriteActor(Sprite sprite) : base(PhysicsType.Rectangle, BodyType.Dynamic, 1.0f, new Vector2(sprite.GetGlobalBounds().Width / 2.0f, sprite.GetGlobalBounds().Height / 2.0f), false, false, false)
		{
			SpriteComp = new SpriteComponent(sprite);
			AddComponent(SpriteComp);
			Origin = SpriteComp.Origin; // Center this actor.
		}
	}
}