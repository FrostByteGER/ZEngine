using System;
using SFML.Graphics;
using SFML_Engine.Engine.Core;

namespace SFML_TowerDefense.Source
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

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
