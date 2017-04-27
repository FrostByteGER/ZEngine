using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Utility;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;
using SFML_Engine.Engine.Physics;

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

				if (this.CollisionShape.GetType() == typeof(SphereShape) && actor.CollisionShape.GetType() == typeof(BoxShape))
				{
					SphereShape cs = (SphereShape)this.CollisionShape;
					BoxShape bc = (BoxShape)actor.CollisionShape;

					Vector2f norm = cs.GetMid(this.Position) - bc.GetMid(actor.Position);

					norm = new Vector2f(norm.X/(Math.Abs(norm.X) + Math.Abs(norm.Y)), norm.Y/ (Math.Abs(norm.X) + Math.Abs(norm.Y)));

					this.Velocity = new Vector2f(norm.X * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)), norm.Y * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)));
					this.Acceleration = new Vector2f(norm.X * Math.Abs(Acceleration.X + Acceleration.Y), norm.Y * Math.Abs(Acceleration.X + Acceleration.Y));

				}
				



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
					++player.Score;
					Console.WriteLine("Score for Player 2!!!");
				}
			}else if (actor.ActorName == "Right Border")
			{
				PongPlayerController player = engine.Players[0] as PongPlayerController;
				if (player != null)
				{
					Console.WriteLine("Score for Player 1!!!");
					++player.Score;
				}
			}
			else
			{
				if (actor.GetType() == typeof(PowerUp))
				{
					actor.IsOverlapping(this);
				}
				return;
			}

			Position = new Vector2f(engine.EngineWindowWidth/2.0f, engine.EngineWindowHeight/2.0f);
			Velocity = new Vector2f(EngineMath.EngineRandom.Next(-(int)MaxVelocity, (int) MaxVelocity), EngineMath.EngineRandom.Next(-(int)MaxVelocity,(int)MaxVelocity));
		}
	}
}
