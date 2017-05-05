using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;

namespace SFML_Engine.Engine
{
	public class ActorComponent : ITickable, ITransformable, Drawable
	{

		public uint ComponentID { get; internal set; } = 0;
		public string ComponentName { get; set; } = "Component";
		public Actor ParentActor { get; set; } = null;
		public bool IsRootComponent { get; internal set; } = false;


		//TODO: Implement
		public ActorComponent ParentComponent { get; set; } = null;

		//TODO: Implement
		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();

		public Transformable Transform { get; set; }

		public virtual void Tick(float deltaTime)
		{
			//Console.WriteLine("Component Tick | Position: " + Position + " Rotation: " + Rotation + " Scale: " + Scale);
		}

		public void SwapParentActor(Actor newParent)
		{
			if (ParentActor == null || newParent == null) return;
			if (IsRootComponent)
			{
				ParentActor.RemoveRootComponent();
			}
			else
			{
				ParentActor.RemoveComponent(this);
			}
			newParent.AddComponent(this);
		}

		public Vector2f Position
		{
			get
			{
				if (IsRootComponent)
				{
					return Transform.Position;
				}
				return ParentActor.Position + Transform.Position;
			}
			set { Transform.Position = value; }
		}

		public float Rotation
		{
			get
			{
				if (IsRootComponent)
				{
					return Transform.Rotation;
				}
				return ParentActor.Rotation + Transform.Rotation;
			}
			set { Transform.Rotation = value; }
		}

		public Vector2f Scale
		{
			get
			{
				if (IsRootComponent)
				{
					return Transform.Scale;
				}
				return new Vector2f(ParentActor.Scale.X * Transform.Scale.X, ParentActor.Scale.Y * Transform.Scale.Y);
			}
			set { Transform.Scale = value; }
		}

		public Transformable ComponentTransform { get; set; } = new Transformable();

		public bool Movable { get; set; }

		public virtual void OnActorComponentDestroy()
		{
			Console.WriteLine("DESTROYING ACTORCOMPONENT: " + ComponentName + "-" + ComponentID);
		}

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

		public virtual void Draw(RenderTarget target, RenderStates states)
		{
			
		}
	}
}