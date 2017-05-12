using System;
using BulletSharp;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace TestProject
{
	class TestProgram
	{
		static void Main(string[] args)
		{

			Engine engine = Engine.Instance;
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 800;
			engine.InitEngine();
			var physics = engine.BulletPhysicsEngine;
			var level = new Level();

			var testActor = new Actor();
			BoxShape collisionShape = new BoxShape(50.0f);
			var component = physics.ConstructCollisionComponent(1.0f, new Vector2f(0.0f, 0.0f), 43.0f, collisionShape, CollisionTypes.Default);
			testActor.SetRootComponent(component);

			var groundActor = new Actor();
			BoxShape groundCollisionShape = new BoxShape(50.0f, 20.0f, 50.0f);
			var groundComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, 100.0f), 0.0f, groundCollisionShape, CollisionTypes.Default);
			Console.WriteLine(EngineMath.ShortToBinary(component.CollisionType) + " " +
			                  EngineMath.ShortToBinary(groundComponent.CollisionType));
			groundComponent.CollisionBody.Gravity = -groundComponent.CollisionBody.Gravity;
			groundActor.SetRootComponent(groundComponent);

			var player = new PlayerController();
			engine.RegisterLevel(level);
			level.RegisterActor(testActor);
			level.RegisterActor(groundActor);
			level.RegisterPlayer(player);
			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
