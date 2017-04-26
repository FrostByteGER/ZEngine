using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;

namespace SFML_Pong
{
	public class PongPlayerController : PlayerController
	{

		public uint Score { get; set; } = 0;
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

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{

			base.OnKeyPressed(sender, keyEventArgs);
			if (ID == 0)
			{
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

			}else if (ID == 1)
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
			if (ID == 0)
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
			else if (ID == 1)
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