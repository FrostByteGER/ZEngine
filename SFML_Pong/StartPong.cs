using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using Actor = SFML_Engine.Engine.Actor;

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

			var Level = new Level();
            var leftPadTexture = new Texture("Assets/SFML_Pong/Goku.png");
			var rightPadTexture = new Texture("Assets/SFML_Pong/Goku_MLG.png");
	        var ballTexture = new Texture("Assets/SFML_Pong/DragonBall4Star.png");
			var leftPad = new SpriteActor(leftPadTexture);
	        leftPad.ActorName = "Left Pad";
	        leftPad.CollisionShape = new BoxShape(leftPadTexture.Size.X, leftPadTexture.Size.Y);
	        var rightPad = new SpriteActor(rightPadTexture) {Position = new Vector2f(650, 0)};
	        rightPad.ActorName = "Right Pad";
	        rightPad.Scale = new Vector2f(-rightPad.Scale.X, rightPad.Scale.Y);
	        rightPad.CollisionShape = new BoxShape(rightPadTexture.Size.X, rightPadTexture.Size.Y);

			var ball = new SpriteActor(ballTexture);
	        ball.ActorName = "Ball";
	        ball.CollisionShape = new SphereShape(ballTexture.Size.X);
	        ball.Position = new Vector2f(400,300);

	        physics.AddActorToGroup("Pads",leftPad);
	        physics.AddActorToGroup("Pads", rightPad);
	        physics.AddActorToGroup("Balls", ball);
	        physics.AddCollidablePartner("Pads", "Balls");


			var leftPadController = new PongPlayerController(leftPad);
			var rightPadController = new PongPlayerController(rightPad);
            Level.RegisterActor(leftPad);
			Level.RegisterActor(rightPad);
	        Level.RegisterActor(ball);
            engine.RegisterLevel(Level);
            engine.RegisterPlayer(leftPadController);
			engine.RegisterPlayer(rightPadController);
            engine.StartEngine();
            Console.ReadLine();
        }
    }
}
