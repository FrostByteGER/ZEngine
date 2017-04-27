using System;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongPlayerController : PlayerController
	{

		public int Score { get; set; } = 0;
		public Level LevelReference { get; set; }
		public PongGameMode GameModeReference { get; set; }
		public PongPlayerController()
		{

		}

		public PongPlayerController(SpriteActor playerPawn) : base(playerPawn)
		{

		}

		public override void RegisterInput(Engine engine)
		{
			Input = engine.InputManager;

			Input.RegisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.RegisterJoystickInput(OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void UnregisterInput(Engine engine)
		{
			Input = engine.InputManager;

			Input.UnregisterKeyInput(OnKeyPressed, OnKeyReleased);

			Input.UnregisterJoystickInput(OnJoystickConnected, OnJoystickDisconnected, OnJoystickButtonPressed, OnJoystickButtonReleased, OnJoystickMoved);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			LevelReference = PlayerPawn.LevelReference;
			var mode = LevelReference.GameMode as PongGameMode;
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

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);

			if (Input.EnterPressed)
			{
				if (GameModeReference.GameRunning)
				{
					GameModeReference.SpawnBall();
				}else if (GameModeReference.GameEnded)
				{
					GameModeReference.RestartGame();
				}
			}



			if (ID == 1)
			{
				if (Input.EscPressed)
				{
					IsActive = false;
					Engine.Instance.Players[0].IsActive = true;
					Engine.Instance.LoadLevel(1);
					return;
				}
				if (Input.WPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, -500.0f);
					//PlayerPawn.Velocity = new Vector2f(0.0f, -400.0f);
				}

				if (Input.SPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 500.0f);
					//PlayerPawn.Velocity = new Vector2f(0.0f, 400.0f);
				}

			}else if (ID == 2)
			{
				if (Input.UpPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, -500.0f);
					//PlayerPawn.Velocity = new Vector2f(0.0f, -400.0f);
				}

				if (Input.DownPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 500.0f);
					//PlayerPawn.Velocity = new Vector2f(0.0f, 400.0f);
				}
			}
		}

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
			if (ID == 1)
			{
				//Console.WriteLine(PlayerPawn.Velocity);

				if (!Input.WPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
					//PlayerPawn.Velocity = new Vector2f(0f, 0f);
				}

				if (!Input.SPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
					//PlayerPawn.Velocity = new Vector2f(0f, 0f);
				}
			}
			else if (ID == 2)
			{
				if (!Input.UpPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
					//PlayerPawn.Velocity = new Vector2f(0f, 0f);
				}

				if (!Input.DownPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(0.0f, 0);
					//PlayerPawn.Velocity = new Vector2f(0f, 0f);
				}
			}
			//Console.WriteLine(PlayerPawn.ActorName+" "+PlayerPawn.Velocity);
		}
	}
}