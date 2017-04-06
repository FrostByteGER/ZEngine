using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
namespace SFML_Pong
{
    public sealed class StartPong
    {
	    public static void Main(string[] args)
        {
            var engine = new Engine(800, 600, "Pong");
	        var physics = engine.PhysicsEngine;
	        physics.AddGroup("Pads");
			physics.AddGroup("Balls");
	        physics.AddGroup("Borders");

			var topBorder = new SpriteActor();
	        var bottomBorder = new SpriteActor();
			var leftBorder = new SpriteActor();
			var rightBorder = new SpriteActor();

	        topBorder.ActorName = "Top Border";
			bottomBorder.ActorName = "Bottom Border";
			leftBorder.ActorName = "Left Border";
			rightBorder.ActorName = "Right Border";

			topBorder.Position = new Vector2f(0,-20);
			bottomBorder.Position = new Vector2f(0, 600);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(800, 0);

			topBorder.CollisionShape = new BoxShape(800,20);
			bottomBorder.CollisionShape = new BoxShape(800, 20);
			leftBorder.CollisionShape = new BoxShape(20, 600);
			rightBorder.CollisionShape = new BoxShape(20, 600);

			topBorder.CollisionShape.show = true;
			bottomBorder.CollisionShape.show = true;
			leftBorder.CollisionShape.show = true;
			rightBorder.CollisionShape.show = true;

			var level = new Level();
            //var leftPadTexture = new Texture("Assets/SFML_Pong/Goku.png");
			//var rightPadTexture = new Texture("Assets/SFML_Pong/Goku_MLG.png");
	        var ballTexture = new Texture("Assets/SFML_Pong/DragonBall4Star.png");
			var leftPad = new SpriteActor();
	        leftPad.ActorName = "Left Pad";
	        leftPad.Position = new Vector2f(30, 30);
	        leftPad.CollisionShape = new BoxShape(20, 175);
			leftPad.CollisionShape.show = true;

	        var rightPad = new SpriteActor() {Position = new Vector2f(750, 30)};
	        rightPad.ActorName = "Right Pad";
	        rightPad.Scale = new Vector2f(-rightPad.Scale.X, rightPad.Scale.Y);
	        rightPad.CollisionShape = new BoxShape(20, 175);
			rightPad.CollisionShape.show = true;

			var ball = new SpriteActor(ballTexture);
	        ball.ActorName = "Ball";
	        ball.CollisionShape = new SphereShape(ballTexture.Size.X);
	        ball.Position = new Vector2f(400,250);
			ball.Velocity = new Vector2f(100,0);
			ball.CollisionShape.show = true;

	        physics.AddActorToGroup("Pads",leftPad);
	        physics.AddActorToGroup("Pads", rightPad);
	        physics.AddActorToGroup("Balls", ball);

			physics.AddActorToGroup("Borders", topBorder);
			physics.AddActorToGroup("Borders", bottomBorder);
			physics.AddActorToGroup("Borders", leftBorder);
			physics.AddActorToGroup("Borders", rightBorder);


			physics.AddCollidablePartner("Pads", "Balls");
	        physics.AddCollidablePartner("Balls", "Borders");
			physics.AddCollidablePartner("Pads", "Borders");


			var leftPadController = new PongPlayerController(leftPad);
			var rightPadController = new PongPlayerController(rightPad);
            level.RegisterActor(leftPad);
			level.RegisterActor(rightPad);
	        level.RegisterActor(ball);
			level.RegisterActor(topBorder);
			level.RegisterActor(bottomBorder);
			level.RegisterActor(leftBorder);
			level.RegisterActor(rightBorder);
			engine.RegisterLevel(level);
            engine.RegisterPlayer(leftPadController);
			engine.RegisterPlayer(rightPadController);
            engine.StartEngine();
            Console.ReadLine();
        }
    }
}
