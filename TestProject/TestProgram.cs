using System;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;
using SFML_SpaceSEM;

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


			SpaceSEMMenuLevel level = new SpaceSEMMenuLevel();

			Console.WriteLine(Directory.GetCurrentDirectory());

			level.MainGameFont = new SFML.Graphics.Font("Assets/TestProject/arial.ttf");

			var physics = level.PhysicsEngine;
			physics.Gravity = new Vector2f(9.81f, 0.0f);

			// Creates a SpriteActor 
			var spriteActor = new SpriteActor(new Sprite(new Texture("Assets/TestProject/TestSprite.png")), level);
			spriteActor.ActorName = "Collision Sprite";
			//spriteActor.GetRootComponent<PhysicsComponent>().CollisionResponseChannels &= ~VelcroPhysics.Collision.Filtering.Category.Cat1;
			spriteActor.GetRootComponent<PhysicsComponent>().CollisionBody.GravityScale *= -1;

			// This is a normal actor that is equivalent to the spriteActor above but is manually composed.
			var spriteActor2 = new Actor(level);
			spriteActor2.ActorName = "No Collision Sprite";
			var spriteComponent = new SpriteComponent(new Sprite(new Texture("Assets/TestProject/TestSprite.png")));
			physics.ConstructRectangleCollisionComponent(spriteActor2, true, new TVector2f(0.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 1.0f, spriteComponent.ComponentBounds, BodyType.Dynamic);
			spriteActor2.AddComponent(spriteComponent);
			spriteActor2.GetRootComponent<PhysicsComponent>().Visible = false;
			//spriteActor2.GetRootComponent<PhysicsComponent>().CollisionResponseChannels &= ~VelcroPhysics.Collision.Filtering.Category.Cat1;
			spriteActor2.Origin = spriteComponent.Origin;

			var leftBorder = new Actor(level);
			leftBorder.ActorName = "Left Border";
			physics.ConstructRectangleCollisionComponent(leftBorder, true, new TVector2f(-450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f,400.0f), BodyType.Static);
			//leftBorder.GetRootComponent<PhysicsComponent>().CollisionType = VelcroPhysics.Collision.Filtering.Category.Cat1;
			leftBorder.Visible = true;

			var rightBorder = new Actor(level);
			rightBorder.ActorName = "Right Border";
			physics.ConstructRectangleCollisionComponent(rightBorder, true, new TVector2f(450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static);
			rightBorder.Visible = true;

			var topBorder = new Actor(level);
			topBorder.ActorName = "Top Border";
			physics.ConstructRectangleCollisionComponent(topBorder, true, new TVector2f(0.0f, -450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static);
			topBorder.Visible = true;

			var bottomBorder = new Actor(level);
			bottomBorder.ActorName = "Bottom Border";
			physics.ConstructRectangleCollisionComponent(bottomBorder, true, new TVector2f(0.0f, 450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static);
			bottomBorder.Visible = true;

			var player = new TestPlayerController();
			player.SetCameraSize(800);

			level.RegisterActor(spriteActor);
			level.RegisterActor(spriteActor2);
			level.RegisterActor(leftBorder);
			level.RegisterActor(rightBorder);
			level.RegisterActor(topBorder);
			level.RegisterActor(bottomBorder);
			level.RegisterPlayer(player);
			engine.LoadLevel(level);

			/*string levelFile = JsonConvert.SerializeObject(level, Formatting.Indented,
				new JsonSerializerSettings
				{
					PreserveReferencesHandling = PreserveReferencesHandling.All,
					ReferenceLoopHandling = ReferenceLoopHandling.Serialize
				});
			File.WriteAllText(@"level.json", levelFile);


			level = JsonConvert.DeserializeObject<Level>(levelFile, new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.All,
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize
			});*/

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
