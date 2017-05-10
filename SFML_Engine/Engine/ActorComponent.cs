using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;

namespace SFML_Engine.Engine
{
	public class ActorComponent : ITickable, ITransformable, Drawable, IDestroyable
	{

		public uint ComponentID { get; internal set; } = 0;
		public string ComponentName { get; set; } = "Component";
		public Actor ParentActor { get; set; } = null;
		public bool IsRootComponent { get; internal set; } = false;


		//TODO: Implement
		public ActorComponent ParentComponent { get; set; } = null;

		//TODO: Implement
		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();

		public Transformable Transform { get; set; } = new Transformable();

		public bool Visible { get; set; } = true;

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
			get => IsRootComponent ? Transform.Position : ParentActor.Position + Transform.Position;
			set => Transform.Position = value;
		}

		public Vector2f LocalPosition
		{
			get => Transform.Position;
			set => Transform.Position = value;
		}

		public float Rotation
		{
			get => IsRootComponent ? Transform.Rotation : ParentActor.Rotation + Transform.Rotation;
			set => Transform.Rotation = value;
		}

		public float LocalRotation
		{
			get => Transform.Rotation;
			set => Transform.Rotation = value;
		}

		public Vector2f Scale
		{
			get => IsRootComponent ? Transform.Scale : new Vector2f(ParentActor.Scale.X * Transform.Scale.X, ParentActor.Scale.Y * Transform.Scale.Y);
			set => Transform.Scale = value;
		}

		public Vector2f LocalScale
		{
			get => Transform.Scale;
			set => Transform.Scale = value;
		}

		public Vector2f Origin
		{
			get => IsRootComponent ? Transform.Origin : ParentActor.Origin;
			set => Transform.Origin = value;
		}

		public Vector2f LocalOrigin
		{
			get => Transform.Origin;
			set => Transform.Origin = value;
		}

		public Transformable ComponentTransform { get; set; } = new Transformable();

		public bool Movable { get; set; }

		public virtual void OnActorComponentDestroy()
		{
			Console.WriteLine("DESTROYING ACTORCOMPONENT: " + ComponentName + "-" + ComponentID);
		}

		public virtual void Move(float x, float y)
		{
			LocalPosition += new Vector2f(x, y);
		}

		public void MoveAbsolute(float x, float y)
		{
			Position = new Vector2f(x, y);
		}

		public virtual void Move(Vector2f position)
		{
			LocalPosition += position;
		}

		public void MoveAbsolute(Vector2f position)
		{
			Position = position;
		}

		public void Rotate(float angle)
		{
			LocalRotation += angle;
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

		private void Dispose(bool disposing)
		{
			Destroy(disposing);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual void Destroy(bool disposing)
		{
			Transform.Dispose();
			foreach (var comp in Components)
			{
				comp.Dispose();
			}
		}
	}
}