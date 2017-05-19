using System;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Collision.Narrowphase;
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Physics
{
	public abstract class PhysicsComponent : ActorComponent, ICollidable
	{
		
		public virtual Color ComponentColor { get; set; } = new Color((byte) EngineMath.EngineRandom.Next(255),
			(byte) EngineMath.EngineRandom.Next(255), (byte) EngineMath.EngineRandom.Next(255));

		public virtual Body CollisionBody { get; set; }
		public abstract TVector2f CollisionBounds { get; }

		public int CollisionResponseChannels { get; set; } = Convert.ToInt32(Category.All);
		public int CollisionType { get; set; } = Convert.ToInt32(Category.Cat1);

		private bool _canOverlap = false;
		public bool CanOverlap
		{
			get => _canOverlap;
			set
			{
				_canOverlap = value;
				CollisionBody.IsSensor = value;
			}
		}

		private bool _collisionCallbacksEnabled = false;
		public bool CollisionCallbacksEnabled
		{
			get => _collisionCallbacksEnabled;
			set
			{
				if (!CollisionCallbacksEnabled && value)
				{
					if (CanOverlap)
					{
						CollisionBody.OnCollision += OnOverlapBegin;
						CollisionBody.OnSeparation += OnOverlapEnd;
					}
					else
					{
						CollisionBody.OnCollision += OnCollide;
						CollisionBody.OnSeparation += OnCollideEnd;
					}
					
				}
				else if(CollisionCallbacksEnabled && !value)
				{
					if (CanOverlap)
					{
						CollisionBody.OnCollision -= OnOverlapBegin;
						CollisionBody.OnSeparation -= OnOverlapEnd;
					}
					else
					{
						CollisionBody.OnCollision -= OnCollide;
						CollisionBody.OnSeparation -= OnCollideEnd;
					}
				}
				_collisionCallbacksEnabled = value;
			}
		}

		public virtual void OnCollide(Fixture otherActor, Fixture self, Contact contactInfo)
		{

		}

		public virtual void OnCollideEnd(Fixture otherActor, Fixture self, Contact contactInfo)
		{

		}

		public virtual void OnOverlapBegin(Fixture otherActor, Fixture self, Contact contactInfo)
		{

		}

		public virtual void OnOverlapEnd(Fixture otherActor, Fixture self, Contact contactInfo)
		{

		}


		public override TVector2f ComponentBounds
		{
			get => CollisionBounds;
			set { } // Since CollisionBounds are computed and handled by Bullet, we will do nothing here.
		}
	}
}