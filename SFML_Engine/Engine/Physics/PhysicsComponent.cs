using System;
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

		public override bool Movable
		{
			get => CollisionBody.IsDynamic;
			set => CollisionBody.BodyType = value ? BodyType.Dynamic : BodyType.Static;
		}

		public TVector2f Velocity
		{
			get => VelcroPhysicsEngine.ToGameUnits(CollisionBody.LinearVelocity);
			set
			{
				var linVel = value;
				// Clamp the value to +-MaxVelocity before assigning it.
				linVel.X = linVel.X.Clamp(-MaxVelocity.X, MaxVelocity.X);
				linVel.Y = linVel.Y.Clamp(-MaxVelocity.Y, MaxVelocity.Y);
				CollisionBody.LinearVelocity = VelcroPhysicsEngine.ToPhysicsUnits(linVel);
			}
		}

		private TVector2f _maxVelocity = new TVector2f(100.0f, 100.0f);
		/// <summary>
		/// Maximum Velocity of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public TVector2f MaxVelocity
		{
			get => _maxVelocity;
			set
			{
				_maxVelocity = value;
				// Now clamp the Velocity to the new MaxVelocity value.
				Velocity = Velocity;
			}
		}

		private TVector2f _acceleration = new TVector2f();
		/// <summary>
		/// </summary>
		public TVector2f Acceleration
		{
			get => _acceleration;
			set
			{
				var accel = value;
				// Clamp the value to +-MaxAcceleration before assigning it.
				accel.X = accel.X.Clamp(-MaxAcceleration.X, MaxAcceleration.X);
				accel.Y = accel.Y.Clamp(-MaxAcceleration.Y, MaxAcceleration.Y);
				_acceleration = accel;
			}
		}

		private TVector2f _maxAcceleration = new TVector2f(100.0f, 100.0f);
		/// <summary>
		/// Maximum Acceleration of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public TVector2f MaxAcceleration
		{
			get => _maxAcceleration;
			set
			{
				_maxAcceleration = value;
				// Now clamp the Acceleration to the new MaxAcceleration value.
				Acceleration = Acceleration;
			}
		}

		public float AngularVelocity
		{
			get => VelcroPhysicsEngine.ToGameUnits(CollisionBody.AngularVelocity);
			set
			{
				var angVel = value;
				// Clamp the value to +-MaxAngularVelocity before assigning it.
				angVel = angVel.Clamp(-MaxAngularVelocity, MaxAngularVelocity);
				CollisionBody.AngularVelocity = VelcroPhysicsEngine.ToPhysicsUnits(angVel);
			}
		}

		private float _maxAngularVelocity = 100.0f;
		/// <summary>
		/// Maximum angular Velocity of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public float MaxAngularVelocity
		{
			get => _maxAngularVelocity;
			set
			{
				_maxAngularVelocity = value;
				// Now clamp the Velocity to the new MaxAngularVelocity value.
				AngularVelocity = AngularVelocity;
			}
		}

		private float _angularAcceleration;
		/// <summary>
		/// </summary>
		public float AngularAcceleration
		{
			get => _angularAcceleration;
			set
			{
				var accel = value;
				// Clamp the value to +-MaxAngularAcceleration before assigning it.
				accel = accel.Clamp(-MaxAngularAcceleration, MaxAngularAcceleration);
				_angularAcceleration = accel;
			}
		}

		private float _maxAngularAcceleration = 100.0f;
		/// <summary>
		/// Maximum angular Acceleration of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public float MaxAngularAcceleration
		{
			get => _maxAngularAcceleration;
			set
			{
				_maxAngularAcceleration = value;
				// Now clamp the Acceleration to the new MaxAngularAcceleration value.
				AngularAcceleration = AngularAcceleration;
			}
		}


		/// <summary>
		/// </summary>
		public float Friction
		{
			get => VelcroPhysicsEngine.ToGameUnits(CollisionBody.LinearDamping);
			set => CollisionBody.LinearDamping = VelcroPhysicsEngine.ToPhysicsUnits(value);
		}

		/// <summary>
		/// </summary>
		public float AngularFriction
		{
			get => VelcroPhysicsEngine.ToGameUnits(CollisionBody.AngularDamping);
			set => CollisionBody.AngularDamping = VelcroPhysicsEngine.ToPhysicsUnits(value);
		}

		/// <summary>
		/// </summary>
		public float Mass
		{
			get => CollisionBody.Mass;
			set => CollisionBody.Mass = value;
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
				CollisionBody.Rotation = VelcroPhysicsEngine.ToPhysicsUnits(EngineMath.DegreesToRadians(value)); //TODO: Verify
			}
		}

		protected PhysicsComponent()
		{
			Visible = false;
		}

		public virtual void OnCollide(Fixture self, Fixture other, Contact contactInfo)
		{
			ParentActor.OnCollide(self, other, contactInfo);
		}

		public virtual void OnCollideEnd(Fixture self, Fixture other, Contact contactInfo)
		{
			ParentActor.OnCollideEnd(self, other, contactInfo);
		}

		public virtual void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{
			ParentActor.OnOverlapBegin(self, other, contactInfo);
		}

		public virtual void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{
			ParentActor.OnOverlapEnd(self, other, contactInfo);
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

		public override void Tick(float deltaTime)
		{
			if (Math.Abs(Acceleration.X) > 0.00001f || Math.Abs(Acceleration.Y) > 0.00001f)
			{
				//PhysComp.CollisionBody.ApplyForce(Acceleration);
				Velocity += Acceleration * deltaTime;
			}

			if (Math.Abs(AngularAcceleration) > 0.00001f)
			{
				//PhysComp.CollisionBody.ApplyTorque(AngularAcceleration);
				AngularVelocity += AngularAcceleration * deltaTime;
			}
		}
	}
}