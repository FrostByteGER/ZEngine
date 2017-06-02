using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;

namespace SFML_SpaceSEM
{
	public class SpaceSEMPlayerController : PlayerController
	{

		public uint Score { get; set; } = 0;

		public SpaceSEMGameMode GameModeReference { get; set; }

		public SpaceSEMPlayerController(SpriteActor playerPawn) : base(playerPawn)
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
			var mode = (SpaceSEMGameMode)LevelReference.GameMode;
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
			//Console.WriteLine(Score);
		}

		protected override void OnJoystickButtonPressed(object sender, JoystickButtonEventArgs joystickButtonEventArgs)
		{
			base.OnJoystickButtonPressed(sender, joystickButtonEventArgs);
			if (ID == 0)
			{
				if (joystickButtonEventArgs.Button == 7 || joystickButtonEventArgs.Button == 1)
				{
					LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, LevelReference.EngineReference.Levels[0])));

				}
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
		}

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);
			if (ID == 0)
			{
				if (Input.EscPressed)
				{
					LevelReference.EngineReference.RegisterEvent(new SwitchLevelEvent<SwitchLevelParams>(new SwitchLevelParams(this, LevelReference.EngineReference.Levels[0])));
				}
				if (Input.F12Pressed)
				{

					((SpaceSEMGameMode)LevelReference.GameMode).LoadNextLevel();
				}
				if (Input.APressed)
				{
					PlayerPawn.Acceleration = new Vector2f(-500.0f, 0.0f);
				}

				if (Input.DPressed)
				{
					PlayerPawn.Acceleration = new Vector2f(500.0f, 0.0f);
				}

				if (Input.SpacePressed)
				{
					SpaceSEMBullet bullet = new SpaceSEMBullet();
					bullet.ActorName = "Bullet";
					bullet.CollisionShape = new BoxShape(2.0f, 4.0f);
					bullet.Position = new Vector2f(PlayerPawn.Position.X + PlayerPawn.Texture.Size.X / 2.0f - bullet.CollisionShape.CollisionBounds.X / 2.0f, PlayerPawn.Position.Y);
					bullet.CollisionShape.ShowCollisionShape = true;
					bullet.CollisionShape.Position = bullet.Position;
					bullet.Velocity = new Vector2f(0.0f, -300.0f);
					LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("Bullets", bullet);
					//bullet.Friction = 0.01f;
					LevelReference.EngineReference.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, bullet, LevelReference.LevelID)));
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
		}
	}
}