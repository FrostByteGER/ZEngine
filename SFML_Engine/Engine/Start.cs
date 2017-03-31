using System;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public class Start
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine(800, 600, "Engine");
            var Level = new Level();
            var actor = new SpriteActor();
			var actor2 = new SpriteActor();
			var pc = new PlayerController(actor);
			var pc2 = new PlayerController(actor2);
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
