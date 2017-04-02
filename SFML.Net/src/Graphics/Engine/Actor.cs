using System;
using System.Collections.Generic;
using SFML.System;

namespace SFML.Graphics.Engine
{
	public class Actor : Transformable, IActorable
	{

		public CollisionShape CollisionShape { get; set; }
		public bool Movable { get; set; }
		public Vector2f Velocity { get; set; }
		public Vector2f Acceleration { get; set; }
		public float Mass { get; set; }

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
		}

		public virtual void Move(Vector2f position)
		{
		}

		public virtual void Tick(float deltaTime)
		{
			foreach (var component in Components)
			{
				component.Tick(deltaTime);
			}
		}
	}
}