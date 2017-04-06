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

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyPressed(sender, keyEventArgs);
			if (ID == 0)
			{
				if (Input.APressed)
				{
					//PlayerPawn.Position += new Vector2f(-10.0f, 0.0f);
					PlayerPawn.Velocity += new Vector2f(-10.0f, 0);

				}

				if (Input.DPressed)
				{
					//PlayerPawn.Position += new Vector2f(10.0f, 0.0f);
					PlayerPawn.Velocity += new Vector2f(10.0f, 0);

				}
				if (Input.WPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, -10.0f);
					PlayerPawn.Velocity += new Vector2f(0.0f, -10.0f);
				}

				if (Input.SPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, 10.0f);
					PlayerPawn.Velocity += new Vector2f(0.0f, 10.0f);

				}

				if (Input.QPressed)
				{
					PlayerPawn.Rotation -= 10.0f;

				}

				if (Input.EPressed)
				{
					PlayerPawn.Rotation += 10.0f;

				}
			}else if (ID == 1)
			{
				if (Input.LeftPressed)
				{
					//PlayerPawn.Position += new Vector2f(-10.0f, 0.0f);
					PlayerPawn.Velocity += new Vector2f(-10.0f, 0);

				}

				if (Input.RightPressed)
				{
					//PlayerPawn.Position += new Vector2f(10.0f, 0.0f);
					PlayerPawn.Velocity += new Vector2f(10.0f, 0);

				}

				if (Input.UpPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, -10.0f);
					PlayerPawn.Velocity += new Vector2f(0.0f, -10.0f);
				}

				if (Input.DownPressed)
				{
					//PlayerPawn.Position += new Vector2f(0.0f, -10.0f);
					PlayerPawn.Velocity += new Vector2f(0.0f, 10.0f);
				}

				if (Input.OPressed)
				{
					PlayerPawn.Rotation -= 10.0f;

				}

				if (Input.PPressed)
				{
					PlayerPawn.Rotation += 10.0f;

				}
			}
		}
			

		protected override void OnKeyReleased(object sender, KeyEventArgs keyEventArgs)
		{
			base.OnKeyReleased(sender, keyEventArgs);
		}


	}
}