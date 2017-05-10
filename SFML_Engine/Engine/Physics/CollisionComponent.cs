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
				if (sphereShape != null) return new Vector2f(sphereShape.Radius, sphereShape.Radius);

				//TODO: Do check
				return new Vector2f();
			}
		}

		public CollisionComponent(RigidBody collisionBody)
		{
			CollisionBody = collisionBody ?? throw new ArgumentNullException(nameof(collisionBody));
			CollisionBody.UserObject = this;
			Origin = CollisionBounds;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			Console.WriteLine(CollisionBody.WorldTransform.Origin);
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			CollisionBody.Dispose();
		}
	}
}