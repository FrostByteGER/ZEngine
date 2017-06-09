using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Graphics
{
	public class SpriteActor : PhysicsActor
	{

		public SpriteComponent SpriteComp { get; private set; }

		public SpriteActor(Sprite sprite, Level level) : base(PhysicsType.Rectangle, BodyType.Dynamic, 1.0f, new TVector2f(sprite.GetGlobalBounds().Width / 2.0f, sprite.GetGlobalBounds().Height / 2.0f), false, level)
		{
			SpriteComp = new SpriteComponent(sprite);
			AddComponent(SpriteComp);
			Origin = SpriteComp.Origin; // Center this actor.
		}

		protected internal override void InitializeActor()
		{
			base.InitializeActor();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			//Console.WriteLine(Position);
		}
	}
}