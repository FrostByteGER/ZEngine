using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace SFML_Engine.Engine.Physics
{
	public class VelcroPhysicsEngine
	{
		

		public World PhysicsWorld { get; }

		private TVector2f _gravity = new TVector2f(0.0f, 9.81f);
		public TVector2f Gravity
		{
			get => PhysicsWorld != null ? (TVector2f)PhysicsWorld.Gravity : _gravity;
			set
			{
				if (PhysicsWorld != null)
				{
					PhysicsWorld.Gravity = value;
				}
				else
				{
					_gravity = value;
				}
			}
		}

		public VelcroPhysicsEngine()
		{
			Gravity = new TVector2f(0.0f, 9.81f);
			PhysicsWorld = new World(Gravity);
		}

		public VelcroPhysicsEngine(float x, float y)
		{
			PhysicsWorld = new World(new TVector2f(x, y));
		}

		public VelcroPhysicsEngine(TVector2f gravity)
		{
			PhysicsWorld = new World(gravity);
		}

		public void PhysicsTick(float deltaTime)
		{
			PhysicsWorld.Step(deltaTime);
			foreach (var body in PhysicsWorld.BodyList)
			{
				var component = (PhysicsComponent) body.UserData;
				if (component == null) continue;
				component.SetLocalPosition(body.Position);
				component.SetLocalRotation(body.Rotation);
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
		/// <param name="rectHalfExtents">Size of the Collision Components Collision Body.</param>
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
			comp.CollisionBody = BodyFactory.CreateRectangle(PhysicsWorld, rectHalfExtents.X * 2.0f, rectHalfExtents.Y * 2.0f, mass, comp.WorldPosition, EngineMath.DegreesToRadians(angle), bodyType, comp);
			comp.ComponentBounds = rectHalfExtents;
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
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
		/// <param name="rectHalfExtents">Size of the Collision Components Collision Body.</param>
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
			comp.CollisionBody = BodyFactory.CreateRectangle(PhysicsWorld, rectHalfExtents.X * 2.0f, rectHalfExtents.Y * 2.0f, mass, new TVector2f(), EngineMath.DegreesToRadians(angle), bodyType, comp);
			comp.SetLocalPosition(position);
			comp.SetLocalRotation(angle);
			comp.SetLocalScale(scale);
			comp.CollisionType = collisionType;
			comp.CollisionResponseChannels = collisionResponseChannels;
			return comp;
		}

		internal void ShutdownPhysicsEngine()
		{
			
		}
	}
}