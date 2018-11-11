using System;
using Exofinity.Source.Game.Core;
using Exofinity.Source.GUI;
using ZEngine.Engine.Core;

namespace Exofinity.Source
{
	internal sealed class StartRoguelike
	{
		public static void Main(string[] args)
		{
			var engine = Engine.Instance;
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 800;
            engine.Bootstrapper = new RBootstrap();
			engine.GameInfo = new RGameInfo();
			engine.InitEngine();

			engine.LoadLevel(new MenuLevel());

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
