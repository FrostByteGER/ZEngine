using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SFML_Engine.Engine
{
	public class Actor : Transformable, IActorable, IGameInterface
	{

		public uint ActorID { get; internal set; } = 0;
		public uint LevelID { get; internal set; } = 0;
		public Level LevelReference { get; internal set; }
		public string ActorName { get; set; } = "Actor";
		public CollisionShape CollisionShape { get; set; } = new CollisionShape();
		public FloatRect ActorBounds { get; set; } = new FloatRect();
		public bool Movable { get; set; } = true;
		public Vector2f Velocity { get; set; }
		public float MaxVelocity { get; set; } = -1.0f;
		public Vector2f Acceleration { get; set; }
		public float MaxAcceleration { get; set; } = -1f;

		public float Friction = 0.0f;
		public float Mass { get; set; } = 1.0f;
		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();
		public bool HasGravity { get; set; } = false;

		public bool MarkedForRemoval { get; internal set; } = false;
		public bool Visible { get; set; } = true;

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
			Position += new Vector2f(x, y);
			CollisionShape.Move(new Vector2f(x, y));
		}

		public void MoveAbsolute(float x, float y)
		{
			Position = new Vector2f(x, y);
			CollisionShape.MoveAbsolute(new Vector2f(x, y));
		}

		public virtual void Move(Vector2f position)
		{
			Position += position;
			CollisionShape.Move(position);
		}

		public void MoveAbsolute(Vector2f position)
		{
			Position = position;
			CollisionShape.MoveAbsolute(position);
		}

		public void Rotate(float angle)
		{
			Rotation += angle;
			CollisionShape.Rotate(angle);
		}

		public void Rotate(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public void RotateAbsolute(float angle)
		{
			Rotation = angle;
			CollisionShape.RotateAbsolute(angle);
		}

		public void RotateAbsolute(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public void ScaleActor(float x, float y)
		{
			base.Scale += new Vector2f(x, y);
			CollisionShape.ScaleActor(new Vector2f(x, y));
		}

		public void ScaleActor(Vector2f scale)
		{
			base.Scale += scale;
			CollisionShape.ScaleActor(scale);
		}

		public void ScaleAbsolute(float x, float y)
		{
			base.Scale = new Vector2f(x, y);
			CollisionShape.ScaleAbsolute(new Vector2f(x, y));
		}

		public void ScaleAbsolute(Vector2f scale)
		{
			base.Scale = scale;
			CollisionShape.ScaleAbsolute(scale);
		}

		public virtual void Tick(float deltaTime)
		{
			foreach (var component in Components)
			{
				component.Tick(deltaTime);
			}
		}

		public virtual void AfterCollision(Actor actor)
		{
			Console.WriteLine(">>>AFTER COLLISION: " + ActorName + " WITH " + actor.ActorName + " <<<");
		}
		public virtual void BeforeCollision(Actor actor)
		{
			Console.WriteLine(">>>BEFORE COLLISION: " + ActorName + " WITH " + actor.ActorName + " <<<");
		}
		public virtual void IsOverlapping(Actor actor)
		{
			Console.WriteLine(">>>OVERLAPPING: " + ActorName + " WITH " + actor.ActorName + " <<<");
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Actor) obj);
		}

		protected bool Equals(Actor other)
		{
			return ActorID == other.ActorID;
		}

		public override int GetHashCode()
		{
			return (int) ActorID;
		}

		public virtual void OnGameStart()
		{
			
		}

		public virtual void OnGamePause()
		{
			
		}

		public virtual void OnGameEnd()
		{
			
		}

		public ActorInformation GenerateActorInformation()
		{
			return new ActorInformation(ActorID, LevelID, Position, Rotation, Scale, Origin, Movable, Velocity, MaxVelocity,
				Acceleration, MaxAcceleration, Mass, Friction, HasGravity);
		}

		public virtual void OnActorDestroy()
		{
			Console.WriteLine("DESTROYING ACTOR: " + ActorName + "-" + ActorID);
		}

		public string GenerateFullName()
		{
			return ActorName + "-" + ActorID;
		}
	}
}