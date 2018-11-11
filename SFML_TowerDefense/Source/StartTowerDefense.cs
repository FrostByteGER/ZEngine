using System;
using Exofinity.Source.GUI;
using ZEngine.Engine.Core;

namespace Exofinity.Source
{
	internal sealed class StartTowerDefense
	{
		public static void Main(string[] args)
		{
			var engine = Engine.Instance;
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 800;
			engine.GameInfo = new TDGameInfo();
			engine.InitEngine();

			engine.LoadLevel(new MenuLevel());

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
