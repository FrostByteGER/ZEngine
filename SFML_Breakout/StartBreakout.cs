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

			/*
			physics.AddGroup("Pads");
			physics.AddGroup("Balls");
			physics.AddGroup("Borders");
			physics.AddGroup("Blocks");

			physics.AddCollidablePartner("Balls", "Pads");
			physics.AddCollidablePartner("Balls", "Borders");
			physics.AddCollidablePartner("Pads", "Borders");
			physics.AddCollidablePartner("Balls", "Blocks");
			physics.AddCollidablePartner("Blocks", "Balls");
			*/

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

			topBorder.SetRootComponent(new CollisionComponent(new BoxShape(engine.EngineWindowWidth, 400)));
			bottomBorder.SetRootComponent(new CollisionComponent(new BoxShape(engine.EngineWindowWidth, 400)));
			leftBorder.SetRootComponent(new CollisionComponent(new BoxShape(20, engine.EngineWindowHeight)));
			rightBorder.SetRootComponent(new CollisionComponent(new BoxShape(20, engine.EngineWindowHeight)));

			topBorder.Position = new Vector2f(0, -400);
			bottomBorder.Position = new Vector2f(0, engine.EngineWindowHeight);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(engine.EngineWindowWidth, 0);



			(topBorder.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = false;
			(bottomBorder.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = false;
			(leftBorder.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = false;
			(rightBorder.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = false;

			var playerPad = new SpriteActor();
			playerPad.ActorName = "Player Pad 1";
			var playerPadBoxShape = new BoxShape(100.0f, 40.0f);
			playerPad.SetRootComponent(new CollisionComponent(new BoxShape(300.0f, 30.0f)));
			playerPad.Position = new Vector2f(engine.EngineWindowWidth / 2.0f - playerPadBoxShape.CollisionBounds.X / 2.0f, engine.EngineWindowHeight - playerPadBoxShape.CollisionBounds.Y * 2.0f);
			(playerPad.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = true;
			playerPad.RootComponent.Origin = new Vector2f(playerPadBoxShape.CollisionBounds.X / 2.0f, playerPadBoxShape.CollisionBounds.Y / 2.0f);
			var comp = new CollisionComponent(new BoxShape(100.0f, 30.0f));
			comp.CollisionBody.ShowCollisionShape = true;
			playerPad.AddComponent(comp);
			comp.ComponentName = "Test";
			comp.Move(150.0f, 40.0f);

			var mainBall = new BreakoutBall();
			mainBall.ActorName = "Ball";
			var mainBallSphereShape = new SphereShape(30.0f);
			mainBall.SetRootComponent(new CollisionComponent(new SphereShape(30.0f)));
			mainBall.Position = new Vector2f(engine.EngineWindowWidth / 2.0f - mainBallSphereShape.CollisionBounds.X / 2.0f, engine.EngineWindowHeight - mainBallSphereShape.CollisionBounds.X * 4.0f);
			(mainBall.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = true;
			mainBall.Velocity = new Vector2f(0.0f, 250.0f);

			var breakoutPlayerController = new BreakoutPlayerController(playerPad);
			breakoutPlayerController.Name = "Player 1";

			/*physics.AddActorToGroup("Borders", topBorder);
			physics.AddActorToGroup("Borders", bottomBorder);
			physics.AddActorToGroup("Borders", leftBorder);
			physics.AddActorToGroup("Borders", rightBorder);
			physics.AddActorToGroup("Pads", playerPad);
			physics.AddActorToGroup("Balls", mainBall);*/

			List<Block> blocks = new List<Block>();
			for (uint i = 0; i < 6; ++i)
			{
				for (uint j = 0; j < 6; ++j)
				{
					var block = new Block();
					block.ActorName = "Block" + (i + j);
					var blockBoxShape = new BoxShape(100.0f, 40.0f);
					block.SetRootComponent(new CollisionComponent(blockBoxShape));
					block.Position = new Vector2f(80.0f + blockBoxShape.CollisionBounds.X * j, 80.0f + blockBoxShape.CollisionBounds.Y * i);
					(block.RootComponent as CollisionComponent).CollisionBody.ShowCollisionShape = true;
					block.Hitpoints = (uint) EngineMath.EngineRandom.Next(1, 4);
					block.MaxHitpoints = block.Hitpoints;
					block.Score *= block.MaxHitpoints;
					//physics.AddActorToGroup("Blocks", block);
					blocks.Add(block);
				}
			}

			var testlvl = new Level();
			var gameMode = new BreakoutGameMode();
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