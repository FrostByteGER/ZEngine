using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	public sealed class StartBreakout
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

			BreakoutMenuLevel.MainGameFont = new Font(MountainDewMode ? "Assets/SFML_Breakout/comic.ttf" : "Assets/SFML_Breakout/arial.ttf");


			EngineRef.GameInfo = new BreakoutGameInfo();
			EngineRef.PersistentGameMode = new BreakoutPersistentGameMode();
			EngineRef.EngineWindowWidth = 800;
			EngineRef.EngineWindowHeight = 800;
			EngineRef.InitEngine();
			Physics = EngineRef.PhysicsEngine;

			Physics.AddGroup("Pads");
			Physics.AddGroup("Balls");
			Physics.AddGroup("Borders");
			Physics.AddGroup("Blocks");
			Physics.AddGroup("PowerUp");
			Physics.AddGroup("Bullets");

			Physics.AddCollidablePartner("Balls","Pads");
			Physics.AddCollidablePartner("Balls", "Borders");
			Physics.AddCollidablePartner("Balls", "Blocks");
			Physics.AddCollidablePartner("Blocks", "Balls");
			Physics.AddCollidablePartner("PowerUp", "Pads");
			Physics.AddCollidablePartner("PowerUp", "Borders");
			Physics.AddCollidablePartner("Pads", "Borders");

			Physics.AddOverlapPartners("Blocks", "Bullets");
			Physics.AddOverlapPartners("Bullets", "Borders");


			var menuLevel = new BreakoutMenuLevel();
			Engine.Instance.RegisterLevel(menuLevel);
			menuLevel.InitiateMenu();
			EngineRef.LoadLevel(menuLevel);


			var values = LoadBlockPositions();

			List<BreakoutGameLevel> levels = new List<BreakoutGameLevel>();


			for (int h = 0; h < values.Count; ++h)
			{
				var l = new BreakoutGameLevel();
				levels.Add(l);
				EngineRef.RegisterLevel(l);
			}

			((BreakoutPersistentGameMode) EngineRef.PersistentGameMode).MaxLevels = (uint) levels.Count;

			EngineRef.StartEngine();
			BreakoutMenuLevel.MainGameFont.Dispose(); // I swear, one day I'll write a Manager that destroys every game object on engine shutdown...
			Console.ReadLine();
		}

		public static List<List<float[]>> LoadBlockPositions()
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