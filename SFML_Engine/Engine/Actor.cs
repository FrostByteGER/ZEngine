using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace SFML_Engine.Engine
{
	public class Actor : Transformable, IActorable
	{
		public uint ID { get; private set; } = 0;
		public string ActorName { get; set; } = "Actor";
		public CollisionShape CollisionShape { get; set; }
		public bool Movable { get; set; } = true;
		public Vector2f Velocity { get; set; }
		public float MaxVelocity { get; set; } = -1f;
		public Vector2f Acceleration { get; set; }
		public float MaxAcceleration { get; set; } = .1f;
		public float Mass { get; set; } = 0.0f;

		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();

		public Actor()
		{
		}

		public Actor(Transformable transformable) : base(transformable)
		{
		}

		protected Actor(IntPtr cPointer) : base(cPointer)
		{
		}

		public virtual void Move(float x, float y)
		{
			Position = new Vector2f(x, y);
		}

		public virtual void Move(Vector2f position)
		{
			Position = position;
		}

		public virtual void Tick(float deltaTime)
		{
			foreach (var component in Components)
			{
				component.Tick(deltaTime);
			}
		}

		public void AfterCollision(Actor actor)
		{
			Console.WriteLine(">>>BEFORE COLLISION: " + ActorName + " WITH " + actor.ActorName + " <<<");
		}
		public void BeforeCollision(Actor actor)
		{
			Console.WriteLine(">>>AFTER COLLISION: " + ActorName + " WITH " + actor.ActorName + " <<<");
		}
		public void IsOverlaping(Actor actor)
		{
			Console.WriteLine(">>>OVERLAPPING: " + ActorName + " WITH " + actor.ActorName + " <<<");
		}
	}
}