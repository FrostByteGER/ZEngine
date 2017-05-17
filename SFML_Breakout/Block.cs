using System;
using BulletSharp;
using SFML.Graphics;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
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

		public Block(SpriteComponent spriteComp) : base(spriteComp)
		{
		}

		public Block(Sprite sprite) : base(sprite)
		{
		}

		public Block(Texture t) : base(t)
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
				var component = RootComponent as CollisionComponent;
				if (Hitpoints == 0)
				{
					//TODO

					PowerUp pow;

					if (EngineMath.EngineRandom.NextDouble() > 0.5)
					{
						pow = new PowerUpDup();
					}
					else
					{
						pow = new PowerUpInc();
					}

					//PowerUpDup pow = new PowerUpDup();

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