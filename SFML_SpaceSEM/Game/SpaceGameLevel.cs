using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.Game.Actors;
using SFML_SpaceSEM.Game.Players;
using SFML_SpaceSEM.IO;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Collision.Narrowphase;
using VelcroPhysics.Dynamics;

namespace SFML_SpaceSEM.Game
{
	public class SpaceGameLevel : SpaceLevel
	{

		internal float LevelTime { get; set; } = 0.0f;
		public SpriteActor Player { get; set; } = null;

		public List<SpriteActor> Enemies { get; set; } = new List<SpriteActor>();

		public List<SpaceSpawnerActor> Spawners { get; set; } = new List<SpaceSpawnerActor>();

		protected override void InitLevel()
		{
			base.InitLevel();

			var playerActor = new SpaceShipPlayer(new Sprite(new Texture(AssetManager.AssetsPath + "Player_01.png")), this);
			playerActor.ActorName = "Player 1";
			playerActor.Position = new TVector2f(0.0f, 300.0f);

			var playerController = new SpaceGamePlayerController(playerActor);
			playerController.SetCameraSize(EngineReference.EngineWindowWidth, EngineReference.EngineWindowHeight);

			var enemyActor = new SpaceShipEnemyFighter(new Sprite(new Texture(AssetManager.AssetsPath + "Enemy_01.png")), this);
			enemyActor.GetRootComponent<PhysicsComponent>().CollisionType = Category.Cat3;
			enemyActor.ActorName = "Enemy 1";
			enemyActor.Position = new TVector2f(0.0f, -300.0f);


			var wrapperData = EngineReference.AssetManager.JSONManager.Load<SpaceLevelDataWrapper>(AssetManager.LevelsPath + "level_1.json");
			foreach (var spawnerData in wrapperData.Spawners)
			{
				var spawner = new SpaceSpawnerActor(this);
				spawner.ActivationTime = spawnerData.ActivationTime;
				spawner.Ships = spawnerData.Ships;
				Spawners.Add(spawner); // TODO:
				RegisterActor(spawner);
			}




			var leftBorder = new Actor(this);
			leftBorder.ActorName = "Left Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(leftBorder, true, new TVector2f(-450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static, Category.Cat2, Category.All);
			leftBorder.Visible = true;

			var rightBorder = new Actor(this);
			rightBorder.ActorName = "Right Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(rightBorder, true, new TVector2f(450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static, Category.Cat2, Category.All);
			rightBorder.Visible = true;

			var topBorder = new Actor(this);
			topBorder.ActorName = "Top Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(topBorder, true, new TVector2f(0.0f, -450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static, Category.Cat2, Category.All);
			topBorder.Visible = true;

			var bottomBorder = new Actor(this);
			bottomBorder.ActorName = "Bottom Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(bottomBorder, true, new TVector2f(0.0f, 450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static, Category.Cat2, Category.All);
			bottomBorder.Visible = true;

			RegisterActor(playerActor);
			RegisterActor(enemyActor);
			RegisterActor(leftBorder);
			RegisterActor(rightBorder);
			RegisterActor(topBorder);
			RegisterActor(bottomBorder);
			RegisterPlayer(playerController);
		}

		protected override void LevelTick(float deltaTime)
		{
			LevelTime += deltaTime;
			base.LevelTick(deltaTime);
		}

		public override void OnLevelLoad()
		{
			base.OnLevelLoad();
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}


	}
}