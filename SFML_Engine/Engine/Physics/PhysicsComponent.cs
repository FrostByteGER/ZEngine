using SFML.Graphics;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Primitives;
using Transform = VelcroPhysics.Primitives.Transform;

namespace SFML_Engine.Engine.Physics
{
	public abstract class PhysicsComponent : RenderComponent, ICollidable
	{
		
		public virtual Color ComponentColor { get; set; } = new Color((byte) EngineMath.EngineRandom.Next(255),
			(byte) EngineMath.EngineRandom.Next(255), (byte) EngineMath.EngineRandom.Next(255));


		private Body _collisionBody;
		public virtual Body CollisionBody
		{
			get => _collisionBody;
			private set //TODO: Use or remove!
			{
				var collisionCallbacksEnabled = CollisionCallbacksEnabled;
				CollisionCallbacksEnabled = false;
				_collisionBody = value;
				CollisionBody.IsSensor = _canOverlap;
				CollisionCallbacksEnabled = collisionCallbacksEnabled;
			}
		}

		public override TVector2f ComponentBounds
		{
			get
			{
				var aabb = new AABB();
				foreach (var fixture in CollisionBody.FixtureList)
				{
					var aabbLocal = new AABB();
					var transformable = new Transform();
					CollisionBody.GetTransform(out transformable);
					fixture.Shape.ComputeAABB(out aabbLocal, ref transformable, 0);
					aabb.Combine(ref aabbLocal);
				}
				_componentBounds = aabb.Extents;
				return aabb.Extents;
			}

			set { } // Since CollisionBounds are computed and handled by Velcro, we will do nothing here.
		}

		public Category CollisionResponseChannels { get; set; } = Category.All;
		public Category CollisionType { get; set; } = Category.Cat1;

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

		public override TVector2f LocalPosition
		{
			get => CollisionBody.Position;
			set => CollisionBody.Position = value;
		}

		public override float LocalRotation
		{
			get => CollisionBody.Rotation;
			set => CollisionBody.Rotation = value;
		}

		public virtual void OnCollide(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnCollideEnd(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public override void Draw(RenderTarget target, RenderStates states)
		{
			base.Draw(target, states);
			//TODO: Draw Global CollisionShape.
		}
	}
}