using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public abstract class PhysicsComponent : ActorComponent
	{

		public virtual Color ComponentColor { get; set; } = new Color((byte) EngineMath.EngineRandom.Next(255),
			(byte) EngineMath.EngineRandom.Next(255), (byte) EngineMath.EngineRandom.Next(255));

		public virtual CollisionObject CollisionObject { get; set; }
		public abstract Vector2f CollisionBounds { get; }

		public virtual short CollisionResponseChannels { get; set; } = Convert.ToInt16(CollisionTypes.All);
		public virtual short CollisionType { get; set; } = Convert.ToInt16(CollisionTypes.None);

	}
}