using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class CollisionComponent : PhysicsComponent
	{
		public override Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public override TVector2f CollisionBounds
		{
			get { throw new NotImplementedException(); }
		}

		public CollisionComponent()
		{
			
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}
	}
}