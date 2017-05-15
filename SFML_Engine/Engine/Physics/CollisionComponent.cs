using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class CollisionComponent : PhysicsComponent
	{
		
		public RigidBody CollisionBody { get; private set; }

		public sealed override CollisionObject CollisionObject
		{
			get => base.CollisionObject;
			set
			{
				var rigid = (RigidBody)value;
				if (rigid != null) CollisionBody = rigid;
				base.CollisionObject = value;
				CollisionBody.UserObject = this;
				value.UserObject = this;
				Origin = CollisionBounds;
			}
		}

		public override Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public override short CollisionResponseChannels
		{
			get => base.CollisionResponseChannels;
			set
			{
				base.CollisionResponseChannels = value;
				ParentActor?.LevelReference?.EngineReference?.BulletPhysicsEngine?.ModifyRigidBody(CollisionBody, CollisionType, value);
			}
		}

		public override short CollisionType
		{
			get => base.CollisionType;
			set
			{
				base.CollisionType = value;
				ParentActor?.LevelReference?.EngineReference?.BulletPhysicsEngine?.ModifyRigidBody(CollisionBody, value, CollisionResponseChannels);
			}
		}

		public override Vector2f CollisionBounds
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

		public override Vector2f Position
		{
			get => base.Position;
			set
			{
				base.Position = value;
				var transform = CollisionBody.WorldTransform;
				transform.Origin = EngineMath.Vec2fToVec3(value);
				CollisionBody.WorldTransform = transform;
			}
		}

		public override Vector2f LocalPosition
		{
			get { return base.LocalPosition; }
			set { base.LocalPosition = value; }
		}

		public override float Rotation
		{
			get { return base.Rotation; }
			set
			{
				base.Rotation = value;
			}
		}

		public override float LocalRotation
		{
			get { return base.LocalRotation; }
			set { base.LocalRotation = value; }
		}

		public CollisionComponent()
		{
			
		}

		public CollisionComponent(RigidBody collisionBody)
		{
			CollisionObject = collisionBody ?? throw new ArgumentNullException(nameof(collisionBody));
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			CollisionBody.Dispose();
		}
	}
}