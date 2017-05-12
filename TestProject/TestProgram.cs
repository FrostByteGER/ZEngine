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
			physics.Gravity = new Vector2f(9.81f, 0.0f);

			
			
			var level = new Level();

			var testActor = new Actor();
			BoxShape collisionShape = new BoxShape(50.0f);
			var component = physics.ConstructCollisionComponent(1.0f, new Vector2f(0.0f, 0.0f), 43.0f, collisionShape, CollisionTypes.Default);
			testActor.SetRootComponent(component);

			var bottomBorder = new Actor();
			BoxShape bottomColShape = new BoxShape(200.0f, 10.0f, 50.0f);
			var bottomComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, 210.0f), 0.0f, bottomColShape, CollisionTypes.Default);
			bottomBorder.SetRootComponent(bottomComponent);

			var topBorder = new Actor();
			BoxShape topColShape = new BoxShape(200.0f, 10.0f, 50.0f);
			var topComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, -210.0f), 0.0f, topColShape, CollisionTypes.Default);
			topBorder.SetRootComponent(topComponent);

			var leftBorder = new Actor();
			BoxShape leftColShape = new BoxShape(10.0f, 200.0f, 50.0f);
			var leftComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(-210.0f, 0.0f), 0.0f, leftColShape, CollisionTypes.Default);
			leftBorder.SetRootComponent(leftComponent);

			var rightBorder = new Actor();
			BoxShape rightColShape = new BoxShape(10.0f, 200.0f, 50.0f);
			var rightComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(210.0f, 0.0f), 0.0f, rightColShape, CollisionTypes.Default);
			rightBorder.SetRootComponent(rightComponent);

			var player = new TestPlayerController();
			engine.RegisterLevel(level);
			level.RegisterActor(testActor);
			level.RegisterActor(bottomBorder);
			level.RegisterActor(topBorder);
			level.RegisterActor(leftBorder);
			level.RegisterActor(rightBorder);
			level.RegisterPlayer(player);
			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
