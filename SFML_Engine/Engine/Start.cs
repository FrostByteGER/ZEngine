using System;
using SFML_Engine.Engine.Game;

namespace SFML_Engine.Engine
{
    internal sealed class Start
    {
        public static void Main(string[] args)
        {
	        Engine engine = Engine.Instance;
	        engine.EngineWindowWidth = 800;
	        engine.EngineWindowHeight = 600;
			engine.InitEngine();
            var Level = new Level();
            var actor = new SpriteActor();
			var actor2 = new SpriteActor();
			var pc = new PlayerController(actor);
			var pc2 = new PlayerController(actor2);
			Level.RegisterActor(actor);
			Level.RegisterActor(actor2);
			engine.RegisterLevel(Level);
	        Level.RegisterPlayer(pc);
	        Level.RegisterPlayer(pc2);
			engine.StartEngine();
            Console.ReadLine();
        }
    }
}
