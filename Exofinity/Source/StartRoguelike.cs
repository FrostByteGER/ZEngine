using System;
using Exofinity.Source.Game.Core;
using Exofinity.Source.Game.TileMap.ImportExport;
using Exofinity.Source.GUI;
using ZEngine.Engine.Core;
using ZEngine.Engine.IO;

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

		    var test = JSONManager.LoadObject<Map>(AssetManager.GameAssetsPath + "/" + "testmap.json");

			engine.LoadLevel(new MenuLevel());

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
