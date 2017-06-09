using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
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
			get => PhysComp.Movable;
			set => PhysComp.Movable = value;
		}

		public TVector2f Velocity
		{
			get => PhysComp.Velocity;
			set => PhysComp.Velocity = value;
		}

		/// <summary>
		/// Maximum Velocity of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public TVector2f MaxVelocity
		{
			get => PhysComp.MaxVelocity;
			set => PhysComp.MaxVelocity = value;
		}

		/// <summary>
		/// </summary>
		public TVector2f Acceleration
		{
			get => PhysComp.Acceleration;
			set => PhysComp.Acceleration = value;
		}

		/// <summary>
		/// Maximum Acceleration of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public TVector2f MaxAcceleration
		{
			get => PhysComp.MaxAcceleration;
			set => PhysComp.MaxAcceleration = value;
		}

		public float AngularVelocity
		{
			get => PhysComp.AngularVelocity;
			set => PhysComp.AngularVelocity = value;
		}

		/// <summary>
		/// Maximum angular Velocity of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public float MaxAngularVelocity
		{
			get => PhysComp.MaxAngularVelocity;
			set => PhysComp.MaxAngularVelocity = value;
		}

		/// <summary>
		/// </summary>
		public float AngularAcceleration
		{
			get => PhysComp.AngularAcceleration;
			set => PhysComp.AngularAcceleration = value;
		}

		/// <summary>
		/// Maximum angular Acceleration of this Physics Actor. This limit is valid in both + direction as well as - direction.
		/// </summary>
		public float MaxAngularAcceleration
		{
			get => PhysComp.MaxAngularAcceleration;
			set => PhysComp.MaxAngularAcceleration = value;
		}


		/// <summary>
		/// </summary>
		public float Friction
		{
			get => PhysComp.Friction;
			set => PhysComp.Friction = value;
		}

		/// <summary>
		/// </summary>
		public float AngularFriction
		{
			get => PhysComp.AngularFriction;
			set => PhysComp.AngularFriction = value;
		}

		/// <summary>
		/// </summary>
		public float Mass
		{
			get => PhysComp.Mass;
			set => PhysComp.Mass = value;
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
	}
}