﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;

namespace SFML_Pong
{
    class StartPong
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Pong");
            var Level = new Level();
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            Texture t = new Texture("Assets/SFML_Pong/Goku.png");
			Texture t2 = new Texture("Assets/SFML_Pong/Goku_MLG.png");
			var actor = new SpriteActor(t);
			var actor2 = new SpriteActor(t2);
			//actor.Position = new Vector2f(50,0);
			actor2.Position = new Vector2f(650, 0);
			actor2.Scale = new Vector2f(-actor2.Scale.X, actor2.Scale.Y);
			var pc = new PongPlayerController(actor);
			var pc2 = new PongPlayerController(actor2);
            Level.RegisterActor(actor);
			Level.RegisterActor(actor2);
            engine.RegisterLevel(Level);
            engine.RegisterPlayer(pc);
			engine.RegisterPlayer(pc2);
            engine.StartEngine();
            Console.ReadLine();
        }
    }
}