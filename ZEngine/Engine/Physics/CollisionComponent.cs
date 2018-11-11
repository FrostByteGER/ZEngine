using SFML.Graphics;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Physics
{
	public class CollisionComponent : PhysicsComponent
	{
		public override Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public CollisionComponent()
		{
			CanOverlap = false;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}