using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_SpaceSEM
{
	public class SpaceSEMGameLevel : Level
	{
		public SpaceSEMPlayer Player { get; set; } = null;

		public List<SpaceSEMEnemy> Enemies { get; set; } = new List<SpaceSEMEnemy>();

		public Text HighscoreText { get; set; }
		public override void InitLevel()
		{
			base.InitLevel();


			var topBorder = new SpriteActor();
			var bottomBorder = new SpriteActor();
			var leftBorder = new SpriteActor();
			var rightBorder = new SpriteActor();

			topBorder.Movable = false;
			bottomBorder.Movable = false;
			leftBorder.Movable = false;
			rightBorder.Movable = false;

			topBorder.ActorName = "Top Border";
			bottomBorder.ActorName = "Bottom Border";
			leftBorder.ActorName = "Left Border";
			rightBorder.ActorName = "Right Border";

			topBorder.Position = new Vector2f(0, -400);
			bottomBorder.Position = new Vector2f(0, EngineReference.EngineWindowHeight);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(EngineReference.EngineWindowWidth, 0);

			topBorder.CollisionShape = new BoxShape(EngineReference.EngineWindowWidth, 400);
			bottomBorder.CollisionShape = new BoxShape(EngineReference.EngineWindowWidth, 400);
			leftBorder.CollisionShape = new BoxShape(20, EngineReference.EngineWindowHeight);
			rightBorder.CollisionShape = new BoxShape(20, EngineReference.EngineWindowHeight);

			topBorder.CollisionShape.ShowCollisionShape = true;
			bottomBorder.CollisionShape.ShowCollisionShape = true;
			leftBorder.CollisionShape.ShowCollisionShape = true;
			rightBorder.CollisionShape.ShowCollisionShape = true;

			var playerTexture = new Texture("Assets/SFML_SpaceSEM/Player_01.png");
			var playerShip = new SpaceSEMPlayer(playerTexture);
			playerShip.ActorName = "Player Ship 1";
			playerShip.CollisionShape = new BoxShape(playerTexture.Size.X, playerTexture.Size.Y);
			playerShip.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f - playerShip.CollisionShape.CollisionBounds.X / 2.0f, EngineReference.EngineWindowHeight - playerShip.CollisionShape.CollisionBounds.Y - 5.0f);
			playerShip.CollisionShape.ShowCollisionShape = true;
			playerShip.CollisionShape.Position = playerShip.Position;
			playerShip.Friction = 0.01f;


			var enemyTexture = new Texture("Assets/SFML_SpaceSEM/Enemy_01.png");
			var enemyShip = new SpaceSEMEnemy(enemyTexture);
			enemyShip.ActorName = "Enemy Ship 1";
			enemyShip.CollisionShape = new BoxShape(enemyTexture.Size.X, enemyTexture.Size.Y);
			enemyShip.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f - enemyShip.CollisionShape.CollisionBounds.X / 2.0f, enemyShip.CollisionShape.CollisionBounds.Y + 5.0f);
			enemyShip.CollisionShape.ShowCollisionShape = true;
			enemyShip.CollisionShape.Position = enemyShip.Position;
			enemyShip.Friction = 0.01f;

			Enemies.Add(enemyShip);

			var breakoutPlayerController = new SpaceSEMPlayerController(playerShip);
			breakoutPlayerController.Name = "Player 1";

			EngineReference.PhysicsEngine.AddActorToGroup("Borders", topBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Borders", bottomBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Borders", leftBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Borders", rightBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Players", playerShip);
			EngineReference.PhysicsEngine.AddActorToGroup("Enemies", enemyShip);

			HighscoreText = new Text();
			HighscoreText.Font = SpaceSEMMenuLevel.MainGameFont;
			HighscoreText.DisplayedString = "Highscore: 0";
			HighscoreText.CharacterSize = 50;
			HighscoreText.Color = Color.White;
			HighscoreText.Style = Text.Styles.Bold;
			HighscoreText.Origin = new Vector2f(HighscoreText.GetLocalBounds().Width / 2.0f, HighscoreText.GetLocalBounds().Height / 2.0f);
			HighscoreText.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 25);

			var gameMode = new SpaceSEMGameMode();

			GameMode = gameMode;
			RegisterActor(topBorder);
			RegisterActor(bottomBorder);
			RegisterActor(leftBorder);
			RegisterActor(rightBorder);
			RegisterActor(playerShip);
			RegisterActor(enemyShip);

			RegisterActor(HighscoreText);
			RegisterPlayer(breakoutPlayerController);

			Player = playerShip;
		}

		public override void ShutdownLevel()
		{
			base.ShutdownLevel();
			Player.Texture.Dispose();
			foreach (var enemy in Enemies)
			{
				enemy.RespawnTimer.Stop();
				enemy.Texture.Dispose();
			}
			UnregisterPlayers();
			UnregisterActors();
		}

		public void UpdateHighscoreText(uint highscore)
		{
			HighscoreText.DisplayedString = "Highscore: " + (((SpaceSEMPersistentGameMode)EngineReference.PersistentGameMode).HighScore + ((SpaceSEMGameMode)GameMode).Player.Score);
			HighscoreText.Origin = new Vector2f(HighscoreText.GetLocalBounds().Width / 2.0f, HighscoreText.GetLocalBounds().Height / 2.0f);
			HighscoreText.Position = new Vector2f(EngineReference.EngineWindowWidth / 2.0f, 25);
		}
	}
}