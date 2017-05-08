using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class CollisionComponent : ActorComponent
	{
		
		public RigidBody CollisionBody { get; set; }

		public Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public Vector2f CollisionBounds
		{
			get
			{
				var boxShape = CollisionBody.CollisionShape as BoxShape;
				if (boxShape != null) return EngineMath.Vec3ToVec2f(boxShape.HalfExtentsWithoutMargin);

				var sphereShape = CollisionBody.CollisionShape as SphereShape;
				if (sphereShape != null) return new Vector2f(sphereShape.Radius, 0.0f);

				//TODO: Do check
				return new Vector2f();
			}
		}

		public CollisionComponent(RigidBody collisionBody)
		{
			CollisionBody = collisionBody ?? throw new ArgumentNullException(nameof(collisionBody));
		}
	}
}