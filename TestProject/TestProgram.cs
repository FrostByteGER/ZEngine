using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Physics;

namespace TestProject
{
	class TestProgram
	{
		static void Main(string[] args)
		{

			Engine engine = Engine.Instance;
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 600;
			engine.InitEngine();
			var physics = engine.BulletPhysicsEngine;

			var level = new Level();

			var testActor = new Actor();
			BoxShape collisionShape = new BoxShape(50.0f);
			var component = new CollisionComponent(BulletPhysicsEngine.ConstructRigidBody(null,1.0f, new Vector2f(0.0f,0.0f), 0.0f, collisionShape));
			testActor.SetRootComponent(component);

			var player = new PlayerController();
			engine.RegisterLevel(level);
			level.RegisterActor(testActor);
			level.RegisterPlayer(player);
			physics.AddRigidBody(component.CollisionBody);
			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
