using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Graphics
{
	public class SpriteActor : Actor
	{

		public SpriteComponent SpriteComp { get; private set; }

		public SpriteActor(Sprite sprite, Level level) : base(level)
		{
			SpriteComp = new SpriteComponent(sprite);
			var extents = new TVector2f(SpriteComp.Sprite.GetGlobalBounds().Width / 2.0f, SpriteComp.Sprite.GetGlobalBounds().Height / 2.0f);
			level.PhysicsEngine.ConstructRectangleCollisionComponent(this, true, new TVector2f(0.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 1.0f, extents, BodyType.Dynamic);
			AddComponent(SpriteComp);
			Origin = SpriteComp.Origin; // Center this actor.
		}

		protected internal override void InitializeActor()
		{
			base.InitializeActor();
		}

	}
}