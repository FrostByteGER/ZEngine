using System.Collections.Generic;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	public class BreakoutGameLevel : Level
	{
		public List<Block> Blocks { get; set; }
		public Actor Pad { get; set; } = null;
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

			var playerPad = new SpriteActor();
			playerPad.ActorName = "Player Pad 1";
			playerPad.CollisionShape = new BoxShape(300.0f, 30.0f);
			playerPad.Position = new Vector2f(
				EngineReference.EngineWindowWidth / 2.0f - playerPad.CollisionShape.CollisionBounds.X / 2.0f,
				EngineReference.EngineWindowHeight - playerPad.CollisionShape.CollisionBounds.Y * 2.0f);
			playerPad.CollisionShape.ShowCollisionShape = true;
			playerPad.CollisionShape.Position = playerPad.Position;
			playerPad.Friction = 0.01f;
			var comp = new ActorComponent();
			playerPad.AddComponent(comp);

			var mainBall = new BreakoutBall();
			mainBall.ActorName = "Ball";
			mainBall.CollisionShape = new SphereShape(15.0f);
			mainBall.Position = new Vector2f(
				EngineReference.EngineWindowWidth / 2.0f - mainBall.CollisionShape.CollisionBounds.X / 2.0f,
				EngineReference.EngineWindowHeight - mainBall.CollisionShape.CollisionBounds.X * 10.0f);
			mainBall.CollisionShape.ShowCollisionShape = true;
			mainBall.CollisionShape.Position = mainBall.Position;
			mainBall.Velocity = new Vector2f(0.0f, 250.0f);

			var breakoutPlayerController = new BreakoutPlayerController(playerPad);
			breakoutPlayerController.Name = "Player 1";

			EngineReference.PhysicsEngine.AddActorToGroup("Borders", topBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Borders", bottomBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Borders", leftBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Borders", rightBorder);
			EngineReference.PhysicsEngine.AddActorToGroup("Pads", playerPad);
			EngineReference.PhysicsEngine.AddActorToGroup("Balls", mainBall);

			var gameMode = new BreakoutGameMode();
			gameMode.AddPowerUp(new PowerUpDup());
			gameMode.AddPowerUp(new PowerUpPadSizeInc());
			gameMode.AddPowerUp(new PuwerUpPadSizeDec());
			gameMode.AddPowerUp(new PowerUpBullets());
			gameMode.Balls.Add(mainBall);
			gameMode.Blocks = Blocks;

			GameMode = gameMode;
			RegisterActor(topBorder);
			RegisterActor(bottomBorder);
			RegisterActor(leftBorder);
			RegisterActor(rightBorder);
			RegisterActor(playerPad);
			RegisterActor(mainBall);
			foreach (var actor in Blocks)
			{
				RegisterActor(actor);
				EngineReference.PhysicsEngine.AddActorToGroup("Blocks", actor);
			}
			RegisterPlayer(breakoutPlayerController);
		}

		public override void ShutdownLevel()
		{
			base.ShutdownLevel();
			UnregisterPlayers();
			UnregisterActors();
		}
	}
}