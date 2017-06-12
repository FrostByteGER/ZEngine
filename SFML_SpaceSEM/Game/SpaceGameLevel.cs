using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.IO;
using SFML_Engine.Engine.Utility;
using SFML_SpaceSEM.Game.Actors;
using SFML_SpaceSEM.Game.Players;
using SFML_SpaceSEM.IO;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;

namespace SFML_SpaceSEM.Game
{
	public class SpaceGameLevel : SpaceLevel
	{

		internal float LevelTime { get; set; } = 0.0f;
		public SpriteActor Player { get; set; } = null;

		public List<SpriteActor> Enemies { get; set; } = new List<SpriteActor>();

		public List<SpaceSpawnerActor> Spawners { get; set; } = new List<SpaceSpawnerActor>();

		public uint SpaceLevelID { get; set; } = 1;

		public Music GameMusic { get; set; }

		protected override void InitLevel()
		{
			base.InitLevel();

			var gameMode = new SpaceGameMode();
			GameMode = gameMode;

			var playerActor = new SpaceShipPlayer(new Sprite(new Texture(AssetManager.AssetsPath + "Player_01.png")), this);
			playerActor.ActorName = "Player 1";
			playerActor.Position = new TVector2f(0.0f, 300.0f);

			var playerController = new SpaceGamePlayerController(playerActor);
			playerController.SetCameraSize(EngineReference.EngineWindowWidth, EngineReference.EngineWindowHeight);

			var wrapperData = JSONManager.LoadObject<SpaceLevelDataWrapper>(AssetManager.LevelsPath + "level_" + SpaceLevelID + ".json");
			foreach (var spawnerData in wrapperData.Spawners)
			{
				var spawner = new SpaceSpawnerActor(this);
				spawner.ActivationTime = spawnerData.ActivationTime;
				spawner.Ships = spawnerData.Ships;
				gameMode.EnemiesRemaining += (uint)spawnerData.Ships.Count;
				Spawners.Add(spawner);
				RegisterActor(spawner);
			}



			var background = new BackgroundActor(new Sprite(new Texture(AssetManager.AssetsPath + "Background_0" + SpaceLevelID + ".png")), this);
			background.ActorName = "Background";


			var leftBorder = new Actor(this);
			leftBorder.ActorName = "Left Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(leftBorder, true, new TVector2f(-450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static, Category.Cat4, Category.All);
			leftBorder.Visible = true;

			var rightBorder = new Actor(this);
			rightBorder.ActorName = "Right Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(rightBorder, true, new TVector2f(450.0f, 0.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(50.0f, 400.0f), BodyType.Static, Category.Cat4, Category.All);
			rightBorder.Visible = true;

			var topBorder = new Actor(this);
			topBorder.ActorName = "Top Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(topBorder, true, new TVector2f(0.0f, -450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static, Category.Cat2, Category.All);
			topBorder.Visible = true;

			var bottomBorder = new Actor(this);
			bottomBorder.ActorName = "Bottom Border";
			PhysicsEngine.ConstructRectangleCollisionComponent(bottomBorder, true, new TVector2f(0.0f, 450.0f), 0.0f, new TVector2f(1.0f, 1.0f), 0.0f, new TVector2f(400.0f, 50.0f), BodyType.Static, Category.Cat4, Category.All);
			bottomBorder.Visible = true;

			RegisterActor(background);
			RegisterActor(playerActor);
			RegisterActor(leftBorder);
			RegisterActor(rightBorder);
			RegisterActor(topBorder);
			RegisterActor(bottomBorder);
			RegisterPlayer(playerController);

			GameMusic = SoundPoolManager.LoadMusic(SoundPoolManager.SFXPath + "BGM_Battle_0" + SpaceLevelID + ".ogg");

			GameMusic.Loop = true;
			GameMusic.Volume = EngineReference.GlobalMusicVolume;
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
			GameMusic.Play();
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
			GameMusic.Pause();
		}

		public override void OnGameResume()
		{
			base.OnGameResume();
			GameMusic.Play();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			GameMusic.Stop();
		}


	}
}