using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Collision.Shapes;
using VelcroPhysics.Dynamics;
using CircleShape = VelcroPhysics.Collision.Shapes.CircleShape;

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
			set //TODO: Use or remove!
			{
				var collisionCallbacksEnabled = CollisionCallbacksEnabled;
				CollisionCallbacksEnabled = false;
				_collisionBody = value;
				CollisionBody.IsSensor = _canOverlap;
				CollisionCallbacksEnabled = collisionCallbacksEnabled;
			}
		}

		private Category _collisionResponseChannels = Category.All;

		public Category CollisionResponseChannels
		{
			get => _collisionResponseChannels;
			set
			{
				_collisionResponseChannels = value;
				CollisionBody.CollidesWith = value;
			}
		}

		private Category _collisionType = Category.Cat1;
		public Category CollisionType
		{
			get => _collisionType;
			set
			{
				_collisionType = value;
				CollisionBody.CollisionCategories = value;
			}
		}

		private bool _canOverlap = false;
		public bool CanOverlap
		{
			get => _canOverlap;
			set
			{
				_canOverlap = value;
				if(CollisionBody != null) CollisionBody.IsSensor = value;

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
			get => base.LocalPosition;
			set
			{
				base.LocalPosition = value;
				CollisionBody.Position = VelcroPhysicsEngine.ToPhysicsUnits(value);
			}
		}

		public override float LocalRotation
		{
			get => base.LocalRotation;
			set
			{
				base.LocalRotation = value;
				CollisionBody.Rotation = VelcroPhysicsEngine.ToPhysicsUnits(value);
			}
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
			foreach (var fixture in CollisionBody.FixtureList)
			{
				if (fixture.Shape is PolygonShape)
				{
					Level.CollisionRectangle.Position = WorldPosition;
					Level.CollisionRectangle.Rotation = LocalRotation;
					Level.CollisionRectangle.Scale = LocalScale;
					Level.CollisionRectangle.Origin = Origin;
					Level.CollisionRectangle.Size = ComponentBounds * 2.0f;
					target.Draw(Level.CollisionRectangle, states);
				}
				else if (fixture.Shape is CircleShape)
				{
					Level.CollisionCircle.Position = WorldPosition;
					Level.CollisionCircle.Rotation = LocalRotation;
					Level.CollisionCircle.Scale = LocalScale;
					Level.CollisionCircle.Origin = Origin;
					Level.CollisionCircle.Radius = fixture.Shape.Radius;
					target.Draw(Level.CollisionCircle, states);
				}
			}
			
		}
	}
}