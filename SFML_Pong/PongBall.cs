using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Pong
{
	public class PongBall : SpriteActor
	{

		public PongGameMode GameModeReference { get; private set; }
		public Actor LastPlayerCollision { get; set; }
		public PongBall()
		{
		}

		public PongBall(Texture texture) : base(texture)
		{
		}

		public PongBall(Texture texture, IntRect rectangle) : base(texture, rectangle)
		{
		}

		public PongBall(Sprite copy) : base(copy)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			//Rotation += 25.0f * deltaTime;

		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			GameModeReference = (PongGameMode) LevelReference.GameMode;
		}

		public override void AfterCollision(Actor actor)
		{
			base.AfterCollision(actor);
			if (actor.ActorName == "Left Pad" || actor.ActorName == "Right Pad")
			{
				LastPlayerCollision = actor;
				Console.WriteLine("COLLISION WITH: " + actor.ActorName);
			}
		}

		public override void BeforeCollision(Actor actor)
		{
			base.BeforeCollision(actor);
		}

		public override void IsOverlapping(Actor actor)
		{
			var engine = Engine.Instance;
			if (actor.ActorName == "Left Border")
			{
				PongPlayerController player = engine.Players[1] as PongPlayerController;
				if (player != null)
				{
					Console.WriteLine("Score for Player 2!!!");
					GameModeReference.OnPlayerScore(player, 1);
					GameModeReference.SpawnBall();
				}
				
			}else if (actor.ActorName == "Right Border")
			{
				PongPlayerController player = engine.Players[0] as PongPlayerController;
				if (player != null)
				{
					Console.WriteLine("Score for Player 1!!!");
					GameModeReference.OnPlayerScore(player, 1);
					GameModeReference.SpawnBall();
				}
				
			}
			else
			{
				if (actor.GetType() == typeof(PowerUp))
				{
					actor.IsOverlapping(this);
				}
			}
		}
	}
}
