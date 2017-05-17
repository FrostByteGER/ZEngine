using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Physics
{
	public abstract class PhysicsComponent : ActorComponent, ICollidable
	{

		public virtual Color ComponentColor { get; set; } = new Color((byte) EngineMath.EngineRandom.Next(255),
			(byte) EngineMath.EngineRandom.Next(255), (byte) EngineMath.EngineRandom.Next(255));

		public virtual CollisionObject CollisionObject { get; set; }
		public abstract TVector2f CollisionBounds { get; }

		public virtual short CollisionResponseChannels { get; set; } = Convert.ToInt16(CollisionTypes.All);
		public virtual short CollisionType { get; set; } = Convert.ToInt16(CollisionTypes.None);

		public bool CollisionCallbacksEnabled
		{
			get { return CollisionObject.CollisionFlags.HasFlag(CollisionFlags.CustomMaterialCallback); }
			set
			{
				if (!CollisionCallbacksEnabled && value)
				{
					CollisionObject.CollisionFlags |= CollisionFlags.CustomMaterialCallback;
					ManifoldPoint.ContactAdded += OnCollide;
				}
				else if(CollisionCallbacksEnabled && !value)
				{
					CollisionObject.CollisionFlags &= ~CollisionFlags.CustomMaterialCallback;
					ManifoldPoint.ContactAdded -= OnCollide;
				}
			}
		}

		public override TVector2f ComponentBounds
		{
			get => CollisionBounds;
			set { } // Since CollisionBounds are computed and handled by Bullet, we will do nothing here.
		}

		public virtual void OnCollide(ManifoldPoint cp, CollisionObjectWrapper collider1, int partId1, int index1, CollisionObjectWrapper collider2, int partId2, int index2)
		{
			ParentActor.OnCollide(cp, collider1, partId1, index1, collider2, partId2, index2);
			//Console.WriteLine("COLLISION: " + (collider1.CollisionObject.UserObject as ActorComponent) + " WITH " + (collider2.CollisionObject.UserObject as ActorComponent));
		}
	}
}