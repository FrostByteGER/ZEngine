using System;
using System.Collections.Generic;
using System.IO;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.JUI;
using SFML.System;
using SFML.Graphics;

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

			SpaceSEMMenuLevel.MainGameFont = new SFML.Graphics.Font(MountainDewMode ? "Assets/SFML_SpaceSEM/comic.ttf" : "Assets/SFML_SpaceSEM/arial.ttf");


			EngineRef.GameInfo = new SpaceSEMGameInfo();
			EngineRef.PersistentGameMode = new SpaceSEMPersistentGameMode();
			EngineRef.EngineWindowWidth = 800;
			EngineRef.EngineWindowHeight = 800;
			EngineRef.InitEngine();
			Physics = EngineRef.PhysicsEngine;

			Physics.AddGroup("Players");
			Physics.AddGroup("Enemies");
			Physics.AddGroup("Borders");
			Physics.AddGroup("Obstacles");
			Physics.AddGroup("PowerUp");
			Physics.AddGroup("Bullets");

			// Player/Enemy Collision
			Physics.AddCollidablePartner("Players", "Enemies");
			Physics.AddCollidablePartner("Enemies", "Players");

			// Bullet Overlaps
			Physics.AddOverlapPartners("Players", "Bullets");
			Physics.AddOverlapPartners("Bullets", "Players");
			Physics.AddOverlapPartners("Enemies", "Bullets");
			Physics.AddOverlapPartners("Bullets", "Enemies");

			// Border Collision
			Physics.AddCollidablePartner("Players", "Borders");
			Physics.AddCollidablePartner("Enemies", "Borders");
			Physics.AddCollidablePartner("Bullets", "Borders");
			Physics.AddCollidablePartner("PowerUp", "Borders");
			Physics.AddCollidablePartner("Obstacles", "Borders");

			// Obstacle Collision
			Physics.AddCollidablePartner("Players", "Obstacles");
			Physics.AddCollidablePartner("Obstacles", "Players");

			// Powerup Overlaps
			Physics.AddOverlapPartners("PowerUp", "Players");


			//JGUI vvv



			//JGUI ^^^


			var menuLevel = new SpaceSEMMenuLevel();
			Engine.Instance.RegisterLevel(menuLevel);
			menuLevel.InitiateMenu();
			EngineRef.LoadLevel(menuLevel);

			var gameLevel = new SpaceSEMGameLevel();
			Engine.Instance.RegisterLevel(gameLevel);

			EngineRef.StartEngine();
			
			SpaceSEMMenuLevel.MainGameFont.Dispose(); // I swear, one day I'll write a Manager that destroys every game object on engine shutdown...
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
