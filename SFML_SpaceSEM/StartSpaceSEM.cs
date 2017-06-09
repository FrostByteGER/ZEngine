using System;
using System.Collections.Generic;
using System.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.Game;
using SFML_SpaceSEM.Game.Actors;
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
			var levelData = GenerateLevel();
			JSONManager.SaveObject("level_2.json", levelData);

			EngineRef.GameInfo = new SpaceSEMGameInfo();
			EngineRef.GameInstance = new SpaceSEMGameInstance();
			EngineRef.EngineWindowWidth = 800;
			EngineRef.EngineWindowHeight = 800;
			EngineRef.InitEngine();


			var menuLevel = new SpaceSEMMenuLevel();

			EngineRef.LoadLevel(menuLevel);

			EngineRef.StartEngine();

			Console.ReadLine();
		}

		public static SpaceLevelDataWrapper GenerateLevel()
		{
			var wrapperData = new SpaceLevelDataWrapper();
			for (var i = 0; i < 10; ++i)
			{  
				var shipType = EngineMath.EngineRandom.Next(0, 4);
				var shipCount = EngineMath.EngineRandom.Next(1, 10);

				var spawnerData = new SpaceLevelSpawnerDataWrapper();
				spawnerData.Ships = new List<SpaceLevelShipDataWrapper>();
				spawnerData.ActivationTime = 1 + i * 2;

				SpaceLevelShipDataWrapper shipData = new SpaceLevelShipDataWrapper();


				for (int j = 0; j < shipCount; ++j)
				{
					if (shipType == 0)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyFighter),
							Position = new TVector2f(400 + j * 100, 0 + j * -50),
							BulletDamage = 1,
							BulletSpeed = 150,
							BulletsPerShot = 1,
							CooldownTime = 1.5f,
							BulletSpread = 0.0f,
							Velocity = new TVector2f(-100, 5)
						};
					}
					else if (shipType == 1)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyFighter),
							Position = new TVector2f(400 + j * 100, 0 + j * -50),
							BulletDamage = 2,
							BulletSpeed = 120,
							BulletsPerShot = 3,
							CooldownTime = 2.55f,
							BulletSpread = 15.0f,
							Velocity = new TVector2f(-100, 5)
						};
					}
					else if (shipType == 2)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyFighter),
							Position = new TVector2f(400 + j * 100, 0 + j * -50),
							BulletDamage = 3,
							BulletSpeed = 100,
							BulletsPerShot = 4,
							CooldownTime = 4.75f,
							BulletSpread = 20.0f,
							Velocity = new TVector2f(-50, 5)
						};
					}
					else if (shipType == 3)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyFighter),
							Position = new TVector2f(400 + j * 100, 0 + j * -50),
							BulletDamage = 5,
							BulletSpeed = 65,
							BulletsPerShot = 2,
							CooldownTime = 5.80f,
							BulletSpread = 10.0f,
							Velocity = new TVector2f(-50, 5)
						};
					}
					spawnerData.Ships.Add(shipData);
				}

				wrapperData.Spawners.Add(spawnerData);
			}
			return wrapperData;
		}
	}
}
