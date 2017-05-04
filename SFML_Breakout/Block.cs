using System;
using SFML.Graphics;
using SFML_Engine.Engine;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Utility;
using Sprite = SFML_Engine.Engine.SFML.Graphics.Sprite;

namespace SFML_Breakout
{
	public class Block : SpriteActor
	{

		public uint Hitpoints { get; set; } = 1;
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
			base.AfterCollision(actor);

			Console.WriteLine("COLLISION WITH: " + actor.ActorName);
			if (actor is BreakoutBall)
			{
				CollisionShape.CollisionShapeColor = new Color((byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255), (byte)EngineMath.EngineRandom.Next(255));
				if (Invincible) return;
				--Hitpoints;
				((BreakoutBall) actor).Score += Score;
				if (Hitpoints == 0) LevelReference.EngineReference.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, this)));
				Console.WriteLine("New HP of " + ActorName +  ": " + Hitpoints);
			}
		}

		public override void BeforeCollision(Actor actor)
		{
			base.BeforeCollision(actor);
		}

		public override void IsOverlapping(Actor actor)
		{
			base.IsOverlapping(actor);
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