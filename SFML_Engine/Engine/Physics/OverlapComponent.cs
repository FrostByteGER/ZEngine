﻿using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class OverlapComponent : PhysicsComponent
	{

		public GhostObject OverlapBody { get; set; }

		public sealed override CollisionObject CollisionObject
		{
			get => base.CollisionObject;
			set
			{
				var rigid = (GhostObject)value;
				if (rigid != null) OverlapBody = rigid;
				base.CollisionObject = value;
				OverlapBody.UserObject = this;
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
				ParentActor?.LevelReference?.EngineReference?.BulletPhysicsEngine?.ModifyGhostObject(OverlapBody, CollisionType, value);
			}
		}

		public override short CollisionType
		{
			get => base.CollisionType;
			set
			{
				base.CollisionType = value;
				ParentActor?.LevelReference?.EngineReference?.BulletPhysicsEngine?.ModifyGhostObject(OverlapBody, value, CollisionResponseChannels);
			}
		}

		public override Vector2f CollisionBounds
		{
			get
			{
				var boxShape = OverlapBody.CollisionShape as BoxShape;
				if (boxShape != null) return EngineMath.Vec3ToVec2f(boxShape.HalfExtentsWithoutMargin);

				var sphereShape = OverlapBody.CollisionShape as SphereShape;
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
				var transform = OverlapBody.WorldTransform;
				transform.Origin = EngineMath.Vec2fToVec3(value);
				OverlapBody.WorldTransform = transform;
			}
		}

		public OverlapComponent()
		{
			
		}

		public OverlapComponent(GhostObject overlapBody)
		{
			CollisionObject = overlapBody ?? throw new ArgumentNullException(nameof(overlapBody));
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			OverlapBody.Dispose();
		}
	}
}