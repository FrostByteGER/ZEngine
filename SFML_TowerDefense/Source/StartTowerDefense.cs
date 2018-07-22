using System;
using SFML_Engine.Engine.Core;
using SFML_Roguelike.Source.GUI;

namespace SFML_Roguelike.Source
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
