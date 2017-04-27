using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;

namespace SFML_Pong
{
    public sealed class StartPong
    {
	    public static void Main(string[] args)
	    {

			Engine engine = Engine.Instance;
		    engine.GameInfo = new PongGameInfo();
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 600;
			engine.InitEngine();
			var physics = engine.PhysicsEngine;
	        physics.AddGroup("Pads");
			physics.AddGroup("Balls");
	        physics.AddGroup("Borders");
			physics.AddGroup("SoftBorders");

			var topBorder = new SpriteActor();
	        var bottomBorder = new SpriteActor();
			var leftBorder = new SpriteActor();
			var rightBorder = new SpriteActor();

			topBorder.Movable = false;
	        bottomBorder.Movable = false;
	        leftBorder.Movable = false;
	        rightBorder.Movable = false;

			topBorder.ActorName = "Top Border";
			bottomBorder.ActorName = "Bottom Border";
			leftBorder.ActorName = "Left Border";
			rightBorder.ActorName = "Right Border";

			topBorder.Position = new Vector2f(0,-400);
			bottomBorder.Position = new Vector2f(0, 600);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(800, 0);

			topBorder.CollisionShape = new BoxShape(800,400);
			bottomBorder.CollisionShape = new BoxShape(800, 400);
			leftBorder.CollisionShape = new BoxShape(20, 600);
			rightBorder.CollisionShape = new BoxShape(20, 600);

			topBorder.CollisionShape.ShowCollisionShape = true;
			bottomBorder.CollisionShape.ShowCollisionShape = true;
			leftBorder.CollisionShape.ShowCollisionShape = true;
			rightBorder.CollisionShape.ShowCollisionShape = true;

		    var menuLevel = new PongMenuLevel();

			var gameLevel = new PongGameLevel();
            var leftPadTexture = new Texture("Assets/SFML_Pong/Goku.png");
			var rightPadTexture = new Texture("Assets/SFML_Pong/Goku_MLG.png");
	        var ballTexture = new Texture("Assets/SFML_Pong/DragonBall4Star.png");

			var leftPad = new PongPlayerPad();
	        leftPad.ActorName = "Left Pad";
	        leftPad.MaxVelocity = 700.0f;
	        leftPad.Position = new Vector2f(30, 30);
	        leftPad.CollisionShape = new BoxShape(leftPadTexture.Size.X, leftPadTexture.Size.Y);
	        leftPad.CollisionShape.Origin = leftPad.Origin;
			leftPad.CollisionShape.ShowCollisionShape = true;
			leftPad.Friction = 0.01f;

		    var rightPad = new PongPlayerPad();
		    rightPad.Position = new Vector2f(650, 30);
	        rightPad.ActorName = "Right Pad";
			rightPad.MaxVelocity = 700.0f;
	        rightPad.CollisionShape = new BoxShape(rightPadTexture.Size.X, rightPadTexture.Size.Y);
	        rightPad.CollisionShape.Origin = rightPad.Origin;
			rightPad.CollisionShape.ShowCollisionShape = true;
			rightPad.Friction = 0.01f;

			var ball = new PongBall(ballTexture);
			ball.ActorName = "Ball";
	        ball.CollisionShape = new SphereShape(ballTexture.Size.X);
			ball.Color = new Color(ball.Color.R, ball.Color.G, ball.Color.B, 60);
	        ball.CollisionShape.Origin = ball.Origin;
			ball.Position = new Vector2f(400,250);
	        ball.MaxVelocity = 500.0f;
			ball.Velocity = new Vector2f(250,0);
			ball.CollisionShape.ShowCollisionShape = true;

	        physics.AddActorToGroup("Pads",leftPad);
	        physics.AddActorToGroup("Pads", rightPad);
	        physics.AddActorToGroup("Balls", ball);
			physics.AddGroup("PowerUP");

			physics.AddActorToGroup("Borders", topBorder);
			physics.AddActorToGroup("Borders", bottomBorder);
			physics.AddActorToGroup("SoftBorders", leftBorder);
			physics.AddActorToGroup("SoftBorders", rightBorder);


			physics.AddCollidablePartner("Balls", "Pads");
	        physics.AddCollidablePartner("Balls", "Borders");
			physics.AddCollidablePartner("Pads", "Borders");
	        physics.AddOverlapPartners("Balls", "SoftBorders");
			physics.AddOverlapPartners("Balls", "PowerUP");

			var leftPadController = new PongPlayerController(leftPad);
			var rightPadController = new AIPlayerController(rightPad);
	        leftPadController.Name = "Player 1";
	        rightPadController.Name = "Player 2";

		    var dummyPawn = new SpriteActor();
			var menuController = new PongMenuController(dummyPawn);
		    menuController.Name = "Player 1";

			engine.LoadLevel(menuLevel);
		    engine.RegisterLevel(gameLevel);
			menuLevel.RegisterActor(dummyPawn);
		    menuLevel.InitLevel();

			gameLevel.RegisterActor(leftPad);
			gameLevel.RegisterActor(rightPad);
	        gameLevel.RegisterActor(ball);
			gameLevel.RegisterActor(topBorder);
			gameLevel.RegisterActor(bottomBorder);
			gameLevel.RegisterActor(leftBorder);
			gameLevel.RegisterActor(rightBorder);
	        gameLevel.GameMode = new PongGameMode();
			engine.RegisterPlayer(menuController);
			leftPadController.IsActive = false;
			rightPadController.IsActive = false;
			engine.RegisterPlayer(leftPadController);
			engine.RegisterPlayer(rightPadController);

			engine.StartEngine();

			// Super important! Delete all textures
	        ballTexture.Dispose();
		    leftPadTexture.Dispose();
		    rightPadTexture.Dispose();
		    PongMenuLevel.MainGameFont.Dispose();
            Console.ReadLine();
        }
    }
}
