using Newtonsoft.Json;
using VelcroPhysics.Dynamics;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Physics
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

        [JsonIgnore]
        // TODO: Move to a PhysicsActor, not an Actor.
        public bool CanOverlap
        {
            get
            {
                var canoverlap = false;
                foreach (var comp in Components)
                {
                    var physComp = comp as PhysicsComponent;
                    if (physComp == null) continue;
                    canoverlap = physComp.CanOverlap;
                    if (canoverlap) break;
                }
                return canoverlap;
            }
            set
            {
                foreach (var comp in Components)
                {
                    var physComp = comp as PhysicsComponent;
                    if (physComp != null) physComp.CanOverlap = value;
                }
            }
        }

        [JsonIgnore]
        // TODO: Useless?
        public bool CanOverlapAll
        {
            get
            {
                var canoverlap = false;
                foreach (var comp in Components)
                {
                    var physComp = comp as PhysicsComponent;
                    if (physComp == null) continue;
                    canoverlap = physComp.CanOverlap;
                    if (!canoverlap) break;
                }
                return canoverlap;
            }
            set => CanOverlap = value;
        }

        public PhysicsActor(PhysicsType type, BodyType bodyType, float mass, TVector2f physBounds, bool overlap, bool collisionCallbacksEnabled, bool visible)
		{
            //TODO: FIX!!! Either defer to the actual level doing the work? Actor factory?
			if (overlap)
			{
				switch (type)
				{
					case PhysicsType.Rectangle:
						//level.PhysicsWorld.ConstructRectangleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
					case PhysicsType.Circle:
						//level.PhysicsWorld.ConstructCircleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds.X, bodyType);
						break;
					default:
						//level.PhysicsWorld.ConstructRectangleOverlapComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
				}
			}
			else
			{
				switch (type)
				{
					case PhysicsType.Rectangle:
						//level.PhysicsWorld.ConstructRectangleCollisionComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
					case PhysicsType.Circle:
						//level.PhysicsWorld.ConstructCircleCollisionComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds.X, bodyType);
						break;
					default:
						//level.PhysicsWorld.ConstructRectangleCollisionComponent(this, true, new TVector2f(), 0.0f, new TVector2f(1.0f, 1.0f), mass, physBounds, bodyType);
						break;
				}
			}
			CollisionCallbacksEnabled = collisionCallbacksEnabled;
			PhysComp.Visible = visible;
		}
	}
}