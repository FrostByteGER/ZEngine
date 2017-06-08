using System;
using System.Collections.Generic;
using System.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Core;
using SFML_SpaceSEM.Game;

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

		public static List<List<float[]>> LoadLevels()
		{
			List<List<float[]>> values = new List<List<float[]>>();
			try
			{
				using (StreamReader sr = new StreamReader("Assets/SFML_Breakout/Levels.cfg"))
				{
					string line;
					List<float[]> levelValues = new List<float[]>();
					float[] pair = new float[2];
					while ((line = sr.ReadLine()) != null)
					{
						if (line == "---")
						{
							values.Add(levelValues);
							levelValues = new List<float[]>();
						}
						else
						{
							line = line.Remove(line.Length - 1);
							var valuesraw = line.Split(',');
							pair[0] = Convert.ToSingle(valuesraw[0]);
							pair[1] = Convert.ToSingle(valuesraw[1]);
							levelValues.Add(pair);
							pair = new float[2];
						}
					}
					values.Add(levelValues);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Console.WriteLine(e.Message);
			}
			return values;
		}
	}
}
