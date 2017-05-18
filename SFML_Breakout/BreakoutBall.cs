using System;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Breakout
{
	public class BreakoutBall : SpriteActor
	{

		public Actor LastPlayerCollision { get; set; }
		public BreakoutGameMode GameModeReference { get; set; }

		public BreakoutPlayerController PCReference { get; set; }

		public BreakoutBall()
		{
		}

		public BreakoutBall(Texture texture) : base(texture)
		{
		}

		public BreakoutBall(Texture texture, IntRect rectangle) : base(texture, rectangle)
		{
		}

		public BreakoutBall(Sprite copy) : base(copy)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
			ScaleAbsolute(1.0f, 1.0f);
			MaxVelocity = 500.0f;
			GameModeReference = (BreakoutGameMode)LevelReference.GameMode;
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
			Position = new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f, LevelReference.EngineReference.EngineWindowHeight / 2.0f);
			Velocity = new Vector2f();
			Acceleration = new Vector2f();
		}

		public void RespawnBall()
		{
			MoveAbsolute(new Vector2f(LevelReference.EngineReference.EngineWindowWidth / 2.0f - CollisionShape.CollisionBounds.X / 2.0f, LevelReference.EngineReference.EngineWindowHeight - CollisionShape.CollisionBounds.X * 4.0f));
			Velocity = new Vector2f(0.0f, -250.0f);
		}

		public override void AfterCollision(Actor actor)
		{
			Console.WriteLine("COLLISION WITH: " + actor.ActorName);
			if (actor.ActorName == "Player Pad 1")
			{
				if (this.CollisionShape.GetType() == typeof(SphereShape) && actor.CollisionShape.GetType() == typeof(BoxShape))
				{
					SphereShape cs = (SphereShape)this.CollisionShape;
					BoxShape bc = (BoxShape)actor.CollisionShape;

					Vector2f norm = cs.GetMid(this.Position) - bc.GetMid(actor.Position) - new Vector2f(0, 50);

					norm = new Vector2f(norm.X / (Math.Abs(norm.X) + Math.Abs(norm.Y)), norm.Y / (Math.Abs(norm.X) + Math.Abs(norm.Y)));

					this.Velocity = new Vector2f(norm.X * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)), norm.Y * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)));
					this.Acceleration = new Vector2f(norm.X * Math.Abs(Acceleration.X + Acceleration.Y), norm.Y * Math.Abs(Acceleration.X + Acceleration.Y));

				}
				if (LastPlayerCollision == null)
				{

				}
				else if (!LastPlayerCollision.Equals(actor))
				{
					this.Velocity = new Vector2f(Velocity.X * 1.05f, Velocity.Y * 1.01f);
					this.MaxVelocity *= 1.01f;
				}
				LastPlayerCollision = actor;
			}else if (actor.ActorName == "Bottom Border")
			{
				RespawnBall();
			}
		}

		public override void BeforeCollision(Actor actor)
		{
			
		}

		public override void IsOverlapping(Actor actor)
		{



		}
	}
}