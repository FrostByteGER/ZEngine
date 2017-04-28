using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;

namespace SFML_Pong
{
	public class PongMenuController : PlayerController
	{
		public PongMenuController()
		{
		}

		public PongMenuController(SpriteActor playerPawn) : base(playerPawn)
		{
		}

		public PongMenuLevel LevelRef { get; set; }
		public int SelectedIndex { get; set; } = 0;
		public override void RegisterInput(Engine engine)
		{
			Input = engine.InputManager;

			Input.RegisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.RegisterJoystickInput(null, null, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void UnregisterInput(Engine engine)
		{
			Input = engine.InputManager;

			Input.UnregisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.UnregisterJoystickInput(null, null, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			if (Input.EnterPressed)
			{
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Player vs. Player")
				{
					//OpenGameLevel(false);
					IsActive = false;
					((PongGameMode) Engine.Instance.Levels[1].GameMode).AIEnabled = false;
					Engine.Instance.LoadLevel(2);
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Player vs. Bot")
				{
					//OpenGameLevel(true);
					IsActive = false;
					((PongGameMode)Engine.Instance.Levels[1].GameMode).AIEnabled = true;
					Engine.Instance.LoadLevel(2);
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Mute Sounds")
				{
					Engine.Instance.GlobalVolume = 0;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Play Sounds";
				}else if (LevelRef.Menu[SelectedIndex].DisplayedString == "Play Sounds")
				{
					Engine.Instance.GlobalVolume = 10;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Mute Sounds";
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Exit Game")
				{
					Engine.Instance.CloseEngineWindow();
				}
			}
			if (Input.WPressed || Input.UpPressed)
			{
				if (SelectedIndex <= 0)
				{
					SelectedIndex = LevelRef.Menu.Count - 1;
				}
				else
				{
					--SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = PongMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = PongMenuLevel.ColorSelected;
			}
			if (Input.SPressed || Input.DownPressed)
			{
				if (SelectedIndex >= LevelRef.Menu.Count - 1)
				{
					SelectedIndex = 0;
				}
				else
				{
					++SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = PongMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = PongMenuLevel.ColorSelected;
			}
		}

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
		}

		protected override void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonPressed(sender, joystickButtonEventArgs);
			if (joystickButtonEventArgs.Button == 0)
			{
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Player vs. Player")
				{
					//OpenGameLevel(false);
					IsActive = false;
					((PongGameMode)Engine.Instance.Levels[1].GameMode).AIEnabled = false;
					Engine.Instance.LoadLevel(2);
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Player vs. Bot")
				{
					//OpenGameLevel(true);
					IsActive = false;
					((PongGameMode)Engine.Instance.Levels[1].GameMode).AIEnabled = true;
					Engine.Instance.LoadLevel(2);
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Mute Sounds")
				{
					Engine.Instance.GlobalVolume = 0;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Play Sounds";
				}
				else if (LevelRef.Menu[SelectedIndex].DisplayedString == "Play Sounds")
				{
					Engine.Instance.GlobalVolume = 10;
					LevelRef.Menu[SelectedIndex].DisplayedString = "Mute Sounds";
				}
				if (LevelRef.Menu[SelectedIndex].DisplayedString == "Exit Game")
				{
					Engine.Instance.CloseEngineWindow();
				}
			}
		}

		protected override void OnJoystickButtonReleased(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonReleased(sender, joystickButtonEventArgs);
		}

		protected override void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			if (joystickMoveEventArgs.Axis == Joystick.Axis.PovY && Math.Abs(joystickMoveEventArgs.Position - 100.0f) < 0.0001f)
			{
				if (SelectedIndex <= 0)
				{
					SelectedIndex = LevelRef.Menu.Count - 1;
				}
				else
				{
					--SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = PongMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = PongMenuLevel.ColorSelected;
			}
			if (joystickMoveEventArgs.Axis == Joystick.Axis.PovY && Math.Abs(joystickMoveEventArgs.Position - -100.0f) < 0.0001f)
			{
				if (SelectedIndex >= LevelRef.Menu.Count - 1)
				{
					SelectedIndex = 0;
				}
				else
				{
					++SelectedIndex;
				}
				foreach (var item in LevelRef.Menu)
				{
					item.Color = PongMenuLevel.ColorUnselected;
				}
				LevelRef.Menu[SelectedIndex].Color = PongMenuLevel.ColorSelected;
			}
		}

		public void OpenGameLevel(bool withAI)
		{
			Engine engine = Engine.Instance;
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

			topBorder.Position = new Vector2f(0, -20);
			bottomBorder.Position = new Vector2f(0, 600);
			leftBorder.Position = new Vector2f(-20, 0);
			rightBorder.Position = new Vector2f(800, 0);

			topBorder.CollisionShape = new BoxShape(800, 20);
			bottomBorder.CollisionShape = new BoxShape(800, 20);
			leftBorder.CollisionShape = new BoxShape(20, 600);
			rightBorder.CollisionShape = new BoxShape(20, 600);

			topBorder.CollisionShape.ShowCollisionShape = true;
			bottomBorder.CollisionShape.ShowCollisionShape = true;
			leftBorder.CollisionShape.ShowCollisionShape = true;
			rightBorder.CollisionShape.ShowCollisionShape = true;


			var gameLevel = new PongGameLevel();
			var leftPadTexture = new Texture("Assets/SFML_Pong/Goku.png");
			var rightPadTexture = new Texture("Assets/SFML_Pong/Goku_MLG.png");
			var ballTexture = new Texture("Assets/SFML_Pong/DragonBall4Star.png");

			var leftPad = new PongPlayerPad();
			leftPad.ActorName = "Left Pad";
			leftPad.MaxVelocity = 700.0f;
			leftPad.Position = new Vector2f(30, 30);
			leftPad.CollisionShape = new BoxShape(leftPadTexture.Size.X, leftPadTexture.Size.Y);
			leftPad.CollisionShape.Origin = leftPad.Origin;
			leftPad.CollisionShape.ShowCollisionShape = true;
			leftPad.Friction = 0.01f;

			var rightPad = new PongPlayerPad();
			rightPad.Position = new Vector2f(650, 30);
			rightPad.ActorName = "Right Pad";
			rightPad.MaxVelocity = 700.0f;
			rightPad.CollisionShape = new BoxShape(rightPadTexture.Size.X, rightPadTexture.Size.Y);
			rightPad.CollisionShape.Origin = rightPad.Origin;
			rightPad.CollisionShape.ShowCollisionShape = true;
			rightPad.Friction = 0.01f;

			var ball = new PongBall(ballTexture);
			ball.ActorName = "Ball";
			ball.CollisionShape = new SphereShape(ballTexture.Size.X);
			ball.Color = new Color(ball.Color.R, ball.Color.G, ball.Color.B, 60);
			ball.CollisionShape.Origin = ball.Origin;
			ball.Position = new Vector2f(400, 250);
			ball.MaxVelocity = 500.0f;
			ball.Velocity = new Vector2f(250, 0);
			ball.CollisionShape.ShowCollisionShape = true;

			engine.PhysicsEngine.AddActorToGroup("Pads", leftPad);
			engine.PhysicsEngine.AddActorToGroup("Pads", rightPad);
			engine.PhysicsEngine.AddActorToGroup("Balls", ball);
			engine.PhysicsEngine.AddGroup("PowerUP");

			engine.PhysicsEngine.AddActorToGroup("Borders", topBorder);
			engine.PhysicsEngine.AddActorToGroup("Borders", bottomBorder);
			engine.PhysicsEngine.AddActorToGroup("SoftBorders", leftBorder);
			engine.PhysicsEngine.AddActorToGroup("SoftBorders", rightBorder);


			engine.PhysicsEngine.AddCollidablePartner("Balls", "Pads");
			engine.PhysicsEngine.AddCollidablePartner("Balls", "Borders");
			engine.PhysicsEngine.AddCollidablePartner("Pads", "Borders");
			engine.PhysicsEngine.AddOverlapPartners("Balls", "SoftBorders");
			engine.PhysicsEngine.AddOverlapPartners("Balls", "PowerUP");

			var leftPadController = new PongPlayerController(leftPad);
			PongPlayerController rightPadController = null;
			if (withAI)
			{
				rightPadController = new PongPlayerController(rightPad);

			}
			else
			{
				rightPadController = new PongPlayerController(rightPad);

			}
			leftPadController.Name = "Player 1";
			rightPadController.Name = "Player 2";

			engine.UnregisterPlayer(this);
			engine.LoadLevel(gameLevel);

			gameLevel.RegisterActor(leftPad);
			gameLevel.RegisterActor(rightPad);
			gameLevel.RegisterActor(ball);
			gameLevel.RegisterActor(topBorder);
			gameLevel.RegisterActor(bottomBorder);
			gameLevel.RegisterActor(leftBorder);
			gameLevel.RegisterActor(rightBorder);
			gameLevel.GameMode = new PongGameMode();
			engine.RegisterPlayer(leftPadController);
			engine.RegisterPlayer(rightPadController);
			engine.StartEngine();

			// Super important! Delete all textures
			/*				ballTexture.Dispose();
							leftPadTexture.Dispose();
							rightPadTexture.Dispose();*/
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			LevelRef = PlayerPawn.LevelReference as PongMenuLevel;
			SelectedIndex = 0;
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}