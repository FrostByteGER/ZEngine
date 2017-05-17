using System;
using BulletSharp;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	public class BreakoutBall : SpriteActor
	{
		public uint Score { get; set; } = 0;
		public Actor LastPlayerCollision { get; set; }
		public BreakoutGameMode GameModeReference { get; set; }

		public BreakoutBall()
		{
		}

		public BreakoutBall(SpriteComponent spriteComp) : base(spriteComp)
		{
		}

		public BreakoutBall(Sprite sprite) : base(sprite)
		{
		}

		public BreakoutBall(Texture t) : base(t)
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
			var collisionComp = RootComponent as CollisionComponent;
			if (collisionComp != null)
				MoveAbsolute(new Vector2f(
					LevelReference.EngineReference.EngineWindowWidth / 2.0f - collisionComp.CollisionBounds.X / 2.0f,
					LevelReference.EngineReference.EngineWindowHeight - collisionComp.CollisionBounds.X * 4.0f));
			Velocity = new Vector2f(0.0f, -250.0f);
		}

		public override void AfterCollision(Actor actor)
		{
			Console.WriteLine("COLLISION WITH: " + actor.ActorName);
			if (actor.ActorName == "Player Pad 1")
			{
				Vector2f norm = Position + Origin - actor.Position + actor.Origin;
				norm = new Vector2f(norm.X / (Math.Abs(norm.X) + Math.Abs(norm.Y)), norm.Y / (Math.Abs(norm.X) + Math.Abs(norm.Y)));
				this.Velocity = new Vector2f(norm.X * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)), norm.Y * (Math.Abs(Velocity.X) + Math.Abs(Velocity.Y)));
				this.Acceleration = new Vector2f(norm.X * Math.Abs(Acceleration.X + Acceleration.Y), norm.Y * Math.Abs(Acceleration.X + Acceleration.Y));
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