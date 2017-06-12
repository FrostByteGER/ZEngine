using System;
using System.Collections.Generic;
using SFML_Engine.Engine.Core;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.Game;
using SFML_SpaceSEM.Game.Actors;
using SFML_SpaceSEM.Game.Actors.Enemies;
using SFML_SpaceSEM.IO;

namespace SFML_SpaceSEM
{
	class StartSpaceSEM
	{
		public static bool MountainDewMode { get; set; } = true;
		public static Engine EngineRef = Engine.Instance;
		public static void Main(string[] args)
		{
			if (args.Length >= 1)
			{
				MountainDewMode = bool.Parse(args[0]);
			}

			for (int i = 0; i < 4; ++i)
			{
				var levelData = GenerateLevel(5 + i * 3, 5 + i * 2);
				JSONManager.SaveObject(@"../../../Assets/Levels/level_" + (i + 1) + ".json", levelData);
				JSONManager.SaveObject(@"Assets/SFML_SpaceSEM/Levels/level_" + (i + 1) + ".json", levelData);
			}


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

		public static SpaceLevelDataWrapper GenerateLevel(int maxwaves, int shipCountPerWave)
		{
			var wrapperData = new SpaceLevelDataWrapper();
			var time = 1;
			for (var i = 0; i < maxwaves; ++i)
			{  
				var shipType = EngineMath.EngineRandom.Next(0, 4);
				var shipCount = EngineMath.EngineRandom.Next(shipCountPerWave - 2, shipCountPerWave);

				var spawnerData = new SpaceLevelSpawnerDataWrapper();
				spawnerData.Ships = new List<SpaceLevelShipDataWrapper>();
				spawnerData.ActivationTime = 1 + time;
				time += 8;

				SpaceLevelShipDataWrapper shipData = new SpaceLevelShipDataWrapper();


				for (int j = 0; j < shipCount; ++j)
				{

					var position = EngineMath.EngineRandom.Next(-350, 350);
					if (shipType == 0)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyFighter),
							Healthpoints = 4,
							Score = 10,
							Position = new TVector2f(position, -400),
							BulletDamage = 1,
							BulletSpeed = 150,
							BulletsPerShot = 1,
							CooldownTime = 3.5f,
							BulletSpread = 0.0f,
							Velocity = new TVector2f(0.0f, 80)
						};
					}
					else if (shipType == 1)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyCorvette),
							Healthpoints = 7,
							Score = 30,
							Position = new TVector2f(position, -400),
							BulletDamage = 2,
							BulletSpeed = 120,
							BulletsPerShot = 3,
							CooldownTime = 4.25f,
							BulletSpread = 15.0f,
							Velocity = new TVector2f(0.0f, 75)
						};
					}
					else if (shipType == 2)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyFrigate),
							Healthpoints = 11,
							Score = 70,
							Position = new TVector2f(position, -400),
							BulletDamage = 3,
							BulletSpeed = 100,
							BulletsPerShot = 4,
							CooldownTime = 6.75f,
							BulletSpread = 20.0f,
							Velocity = new TVector2f(0.0f, 60)
						};
					}
					else if (shipType == 3)
					{
						shipData = new SpaceLevelShipDataWrapper
						{
							ShipType = typeof(SpaceShipEnemyDestroyer),
							Healthpoints = 15,
							Score = 120,
							Position = new TVector2f(position, -400),
							BulletDamage = 5,
							BulletSpeed = 65,
							BulletsPerShot = 2,
							CooldownTime = 8.80f,
							BulletSpread = 10.0f,
							Velocity = new TVector2f(0.0f, 40)
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
