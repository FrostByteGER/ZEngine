using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;

namespace SFML_Engine.Engine
{
	public class ActorComponent : Transformable, ITickable, ITransformable
	{
		public Actor ParentActor { get; set; } = null;

		//TODO: Implement
		public ActorComponent ParentComponent { get; set; } = null;

		//TODO: Implement
		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();
		public virtual void Tick(float deltaTime)
		{
			//Console.WriteLine("Component Tick | Position: " + Position + " Rotation: " + Rotation + " Scale: " + Scale);
		}

		public void SwapParentActor(Actor newParent)
		{
			if (ParentActor == null || newParent == null) return;
			ParentActor.RemoveComponent(this);
			newParent.AddComponent(this);
		}

		public override Vector2f Position
		{
			get { return ParentActor.Position + base.Position; }
			set { base.Position = value; }
		}

		public override float Rotation
		{
			get { return ParentActor.Rotation + base.Rotation; }
			set { base.Rotation = value; }
		}

		public override Vector2f Scale
		{
			get { return new Vector2f(ParentActor.Scale.X * base.Scale.X, ParentActor.Scale.Y * base.Scale.Y); }
			set { base.Scale = value; }
		}

		public Transformable ComponentTransform { get; set; } = new Transformable();

		public bool Movable { get; set; }

		public virtual void Move(float x, float y)
		{
			Position += new Vector2f(x, y);
		}

		public void MoveAbsolute(float x, float y)
		{
			Position = new Vector2f(x, y);
		}

		public virtual void Move(Vector2f position)
		{
			Position += position;
		}

		public void MoveAbsolute(Vector2f position)
		{
			Position = position;
		}

		public void Rotate(float angle)
		{
			Rotation += angle;
		}

		public void Rotate(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public void RotateAbsolute(float angle)
		{
			Rotation = angle;
		}

		public void RotateAbsolute(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public void ScaleActor(float x, float y)
		{
			Scale += new Vector2f(x, y);
		}

		public void ScaleActor(Vector2f scale)
		{
			Scale += scale;
		}

		public void ScaleAbsolute(float x, float y)
		{
			Scale = new Vector2f(x, y);
		}

		public void ScaleAbsolute(Vector2f scale)
		{
			Scale = scale;
		}
	}
}