using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;

namespace SFML_Breakout
{
	public class BreakoutPlayerController : PlayerController
	{

		public uint Score { get; set; } = 0;

		public BreakoutGameMode GameModeReference { get; set; }

		public BreakoutPlayerController(SpriteActor playerPawn) : base(playerPawn)
		{
		}

		public override void RegisterInput()
		{
			Input = LevelReference.EngineReference.InputManager;

			Input.RegisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.RegisterJoystickInput(OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void UnregisterInput()
		{
			Input = LevelReference.EngineReference.InputManager;

			Input.UnregisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.UnregisterJoystickInput(OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			var mode = LevelReference.GameMode as BreakoutGameMode;
			if (mode != null)
			{
				GameModeReference = mode;
			}
		}

		public override void OnGamePause()
		{
			base.OnGamePause();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		protected override void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonPressed(sender, joystickButtonEventArgs);
			if (ID == 0)
			{
			}
		}

		protected override void OnJoystickMoved(object sender, JoystickMoveEventArgs joystickMoveEventArgs)
		{
			if (ID == 0)
			{

				if (joystickMoveEventArgs.Axis == Joystick.Axis.Y && joystickMoveEventArgs.Position > 20.0f)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 500.0f);
				}
				if (joystickMoveEventArgs.Axis == Joystick.Axis.Y && joystickMoveEventArgs.Position < -20.0f)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, -500.0f);
				}
				if (joystickMoveEventArgs.Axis == Joystick.Axis.Y && (joystickMoveEventArgs.Position < 20.0f && joystickMoveEventArgs.Position > -20.0f))
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
				}

			}
			else if (ID == 1)
			{
				if (joystickMoveEventArgs.Axis == Joystick.Axis.R && joystickMoveEventArgs.Position > 20.0f)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 500.0f);
				}
				if (joystickMoveEventArgs.Axis == Joystick.Axis.R && joystickMoveEventArgs.Position < -20.0f)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, -500.0f);
				}
				if (joystickMoveEventArgs.Axis == Joystick.Axis.R && (joystickMoveEventArgs.Position < 20.0f && joystickMoveEventArgs.Position > -20.0f))
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
				}
			}
		}

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);
			if (ID == 0)
			{
				if (Input.APressed)
				{
					PlayerPawn.Acceleration = new Vector2f(-500.0f, 0.0f);
					//PlayerPawn.Move(-10.0f, 0.0f);
					PlayerPawn.Rotate(-10.0f);
					//PlayerPawn.ScaleActor(-0.1f,-0.1f);
				}

				if (Input.DPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(500.0f, 0.0f);
					//PlayerPawn.Move(10.0f, 0.0f);
					PlayerPawn.Rotate(10.0f);
					//PlayerPawn.ScaleActor(0.1f, 0.1f);
				}
			}
		}

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
			if (ID == 0)
			{
				if (!Input.WPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
				}

				if (!Input.SPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
				}
			}
			else if (ID == 1)
			{
				if (!Input.UpPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
				}

				if (!Input.DownPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
				}
			}
		}
	}
}