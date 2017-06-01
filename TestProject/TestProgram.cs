using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Dynamics;

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


			var level = new Level();
			var physics = level.PhysicsEngine;
			physics.Gravity = new Vector2f(9.81f, 0.0f);

			
			
			

			var testActor2 = new Actor();
			var spriteComponent = new SpriteComponent(new Sprite(new Texture("Assets/TestProject/TestSprite.png")));
			physics.ConstructRectangleCollisionComponent(testActor2, true, new TVector2f(0.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 1.0f, spriteComponent.ComponentBounds, BodyType.Dynamic);
			testActor2.AddComponent(spriteComponent);
			testActor2.GetRootComponent<PhysicsComponent>().Visible = false;
			testActor2.Origin = spriteComponent.Origin;

			var leftBorder = new Actor();
			physics.ConstructRectangleCollisionComponent(leftBorder, true, new TVector2f(-450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f), BodyType.Static);
			leftBorder.Visible = false;

			var rightBorder = new Actor();
			physics.ConstructRectangleCollisionComponent(rightBorder, true, new TVector2f(450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f), BodyType.Static);
			rightBorder.Visible = false;

			var topBorder = new Actor();
			physics.ConstructRectangleCollisionComponent(topBorder, true, new TVector2f(0.0f, -450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f), BodyType.Static);
			topBorder.Visible = false;

			var bottomBorder = new Actor();
			physics.ConstructRectangleCollisionComponent(bottomBorder, true, new TVector2f(0.0f, 450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f), BodyType.Static);
			bottomBorder.Visible = false;
			/*
var testActor = new Actor();
var collisionShape = new BoxShape(50.0f);
var component = physics.ConstructCollisionComponent(1.0f, new Vector2f(0.0f, 0.0f), 43.0f, collisionShape, (short)CollisionTypes.Default);
component.CollisionBody.AngularVelocity = EngineMath.DegreesToRadians(30);
component.CollisionCallbacksEnabled = false;
testActor.SetRootComponent(component);

var bottomBorder = new Actor();
var bottomColShape = new BoxShape(200.0f, 10.0f, 50.0f);
var bottomComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, 210.0f), 0.0f, bottomColShape, (short)CollisionTypes.Default);
bottomBorder.SetRootComponent(bottomComponent);

var topBorder = new Actor();
var topColShape = new BoxShape(200.0f, 10.0f, 50.0f);
var topComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, -210.0f), 0.0f, topColShape, (short)CollisionTypes.Default);
topBorder.SetRootComponent(topComponent);

var leftBorder = new Actor();
var leftColShape = new BoxShape(10.0f, 200.0f, 50.0f);
var leftComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(-210.0f, 0.0f), 0.0f, leftColShape, (short)CollisionTypes.Default);
leftBorder.SetRootComponent(leftComponent);

var rightBorder = new Actor();
var rightColShape = new BoxShape(10.0f, 200.0f, 50.0f);
var rightComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(210.0f, 0.0f), 0.0f, rightColShape, (short)CollisionTypes.Default);
rightBorder.SetRootComponent(rightComponent);*/

			var player = new TestPlayerController();
			player.SetCameraSize(800);
			level.RegisterActor(testActor2);
			//level.RegisterActor(testActor);
			level.RegisterActor(leftBorder);
			level.RegisterActor(rightBorder);
			level.RegisterActor(topBorder);
			level.RegisterActor(bottomBorder);
			level.RegisterPlayer(player);
			engine.LoadLevel(level);

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
