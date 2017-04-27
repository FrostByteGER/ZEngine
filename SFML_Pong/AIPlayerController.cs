using SFML.System;
using SFML.Window;
using SFML_Engine.Engine;
using System;

namespace SFML_Pong
{
	public class AIPlayerController : PongPlayerController
	{

		float difficulty { set; get; } = 0.1f;

		float wait = 0;

		uint PadID = 2;

		PongBall ball { set; get; }
		SpriteActor pad { set; get; }

		Vector2f point;

		public AIPlayerController(SpriteActor playerPawn) : base(playerPawn)
		{
			pad = playerPawn;
			

			
		}
 
		public override void OnGameStart()
		{
			base.OnGameStart();
			ball = (PongBall)LevelReference.FindActorInLevel("Ball");
			point = ((SFML_Engine.Engine.Physics.SphereShape)ball.CollisionShape).GetMid(ball.Position);
		}

			

		protected override void OnKeyPressed(object sender, KeyEventArgs keyEventArgs)
		{
			//base.OnKeyPressed(sender, keyEventArgs);

		}



		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			//Console.WriteLine(wait+" "+ ball+" " + LevelReference);

			if (point.Y < ((SFML_Engine.Engine.Physics.BoxShape)pad.CollisionShape).GetMid(pad.Position).Y)
			{
				// Down
				PlayerPawn.Acceleration = new Vector2f(0.0f, -500.0f);
			}
			else if (point.Y > ((SFML_Engine.Engine.Physics.BoxShape)pad.CollisionShape).GetMid(pad.Position).Y)
			{
				// UP
				PlayerPawn.Acceleration = new Vector2f(0.0f, 500.0f);
			}

			if (wait > difficulty && ball != null)
			{
				point = ((SFML_Engine.Engine.Physics.SphereShape)ball.CollisionShape).GetMid(ball.Position);
				wait = 0;
				Console.WriteLine(point);
			}

			wait += deltaTime;

		}
	}
}
