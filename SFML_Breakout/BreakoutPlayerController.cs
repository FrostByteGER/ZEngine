using System;
using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Game;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Breakout
{
	public class BreakoutPlayerController : PlayerController
	{

		public uint Score { get; set; } = 0;

		public BreakoutGameMode GameModeReference { get; set; }

		public bool LeftBoundReached = false;
		public bool RightBoundReached = false;

		public BreakoutPlayerController()
		{
		}

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
			if (!Input.DPressed && Math.Abs(-LevelReference.LevelBounds.X - PlayerPawn.Position.X) < Math.Abs(PlayerPawn.ActorBounds.X + 10.0f) && !LeftBoundReached)
			{
				var phys = (CollisionComponent)PlayerPawn.RootComponent;
				phys.CollisionBody.LinearVelocity = new TVector2f();
				phys.CollisionBody.SetDamping(1.0f, 0.0f);
				LeftBoundReached = true;
			}
			if (!Input.APressed && Math.Abs(LevelReference.LevelBounds.X - PlayerPawn.Position.X) < Math.Abs(PlayerPawn.ActorBounds.X + 10.0f) && !RightBoundReached)
			{
				var phys = (CollisionComponent)PlayerPawn.RootComponent;
				phys.CollisionBody.LinearVelocity = new TVector2f();
				phys.CollisionBody.SetDamping(1.0f, 0.0f);
				RightBoundReached = true;
			}
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
				if (Input.APressed && !LeftBoundReached)
				{
					Console.WriteLine(">>>>>>" + keyEventArgs.Code + " " + ((CollisionComponent)PlayerPawn.RootComponent).CollisionBody.LinearVelocity);
					RightBoundReached = false;
					var phys = (CollisionComponent)PlayerPawn.RootComponent;
					phys.CollisionBody.SetDamping(0.0f, 0.0f);
					phys.CollisionBody.LinearVelocity = new TVector2f(-PlayerPawn.MaxVelocity, 0.0f);
				}

				if (Input.DPressed && !RightBoundReached)
				{
					Console.WriteLine(keyEventArgs.Code + " " + ((CollisionComponent)PlayerPawn.RootComponent).CollisionBody.LinearVelocity);
					LeftBoundReached = false;
					var phys = (CollisionComponent)PlayerPawn.RootComponent;
					phys.CollisionBody.LinearVelocity = new TVector2f(PlayerPawn.MaxVelocity, 0.0f);
					phys.CollisionBody.SetDamping(0.0f, 0.0f);
				}
				
			}
		}

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
			var phys = (CollisionComponent)PlayerPawn.RootComponent;
			if (ID == 0)
			{
				if (!Input.WPressed)
				{
					//phys.CollisionBody.LinearVelocity = new TVector2f();
					phys.CollisionBody.SetDamping(0.75f, 0.0f);
				}

				if (!Input.SPressed)
				{
					//phys.CollisionBody.LinearVelocity = new TVector2f();
					phys.CollisionBody.SetDamping(0.75f, 0.0f);
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