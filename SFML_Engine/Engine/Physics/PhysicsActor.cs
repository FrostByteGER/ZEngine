using System;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Physics
{
	public class PhysicsActor : Actor
	{

		public PhysicsComponent PhysComp
		{
			get => RootComponent as PhysicsComponent;
			set => SetRootComponent(value); // TODO: Verify everything!
		}

		public bool Movable
		{
			get => PhysComp.CollisionBody.IsDynamic;
			set => PhysComp.CollisionBody.BodyType = value ? BodyType.Dynamic : BodyType.Static;
		}

		public TVector2f Velocity
		{
			get => VelcroPhysicsEngine.ToGameUnits( PhysComp.CollisionBody.LinearVelocity);
			set
			{
				var linVel = value;
				// Clamp the value to +-MaxVelocity before assigning it.
				linVel.X = linVel.X.Clamp(-MaxVelocity.X, MaxVelocity.X);
				linVel.Y = linVel.Y.Clamp(-MaxVelocity.Y, MaxVelocity.Y);
				PhysComp.CollisionBody.LinearVelocity = VelcroPhysicsEngine.ToPhysicsUnits(linVel);
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
			get => VelcroPhysicsEngine.ToGameUnits(PhysComp.CollisionBody.AngularVelocity);
			set
			{
				var angVel = value;
				// Clamp the value to +-MaxAngularVelocity before assigning it.
				angVel = angVel.Clamp(-MaxAngularVelocity, MaxAngularVelocity);
				PhysComp.CollisionBody.AngularVelocity = VelcroPhysicsEngine.ToPhysicsUnits(angVel);
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
			get => VelcroPhysicsEngine.ToGameUnits(PhysComp.CollisionBody.LinearDamping);
			set => PhysComp.CollisionBody.LinearDamping = VelcroPhysicsEngine.ToPhysicsUnits(value);
		}

		/// <summary>
		/// </summary>
		public float AngularFriction
		{
			get => VelcroPhysicsEngine.ToGameUnits(PhysComp.CollisionBody.AngularDamping);
			set => PhysComp.CollisionBody.AngularDamping = VelcroPhysicsEngine.ToPhysicsUnits(value);
		}

		/// <summary>
		/// </summary>
		public float Mass
		{
			get => PhysComp.CollisionBody.Mass;
			set => PhysComp.CollisionBody.Mass = value;
		}

		public PhysicsActor(PhysicsType type, BodyType bodyType, float mass, TVector2f physBounds, bool overlap, Level level) : base(level)
		{
			if (overlap)
			{
				switch (type)
				{
					case PhysicsType.Rectangle:
						level.PhysicsEngine.ConstructRectangleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
					case PhysicsType.Circle:
						level.PhysicsEngine.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds.X, bodyType);
						break;
					default:
						level.PhysicsEngine.ConstructRectangleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
				}
			}
			else
			{
				switch (type)
				{
					case PhysicsType.Rectangle:
						level.PhysicsEngine.ConstructRectangleCollisionComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
					case PhysicsType.Circle:
						level.PhysicsEngine.ConstructCircleCollisionComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds.X, bodyType);
						break;
					default:
						level.PhysicsEngine.ConstructRectangleCollisionComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
				}
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
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