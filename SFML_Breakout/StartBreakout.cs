using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

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
			EngineRef.GameInfo = new BreakoutGameInfo();
			EngineRef.PeristentGameMode = new BreakoutPersistentGameMode();
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
			var dummyPawn = new SpriteActor();
			var menuController = new BreakoutMenuPlayerController(dummyPawn);
			menuLevel.RegisterPlayer(menuController);
			menuLevel.RegisterActor(dummyPawn);
			menuLevel.InitiateMenu();
			EngineRef.LoadLevel(menuLevel);


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

			List<BreakoutGameLevel> levels = new List<BreakoutGameLevel>();


			for (int h = 0; h < values.Count; ++h)
			{
				levels.Add(new BreakoutGameLevel());
			}

			((BreakoutPersistentGameMode) EngineRef.PeristentGameMode).MaxLevels = (uint) levels.Count;
				
			for (int i = 0; i < values.Count; ++i)
			{
				BreakoutGameLevel l = levels[i];
				l.GameMode = new BreakoutGameMode();
				EngineRef.RegisterLevel(l);
				List<Block> blocks = new List<Block>();
				float blockSizeX = values[i][0][0];
				float blockSizeY = values[i][0][1];
				for (int j = 0; j < values[i].Count; ++j)
				{
					var block = new Block();
					block.ActorName = "Block" + (i + j);
					block.CollisionShape = new BoxShape(blockSizeX, blockSizeY);
					block.Position = new Vector2f(values[i][j][0], values[i][j][1]);
					block.CollisionShape.ShowCollisionShape = true;
					block.CollisionShape.Position = block.Position;
					block.Hitpoints = (uint)EngineMath.EngineRandom.Next(1, 4);
					block.MaxHitpoints = block.Hitpoints;
					block.Score *= block.MaxHitpoints;
					blocks.Add(block);
					l.RegisterActor(block);
				}
				l.Blocks = blocks;
			}

			EngineRef.StartEngine();
			BreakoutMenuLevel.MainGameFont.Dispose(); // I swear, one day I'll write a Manager that destroys every game object on engine shutdown...
			Console.ReadLine();
		}
	}
}