using System;
using BulletSharp;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;

namespace SFML_Breakout
{
	public sealed class StartBreakout
	{
		public static bool MountainDewMode { get; set; } = true;

		public static Vector2f LevelBoundsHalf = new Vector2f(700.0f, 700.0f);

		public static void Main(string[] args)
		{
			if (args.Length >= 1)
			{
				MountainDewMode = bool.Parse(args[0]);
			}

			Engine engine = Engine.Instance;
			engine.GameInfo = new BreakoutGameInfo();
			engine.EngineWindowWidth = 1024;
			engine.EngineWindowHeight = 800;
			engine.InitEngine();
			var physics = engine.PhysicsEngine;
			physics.Gravity = new Vector2f();

			var topBorder = new Actor();
			var bottomBorder = new Actor();
			var leftBorder = new Actor();
			var rightBorder = new Actor();

			topBorder.Movable = false;
			bottomBorder.Movable = false;
			leftBorder.Movable = false;
			rightBorder.Movable = false;

			topBorder.ActorName = "Top Border";
			bottomBorder.ActorName = "Bottom Border";
			leftBorder.ActorName = "Left Border";
			rightBorder.ActorName = "Right Border";

			BoxShape topColShape = new BoxShape(LevelBoundsHalf.X, 10.0f, 50.0f);
			var topComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, -(LevelBoundsHalf.Y + 10.0f)), 0.0f, topColShape, (short)BreakoutCollisionTypes.Borders);
			topComponent.CollisionBody.Restitution = 1.0f;
			topBorder.SetRootComponent(topComponent);

			BoxShape bottomColShape = new BoxShape(LevelBoundsHalf.X, 10.0f, 50.0f);
			var bottomComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(0.0f, LevelBoundsHalf.Y + 10.0f), 0.0f, bottomColShape, (short)BreakoutCollisionTypes.Borders);
			bottomComponent.CollisionBody.Restitution = 1.0f;
			bottomBorder.SetRootComponent(bottomComponent);

			BoxShape leftColShape = new BoxShape(10.0f, LevelBoundsHalf.Y, 50.0f);
			var leftComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(-(LevelBoundsHalf.X + 10.0f), 0.0f), 0.0f, leftColShape, (short)BreakoutCollisionTypes.Borders);
			leftComponent.CollisionBody.Restitution = 1.0f;
			leftBorder.SetRootComponent(leftComponent);

			BoxShape rightColShape = new BoxShape(10.0f, LevelBoundsHalf.Y, 50.0f);
			var rightComponent = physics.ConstructCollisionComponent(0.0f, new Vector2f(LevelBoundsHalf.X + 10.0f, 0.0f), 0.0f, rightColShape, (short)BreakoutCollisionTypes.Borders);
			rightComponent.CollisionBody.Restitution = 1.0f;
			rightBorder.SetRootComponent(rightComponent);

			topBorder.Position = new Vector2f(0, -LevelBoundsHalf.Y - 10.0f);
			bottomBorder.Position = new Vector2f(0.0f, LevelBoundsHalf.Y + 10.0f);
			leftBorder.Position = new Vector2f(-LevelBoundsHalf.X - 10.0f, 0.0f);
			rightBorder.Position = new Vector2f(LevelBoundsHalf.X + 10.0f, 0.0f);



			var playerPad = new SpriteActor();
			playerPad.ActorName = "Player Pad 1";
			var playerPadBoxShape = new BoxShape(100.0f, 10.0f, 50.0f);
			var playerPadComponent = physics.ConstructCollisionComponent(1.0f, new Vector2f(0.0f, LevelBoundsHalf.Y - 50.0f), 0.0f, playerPadBoxShape, (short)BreakoutCollisionTypes.Kinematic, new TVector2f(1,0),false);
			playerPadComponent.CollisionBody.ForceActivationState(ActivationState.DisableDeactivation);
			playerPadComponent.CollisionBody.Restitution = 1.0f;
			playerPad.SetRootComponent(playerPadComponent);
			playerPad.Position = new Vector2f(0.0f, LevelBoundsHalf.Y - 50.0f);
			playerPad.MaxVelocity = 400.0f;

			
			var mainBall = new BreakoutBall();
			mainBall.ActorName = "Ball";
			var mainBallSphereShape = new SphereShape(50.0f);
			var mainBallComponent = physics.ConstructCollisionComponent(1.0f, new Vector2f(0.0f, 0.0f), 0.0f, mainBallSphereShape, (short)BreakoutCollisionTypes.Dynamic, new TVector2f(1,1), true);
			mainBallComponent.CollisionBody.ForceActivationState(ActivationState.DisableDeactivation);
			mainBallComponent.CollisionBody.Restitution = 1.0f;
			mainBall.SetRootComponent(mainBallComponent);
			mainBall.MaxVelocity = 400.0f;
			mainBallComponent.CollisionBody.LinearVelocity = new TVector2f(0.0f, 350.0f);


			/*
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
					block.Hitpoints = (uint) EngineMath.EngineRandom.Next(1, 4);
					block.MaxHitpoints = block.Hitpoints;
					block.Score *= block.MaxHitpoints;
					//physics.AddActorToGroup("Blocks", block);
					blocks.Add(block);
				}
			}
			*/

			var breakoutPlayerController = new BreakoutPlayerController(playerPad);
			breakoutPlayerController.SetCameraSize(LevelBoundsHalf * 2.0f);
			breakoutPlayerController.Name = "Player 1";

			var testlvl = new Level();
			testlvl.LevelBounds = LevelBoundsHalf;
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
			//foreach (var block in blocks)
			//{
				//testlvl.RegisterActor(block);
			//}

			engine.StartEngine();
			Console.ReadLine();
		}
	}
}