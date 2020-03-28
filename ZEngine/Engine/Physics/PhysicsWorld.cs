using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;
using VelcroPhysics.Utils;
using ZEngine.Engine.Game;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Physics
{
	public class PhysicsWorld : IPhysicsWorld
    {
		public World World { get; }

		public bool CanTick { get; set; } = true;

		private Vector2 _gravity = ToPhysicsUnits(new Vector2(0.0f, 9.81f));

		public Vector2 Gravity
		{
			get => World != null ? ToGameUnits(World.Gravity) : ToGameUnits(_gravity);
			set
			{
				if (World != null)
				{
					// Only notify each body for a gravity change if the gravity has changed from its current value. If we're not doing this, bodies that are not awake will not respond to the new gravity values!
					if (World.Gravity != value)
					{
						foreach (var body in World.BodyList)
						{
							body.Awake = true;
						}
					}
					World.Gravity = ToPhysicsUnits(value);
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


		public PhysicsWorld() : this(new Vector2(0.0f, 9.81f))
		{
		}

		public PhysicsWorld(float x, float y) : this(new Vector2(x, y))
		{
		}

		public PhysicsWorld(Vector2 gravity)
		{
			GameToPhysicsUnitsRatio = GameToPhysicsUnitsRatio;
			Gravity = gravity;
			World = new World(Gravity);
		}

		public PhysicsWorld(Vector2 gravity, float gameToPhysicsUnitsRatio)
		{
			GameToPhysicsUnitsRatio = gameToPhysicsUnitsRatio;
			Gravity = gravity;
			World = new World(Gravity);
		}

		public void PhysicsTick(float deltaTime)
		{
			World.Step(deltaTime);
			foreach (var body in World.BodyList)
			{
				var component = body.UserData as PhysicsComponent;
				if (component == null) continue;
                //TODO: Support multi-physics-component actors
				//TODO: Setting the Local Position is very bad... it produces weird results when a PhysicsComponent is NOT the RootComponent of an actor!
				component.SetLocalPosition(ToGameUnits(body.Position));
				component.SetLocalRotation(EngineMath.RadiansToDegrees(ToGameUnits(body.Rotation)));
			}
		}

		public void UnregisterPhysicsComponent(PhysicsComponent comp)
		{
			World.RemoveBody(comp.CollisionBody);
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
		public CollisionComponent ConstructRectangleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, bool forceStayAwake = false)
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
			comp.CollisionBody = BodyFactory.CreateRectangle(World, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, ToPhysicsUnits(comp.WorldPosition), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
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
		public CollisionComponent ConstructRectangleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = false)
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
			comp.CollisionBody = BodyFactory.CreateRectangle(World, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, new Vector2(), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
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
		public OverlapComponent ConstructRectangleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, bool forceStayAwake = true)
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
			comp.CollisionBody = BodyFactory.CreateRectangle(World, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, ToPhysicsUnits(comp.WorldPosition), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
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
		public OverlapComponent ConstructRectangleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, Vector2 rectHalfExtents, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = true)
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
			comp.CollisionBody = BodyFactory.CreateRectangle(World, ToPhysicsUnits(rectHalfExtents.X * 2.0f), ToPhysicsUnits(rectHalfExtents.Y * 2.0f), mass, new Vector2(), ToPhysicsUnits(EngineMath.DegreesToRadians(angle)), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
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
		public CollisionComponent ConstructCircleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, bool forceStayAwake = false)
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
			comp.CollisionBody = BodyFactory.CreateCircle(World, ToPhysicsUnits(circleRadius), mass, new Vector2(), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
			comp.ComponentBounds = new Vector2(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new Vector2(circleRadius);
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
		public CollisionComponent ConstructCircleCollisionComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = false)
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
			comp.CollisionBody = BodyFactory.CreateCircle(World, ToPhysicsUnits(circleRadius), mass, new Vector2(), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
			comp.ComponentBounds = new Vector2(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new Vector2(circleRadius);
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
		/// <param name="forceStayAwake">Wether this OverlapComponent can sleep(receive no Collision Detection and Gravity influence) if no forces act upon it or not. Fakse will force the object to stay awake at all times no matter what.</param>
		/// <returns></returns>
		public OverlapComponent ConstructCircleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, bool forceStayAwake = true)
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
			comp.CollisionBody = BodyFactory.CreateCircle(World, ToPhysicsUnits(circleRadius), mass, new Vector2(), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
			comp.ComponentBounds = new Vector2(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new Vector2(circleRadius);
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
		/// <param name="forceStayAwake">Wether this OverlapComponent can sleep(receive no Collision Detection and Gravity influence) if no forces act upon it or not. Fakse will force the object to stay awake at all times no matter what.</param>
		/// <returns></returns>
		public OverlapComponent ConstructCircleOverlapComponent(Actor parent, bool asRootComponent, Vector2 position, float angle, Vector2 scale, float mass, float circleRadius, BodyType bodyType, Category collisionType, Category collisionResponseChannels, bool forceStayAwake = true)
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
			comp.CollisionBody = BodyFactory.CreateCircle(World, ToPhysicsUnits(circleRadius), mass, new Vector2(), bodyType, comp);
			comp.CollisionBody.SleepingAllowed = !forceStayAwake;
			comp.ComponentBounds = new Vector2(circleRadius * 2.0f);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.Origin = new Vector2(circleRadius);
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			return comp;
		}



		internal void ShutdownPhysicsEngine()
		{
			CanTick = false;
			World.Enabled = false;
			World.ClearForces();
			World.Clear();
		}

		public static Vector2 ToGameUnits(Vector2 physicsUnits)
		{
			physicsUnits.X = ConvertUnits.ToDisplayUnits(physicsUnits.X);
			physicsUnits.Y = ConvertUnits.ToDisplayUnits(physicsUnits.Y);
			return physicsUnits;
		}

		public static Vector2 ToGameUnits(float physicsUnitX, float physicsUnitY)
		{
			return new Vector2
			{
				X = ConvertUnits.ToDisplayUnits(physicsUnitX),
				Y = ConvertUnits.ToDisplayUnits(physicsUnitY)
			};
		}

		public static float ToGameUnits(float physicsUnit)
		{
			return ConvertUnits.ToDisplayUnits(physicsUnit);
		}

		public static Vector2 ToPhysicsUnits(Vector2 gameUnits)
		{
			gameUnits.X = ConvertUnits.ToSimUnits(gameUnits.X);
			gameUnits.Y = ConvertUnits.ToSimUnits(gameUnits.Y);
			return gameUnits;
		}

		public static Vector2 ToPhysicsUnits(float gameUnitX, float gameUnitY)
		{
			return new Vector2
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