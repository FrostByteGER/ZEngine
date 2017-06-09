using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;
using VelcroPhysics.Utils;

namespace SFML_Engine.Engine.Physics
{
	public class VelcroPhysicsEngine
	{
		

		public World PhysicsWorld { get; }

		public bool CanTick { get; set; } = true;

		private TVector2f _gravity = ToPhysicsUnits(new TVector2f(0.0f, 9.81f));

		public TVector2f Gravity
		{
			get => PhysicsWorld != null ? ToGameUnits(PhysicsWorld.Gravity) : ToGameUnits(_gravity);
			set
			{
				if (PhysicsWorld != null)
				{
					// Only notify each body for a gravity change if the gravity has changed from its current value. If we're not doing this, bodies that are not awake will not respond to the new gravity values!
					if (PhysicsWorld.Gravity != value)
					{
						foreach (var body in PhysicsWorld.BodyList)
						{
							body.Awake = true;
						}
					}
					PhysicsWorld.Gravity = ToPhysicsUnits(value);
				}
				else
				{
					_gravity = ToPhysicsUnits(value);
				}
			}
		}

		private float _gameToPhysicsUnitsRatio = 100.0f;
		public float GameToPhysicsUnitsRatio
		{
			get => _gameToPhysicsUnitsRatio;
			set
			{
				_gameToPhysicsUnitsRatio = value;
				ConvertUnits.SetDisplayUnitToSimUnitRatio(value);
			}
		}


		public VelcroPhysicsEngine() : this(new TVector2f(0.0f, 9.81f))
		{
		}

		public VelcroPhysicsEngine(float x, float y) : this(new TVector2f(x, y))
		{
		}

		public VelcroPhysicsEngine(TVector2f gravity)
		{
			GameToPhysicsUnitsRatio = GameToPhysicsUnitsRatio;
			Gravity = gravity;
			PhysicsWorld = new World(Gravity);
		}

		public VelcroPhysicsEngine(TVector2f gravity, float gameToPhysicsUnitsRatio)
		{
			GameToPhysicsUnitsRatio = gameToPhysicsUnitsRatio;
			Gravity = gravity;
			PhysicsWorld = new World(Gravity);
		}

		public void PhysicsTick(float deltaTime)
		{
			PhysicsWorld.Step(deltaTime);
			foreach (var body in PhysicsWorld.BodyList)
			{
				var component = body.UserData as PhysicsComponent;
				if (component == null) continue;
				component.SetLocalPosition(ToGameUnits(body.Position));
				component.SetLocalRotation(EngineMath.RadiansToDegrees(ToGameUnits(body.Rotation)));
				var actor = component.ParentActor;
				if (actor == null) continue;

			}
		}

