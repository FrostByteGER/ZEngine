using System;
using System.Collections.Generic;
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

		public static void Main(string[] args)
		{
			if (args.Length >= 1)
			{
				MountainDewMode = bool.Parse(args[0]);
			}

			Engine engine = Engine.Instance;
			engine.GameInfo = new BreakoutGameInfo();
			engine.EngineWindowWidth = 800;
			engine.EngineWindowHeight = 600;
			engine.InitEngine();
			var physics = engine.PhysicsEngine;

			physics.AddGroup("Pads");
			physics.AddGroup("Balls");
			physics.AddGroup("Borders");
			physics.AddGroup("Blocks");
			physics.AddGroup("PowerUp");
			physics.AddGroup("Bullets");

			physics.AddCollidablePartner("Balls", "Pads");
			physics.AddCollidablePartner("Balls", "Borders");
			physics.AddCollidablePartner("PowerUp", "Pads");
			physics.AddCollidablePartner("PowerUp", "Borders");
			physics.AddCollidablePartner("Pads", "Borders");
			physics.AddCollidablePartner("Balls", "Blocks");
			physics.AddCollidablePartner("Blocks", "Balls");

			physics.AddOverlapPartners("Blocks", "Bullets");
			physics.AddOverlapPartners("Bullets", "Borders");

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
			bottomBorder.Position = new Vector2f(0, engine.EngineWindowHeight);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(engine.EngineWindowWidth, 0);

			topBorder.CollisionShape = new BoxShape(engine.EngineWindowWidth, 400);
			bottomBorder.CollisionShape = new BoxShape(engine.EngineWindowWidth, 400);
			leftBorder.CollisionShape = new BoxShape(20, engine.EngineWindowHeight);
			rightBorder.CollisionShape = new BoxShape(20, engine.EngineWindowHeight);

			topBorder.CollisionShape.ShowCollisionShape = true;
			bottomBorder.CollisionShape.ShowCollisionShape = true;
			leftBorder.CollisionShape.ShowCollisionShape = true;
			rightBorder.CollisionShape.ShowCollisionShape = true;

			var playerPad = new SpriteActor();
			playerPad.ActorName = "Player Pad 1";
			playerPad.CollisionShape = new BoxShape(300.0f, 30.0f);
			playerPad.Position = new Vector2f(engine.EngineWindowWidth / 2.0f - playerPad.CollisionShape.CollisionBounds.X / 2.0f, engine.EngineWindowHeight - playerPad.CollisionShape.CollisionBounds.Y * 2.0f);
			playerPad.CollisionShape.ShowCollisionShape = true;
			playerPad.CollisionShape.Position = playerPad.Position;
			playerPad.Friction = 0.01f;
			var comp = new ActorComponent();
			playerPad.AddComponent(comp);

			var mainBall = new BreakoutBall();
			mainBall.ActorName = "Ball";
			mainBall.CollisionShape = new SphereShape(30.0f);
			mainBall.Position = new Vector2f(engine.EngineWindowWidth / 2.0f - mainBall.CollisionShape.CollisionBounds.X / 2.0f, engine.EngineWindowHeight - mainBall.CollisionShape.CollisionBounds.X * 4.0f);
			mainBall.CollisionShape.ShowCollisionShape = true;
			mainBall.CollisionShape.Position = mainBall.Position;
			mainBall.Velocity = new Vector2f(0.0f, 250.0f);

			var breakoutPlayerController = new BreakoutPlayerController(playerPad);
			breakoutPlayerController.Name = "Player 1";

			physics.AddActorToGroup("Borders", topBorder);
			physics.AddActorToGroup("Borders", bottomBorder);
			physics.AddActorToGroup("Borders", leftBorder);
			physics.AddActorToGroup("Borders", rightBorder);
			physics.AddActorToGroup("Pads", playerPad);
			physics.AddActorToGroup("Balls", mainBall);

			List<Block> blocks = new List<Block>();
			for (uint i = 0; i < 6; ++i)
			{
				for (uint j = 0; j < 6; ++j)
				{
					var block = new Block();
					block.ActorName = "Block" + (i + j);
					block.CollisionShape = new BoxShape(100.0f, 40.0f);
					block.Position = new Vector2f(80.0f + block.CollisionShape.CollisionBounds.X * j, 80.0f + block.CollisionShape.CollisionBounds.Y * i);
					block.CollisionShape.ShowCollisionShape = true;
					block.CollisionShape.Position = block.Position;
					block.Hitpoints = (uint) EngineMath.EngineRandom.Next(1, 4);
					block.MaxHitpoints = block.Hitpoints;
					block.Score *= block.MaxHitpoints;
					physics.AddActorToGroup("Blocks", block);
					blocks.Add(block);
				}
			}

			var testlvl = new Level();
			var gameMode = new BreakoutGameMode();
			//gameMode.AddPowerUp(new PowerUpDup());
			//gameMode.AddPowerUp(new PowerUpPadSizeInc());
			//gameMode.AddPowerUp(new PuwerUpPadSizeDec());
			//gameMode.AddPowerUp(new PowerUpBullets());
			gameMode.AddPowerUp(new PowerUpPunchThrow());

			testlvl.GameMode = gameMode;
			engine.RegisterLevel(testlvl);
			testlvl.RegisterActor(topBorder);
			testlvl.RegisterActor(bottomBorder);
			testlvl.RegisterActor(leftBorder);
			testlvl.RegisterActor(rightBorder);
			testlvl.RegisterActor(playerPad);
			testlvl.RegisterActor(mainBall);
			testlvl.RegisterPlayer(breakoutPlayerController);
			foreach (var block in blocks)
			{
				testlvl.RegisterActor(block);
			}

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}