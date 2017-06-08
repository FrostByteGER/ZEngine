using System;
using System.Collections.Generic;
using System.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Graphics;
using SFML_SpaceSEM.Game;
using SFML_SpaceSEM.IO;
using VelcroPhysics.Dynamics.Solver;

namespace SFML_SpaceSEM
{
	class StartSpaceSEM
	{
		public static bool MountainDewMode { get; set; } = true;
		public static Engine EngineRef = Engine.Instance;
		public static PhysicsEngine Physics;
		public static void Main(string[] args)
		{
			if (args.Length >= 1)
			{
				MountainDewMode = bool.Parse(args[0]);
			}
			//SpaceSEMMenuLevel.SaveSpaceLevel(wrapperData);

			//SpaceSEMMenuLevel.LoadSpaceLevel("level_1.json");

			EngineRef.GameInfo = new SpaceSEMGameInfo();
			EngineRef.GameInstance = new SpaceSEMGameInstance();
			EngineRef.EngineWindowWidth = 800;
			EngineRef.EngineWindowHeight = 800;
			EngineRef.InitEngine();


			var menuLevel = new SpaceSEMMenuLevel();

			//SpaceSEMMenuLevel.MainGameFont = new SFML.Graphics.Font(MountainDewMode ? "Assets/SFML_SpaceSEM/comic.ttf" : "Assets/SFML_SpaceSEM/arial.ttf");
			//menuLevel.MainGameFont = new SFML.Graphics.Font("Assets/SFML_SpaceSEM/arial.ttf"); Old

			EngineRef.LoadLevel(menuLevel);

			//var gameLevel = new SpaceSEMGameLevel();

			EngineRef.StartEngine();

			menuLevel.MainGameFont.Dispose(); // I swear, one day I'll write a Manager that destroys every game object on engine shutdown...
			Console.ReadLine();
		}

		public static SpaceLevelDataWrapper GenerateLevel()
		{
			var wrapperData = new SpaceLevelDataWrapper();
			for (var i = 0; i < 3; ++i)
			{
				wrapperData.Spawners.Add(new SpaceLevelSpawnerDataWrapper
				{
					ActivationTime = 1 + i,
					Ships = new List<SpaceLevelShipDataWrapper>()
					{
						new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpriteActor),
							Position = new SFML_Engine.Engine.Utility.TVector2f(i * 10)
						}
					}


				});
			}
			return wrapperData;
		}
	}
}