		public void UnregisterPhysicsComponent(PhysicsComponent comp)
		{
			PhysicsWorld.RemoveBody(comp.CollisionBody);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="rectHalfExtents">Size of the Collision Components Collision Rectangle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <returns></returns>
		public CollisionComponent ConstructRectangleCollisionComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, TVector2f rectHalfExtents, BodyType bodyType)
		{
			var comp = new CollisionComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateRectangle(PhysicsWorld, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, ToPhysicsUnits(comp.WorldPosition), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.ComponentBounds = rectHalfExtents;
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = rectHalfExtents;
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="rectHalfExtents">Size of the Collision Components Collision Rectangle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <param name="collisionType">Collision Type of the Collision Component.</param>
		/// <param name="collisionResponseChannels">To which Collision Types this Collision Component reacts to.</param>
		/// <returns></returns>
		public CollisionComponent ConstructRectangleCollisionComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, TVector2f rectHalfExtents, BodyType bodyType, Category collisionType, Category collisionResponseChannels)
		{
			var comp = new CollisionComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateRectangle(PhysicsWorld, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, new TVector2f(), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.ComponentBounds = rectHalfExtents;
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = rectHalfExtents;
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="rectHalfExtents">Size of the Collision Components Collision Rectangle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <returns></returns>
		public OverlapComponent ConstructRectangleOverlapComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, TVector2f rectHalfExtents, BodyType bodyType)
		{
			var comp = new OverlapComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateRectangle(PhysicsWorld, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, ToPhysicsUnits(comp.WorldPosition), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.ComponentBounds = rectHalfExtents;
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = rectHalfExtents;
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="rectHalfExtents">Size of the Collision Components Collision Rectangle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <param name="collisionType">Collision Type of the Collision Component.</param>
		/// <param name="collisionResponseChannels">To which Collision Types this Collision Component reacts to.</param>
		/// <returns></returns>
		public OverlapComponent ConstructRectangleOverlapComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, TVector2f rectHalfExtents, BodyType bodyType, Category collisionType, Category collisionResponseChannels)
		{
			var comp = new OverlapComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateRectangle(PhysicsWorld, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, new TVector2f(), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.ComponentBounds = rectHalfExtents;
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = rectHalfExtents;
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="circleRadius">Radius of the Collision Components Collision Circle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <returns></returns>
		public CollisionComponent ConstructCircleCollisionComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, float circleRadius, BodyType bodyType)
		{
			var comp = new CollisionComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateCircle(PhysicsWorld, ToPhysicsUnits(circleRadius), mass, new TVector2f(), bodyType, comp);
			comp.ComponentBounds = new TVector2f(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new TVector2f(circleRadius);
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="circleRadius">Radius of the Collision Components Collision Circle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <param name="collisionType">Collision Type of the Collision Component.</param>
		/// <param name="collisionResponseChannels">To which Collision Types this Collision Component reacts to.</param>
		/// <returns></returns>
		public CollisionComponent ConstructCircleCollisionComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, float circleRadius, BodyType bodyType, Category collisionType, Category collisionResponseChannels)
		{
			var comp = new CollisionComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateCircle(PhysicsWorld, ToPhysicsUnits(circleRadius), mass, new TVector2f(), bodyType, comp);
			comp.ComponentBounds = new TVector2f(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new TVector2f(circleRadius);
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="circleRadius">Radius of the Collision Components Collision Circle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <returns></returns>
		public OverlapComponent ConstructCircleOverlapComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, float circleRadius, BodyType bodyType)
		{
			var comp = new OverlapComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateCircle(PhysicsWorld, ToPhysicsUnits(circleRadius), mass, new TVector2f(), bodyType, comp);
			comp.ComponentBounds = new TVector2f(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new TVector2f(circleRadius);
			return comp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parent">Parent Actor.</param>
		/// <param name="asRootComponent">Wether this CollisionComponent shall be the RootComponent of the given parent actor.</param>
		/// <param name="position">Position relative to Parent Actor.</param>
		/// <param name="angle">Rotation relative to Parent Actor.</param>
		/// <param name="scale">Scale relative to Parent Actor.</param>
		/// <param name="mass">Mass in kg. of the Collision Component Collision Body.</param>
		/// <param name="circleRadius">Radius of the Collision Components Collision Circle.</param>
		/// <param name="bodyType">Type of the Collision Component Collision Body.</param>
		/// <param name="collisionType">Collision Type of the Collision Component.</param>
		/// <param name="collisionResponseChannels">To which Collision Types this Collision Component reacts to.</param>
		/// <returns></returns>
		public OverlapComponent ConstructCircleOverlapComponent(Actor parent, bool asRootComponent, TVector2f position, float angle, TVector2f scale, float mass, float circleRadius, BodyType bodyType, Category collisionType, Category collisionResponseChannels)
		{
			var comp = new OverlapComponent();
			if (asRootComponent)
			{
				parent.SetRootComponent(comp);
			}
			else
			{
				parent.AddComponent(comp);
			}
			comp.CollisionBody = BodyFactory.CreateCircle(PhysicsWorld, ToPhysicsUnits(circleRadius), mass, new TVector2f(), bodyType, comp);
			comp.ComponentBounds = new TVector2f(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new TVector2f(circleRadius);
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			return comp;
		}



		internal void ShutdownPhysicsEngine()
		{
			
		}

		public static TVector2f ToGameUnits(TVector2f physicsUnits)
		{
			physicsUnits.X = ConvertUnits.ToDisplayUnits(physicsUnits.X);
			physicsUnits.Y = ConvertUnits.ToDisplayUnits(physicsUnits.Y);
			return physicsUnits;
		}

		public static TVector2f ToGameUnits(float physicsUnitX, float physicsUnitY)
		{
			return new TVector2f
			{
				X = ConvertUnits.ToDisplayUnits(physicsUnitX),
				Y = ConvertUnits.ToDisplayUnits(physicsUnitY)
			};
		}

		public static float ToGameUnits(float physicsUnit)
		{
			return ConvertUnits.ToDisplayUnits(physicsUnit);
		}

		public static TVector2f ToPhysicsUnits(TVector2f gameUnits)
		{
			gameUnits.X = ConvertUnits.ToSimUnits(gameUnits.X);
			gameUnits.Y = ConvertUnits.ToSimUnits(gameUnits.Y);
			return gameUnits;
		}

		public static TVector2f ToPhysicsUnits(float gameUnitX, float gameUnitY)
		{
			return new TVector2f
			{
				X = ConvertUnits.ToSimUnits(gameUnitX),
				Y = ConvertUnits.ToSimUnits(gameUnitY)
			};
		}

		public static float ToPhysicsUnits(float gameUnit)
		{
			return ConvertUnits.ToSimUnits(gameUnit);
		}
	}
}