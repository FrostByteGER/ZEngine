using System;
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
					
					//PlayerPawn.Position += new Vector2f(0.0f, -10.0f);
					//float velocityY = PlayerPawn.Velocity.Y -10.0f;
					//velocityY = Math.Abs(velocityY).Clamp(0.0f, PlayerPawn.MaxVelocity);
					//Console.WriteLine(velocityY);
					PlayerPawn.Velocity += new Vector2f(0.0f, -10.0f);
				}

				if (Input.SPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, 10.0f);
					//float velocityY = PlayerPawn.Velocity.Y + 10.0f;
					//velocityY = Math.Abs(velocityY).Clamp(0.0f, PlayerPawn.MaxVelocity);
					//Console.WriteLine(velocityY);
					PlayerPawn.Velocity += new Vector2f(0.0f, 10.0f);

				}
			}else if (ID == 1)
			{
				if (Input.UpPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, -10.0f);
					if (Math.Abs(PlayerPawn.Velocity.Y) < PlayerPawn.MaxVelocity)
					{
						PlayerPawn.Velocity += new Vector2f(0.0f, -10.0f);
					}
				}

				if (Input.DownPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, -10.0f);
					if (Math.Abs(PlayerPawn.Velocity.Y) < PlayerPawn.MaxVelocity)
					{
						PlayerPawn.Velocity += new Vector2f(0.0f, 10.0f);
					}
				}
			}
			Console.WriteLine(PlayerPawn.ActorName+" "+PlayerPawn.Velocity);
		}
	}
}