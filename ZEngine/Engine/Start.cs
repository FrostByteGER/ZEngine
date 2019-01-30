﻿using System;
using ZEngine.Engine.Game;
using ZEngine.Engine.Services;

namespace ZEngine.Engine
{
    internal sealed class Start
    {
        public static void Main(string[] args)
        {
	        Core.Engine engine = Core.Engine.Instance;
	        engine.EngineWindowWidth = 800;
	        engine.EngineWindowHeight = 600;
            engine.Bootstrapper = new Bootstrap();
            engine.GameInfo = new GameInfo();
            engine.InitEngine();
            var level = new Level();
            var actor = new Actor();
			var actor2 = new Actor();
			var pc = new PlayerController();
			var pc2 = new PlayerController();
			level.RegisterActor(actor);
			level.RegisterActor(actor2);
			engine.LoadLevel(level);
	        level.RegisterPlayer(pc);
	        level.RegisterPlayer(pc2);
			engine.StartEngine();
            Console.ReadLine();
        }
    }
}
