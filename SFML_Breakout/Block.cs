using System;
using SFML.Graphics;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Utility;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;
using SFML_Engine.Engine.Physics;

namespace SFML_Breakout
{
	public class Block : SpriteActor
	{

		public uint Hitpoints { get; set; } = 1;
		public uint MaxHitpoints { get; set; } = 1;
		public bool Invincible { get; set; } = false;
		public uint Score { get; set; } = 1;

		public Block()
		{
		}

		public Block(Texture texture) : base(texture)
		{
		}

		public Block(Texture texture, IntRect rectangle) : base(texture, rectangle)
		{
		}

		public Block(Sprite copy) : base(copy)
		{
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
		}

		public override void AfterCollision(Actor actor)
		{
			Console.WriteLine("COLLISION WITH: " + actor.ActorName);
			if (actor is BreakoutBall)
			{
				if (Invincible) return;

				--Hitpoints;
				((BreakoutBall) actor).Score += Score;
				Console.WriteLine("Player Score: " + ((BreakoutBall) actor).Score);
				var newAlpha = Math.Max(0.0f, Math.Min(1.0f, (float)Hitpoints / MaxHitpoints));
				CollisionShape.CollisionShapeColor = new Color(CollisionShape.CollisionShapeColor.R, CollisionShape.CollisionShapeColor.G, CollisionShape.CollisionShapeColor.B, (byte)Math.Floor(newAlpha == 1.0f ? 255 : newAlpha * 256.0f));
				if (Hitpoints == 0)
				{
					//TODO

					PowerUp pow = ((BreakoutGameMode)LevelReference.GameMode).GetRandomPowerUp();

					//double choose = EngineMath.EngineRandom.NextDouble();

					/*
					if (choose < 0.3)
					{
						pow = new PowerUpDup();
					}
					else if(choose < 0.6)
					{
						pow = new PowerUpPadSizeInc();
					}
					else
					{
						pow = new PowerUpDec();
					}
					*/
					//PowerUpDup pow = new PowerUpDup();

					pow.Position = ((BoxShape)CollisionShape).GetMid(Position);

					pow.CollisionShape.ShowCollisionShape = true;

					LevelReference.EngineReference.PhysicsEngine.AddActorToGroup("PowerUp", pow);

					LevelReference.EngineReference.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, pow, LevelReference.LevelID)));
					LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
				}
			}
		}

		public override void BeforeCollision(Actor actor)
		{
		}

		public override void IsOverlapping(Actor actor)
		{
		}

		public override void OnGameStart()
		{
			base.OnGameStart();
		}

		public override void OnGameEnd()
		{
			base.OnGameEnd();
		}
	}
}