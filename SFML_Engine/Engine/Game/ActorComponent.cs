using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

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
		public bool CanTick { get; set; } = true;

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

		public virtual TVector2f Position
		{
			get => IsRootComponent ? (TVector2f)Transform.Position : ParentActor.Position + Transform.Position;
			set => Transform.Position = value;
		}

		public virtual TVector2f LocalPosition
		{
			get => Transform.Position;
			set => Transform.Position = value;
		}

		public virtual float Rotation
		{
			get => IsRootComponent ? Transform.Rotation : ParentActor.Rotation + Transform.Rotation;
			set => Transform.Rotation = value;
		}

		public virtual float LocalRotation
		{
			get => Transform.Rotation;
			set => Transform.Rotation = value;
		}

		public virtual TVector2f Scale
		{
			get => IsRootComponent ? (TVector2f)Transform.Scale : new TVector2f(ParentActor.Scale.X * Transform.Scale.X, ParentActor.Scale.Y * Transform.Scale.Y);
			set => Transform.Scale = value;
		}

		public virtual TVector2f LocalScale
		{
			get => Transform.Scale;
			set => Transform.Scale = value;
		}

		public virtual TVector2f Origin
		{
			get => IsRootComponent ? (TVector2f)Transform.Origin : ParentActor.Origin;
			set => Transform.Origin = value;
		}

		public virtual TVector2f LocalOrigin
		{
			get => Transform.Origin;
			set => Transform.Origin = value;
		}

		private TVector2f componentBounds;
		public virtual TVector2f ComponentBounds
		{
			get => IsRootComponent ? componentBounds : ParentActor.ActorBounds;
			set => componentBounds = value;
		}

		public Transformable ComponentTransform { get; set; } = new Transformable();

		public bool Movable { get; set; }

		public virtual void OnActorComponentDestroy()
		{
			Console.WriteLine("DESTROYING ACTORCOMPONENT: " + ComponentName + "-" + ComponentID);
		}

		public virtual void Move(float x, float y)
		{
			LocalPosition += new TVector2f(x, y);
		}

		public void MoveAbsolute(float x, float y)
		{
			Position = new TVector2f(x, y);
		}

		public virtual void Move(TVector2f position)
		{
			LocalPosition += position;
		}

		public void MoveAbsolute(TVector2f position)
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
			Scale += new TVector2f(x, y);
		}

		public void ScaleActor(TVector2f scale)
		{
			Scale += scale;
		}

		public void ScaleAbsolute(float x, float y)
		{
			Scale = new TVector2f(x, y);
		}

		public void ScaleAbsolute(TVector2f scale)
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

		public override string ToString()
		{
			return ComponentName + "-" + ComponentID;
		}
	}
}