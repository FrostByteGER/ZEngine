using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public class OverlapComponent : ActorComponent
	{

		public GhostObject OverlapBody { get; set; }

		public Color ComponentColor { get; set; } = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));

		public Vector2f CollisionBounds
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

		public OverlapComponent(GhostObject overlapBody)
		{
			OverlapBody = overlapBody ?? throw new ArgumentNullException(nameof(overlapBody));
			Origin = CollisionBounds;
		}

		public override void Destroy(bool disposing)
		{
			base.Destroy(disposing);
			OverlapBody.Dispose();
		}
	}
}