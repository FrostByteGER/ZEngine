using System;
using SFML.Graphics;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;

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

			var testLevel = new Level();
			var animActor = new Actor(testLevel);
			animActor.SetRootComponent(new AnimationComponent(new Texture("Assets/Game/Animations/ExampleSheet.png"),100,100));


			testLevel.RegisterActor(animActor);

			var pc = new PlayerController();
			testLevel.RegisterPlayer(pc);

			engine.LoadLevel(testLevel);

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}
