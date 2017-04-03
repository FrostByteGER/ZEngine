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
            var Level = new Level();
            var leftPadTexture = new Texture("Assets/SFML_Pong/Goku.png");
			var rightPadTexture = new Texture("Assets/SFML_Pong/Goku_MLG.png");
			var leftPad = new SpriteActor(leftPadTexture);
	        leftPad.Components.Add(new ActorComponent());
	        var rightPad = new SpriteActor(rightPadTexture) {Position = new Vector2f(650, 0)};
	        rightPad.Scale = new Vector2f(-rightPad.Scale.X, rightPad.Scale.Y);

	        var ball = new SpriteActor();
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
