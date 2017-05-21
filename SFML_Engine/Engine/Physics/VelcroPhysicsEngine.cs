using System;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Physics
{
	public class VelcroPhysicsEngine
	{
		

		public World PhysicsWorld { get; }
		public TVector2f Gravity
		{
			get => PhysicsWorld.Gravity;
			set => PhysicsWorld.Gravity = value;
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
				var actor = component.ParentActor;
				if (actor == null) continue;

			}
		}
	}
}