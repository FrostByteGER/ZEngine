using System;
using SFML_Engine.Engine.Core;
using SFML_TowerDefense.Source.Game;
using SFML_TowerDefense.Source.GUI;

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

			//engine.LoadLevel(new GUILevelTest());
			engine.LoadLevel(new TDMapTestLevel());

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
