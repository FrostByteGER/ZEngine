using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Graphics;
using Sprite = SFML_Engine.Engine.Graphics.Sprite;

namespace SFML_Pong
{
	public class PongBall : SpriteActor
	{
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
				if (player != null) ++player.Score;
			}else if (actor.ActorName == "Right Border")
			{
				PongPlayerController player = engine.Players[0] as PongPlayerController;
				if (player != null) ++player.Score;
			}
			else
			{
				if (actor.GetType() == typeof(PowerUP))
				{
					actor.IsOverlapping(this);
				}
				return;
			}
			//engine.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));

			Position = new Vector2f(engine.EngineWindowWidth/2.0f, engine.EngineWindowHeight/2.0f);
			Velocity = new Vector2f(EngineMath.EngineRandom.Next((int)MaxVelocity/2, (int) MaxVelocity), EngineMath.EngineRandom.Next((int)MaxVelocity / 2,(int)MaxVelocity));
		}
	}
}
