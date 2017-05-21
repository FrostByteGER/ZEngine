using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
	public class ActorComponent : ITickable, ITransformable, IDestroyable
	{
		public uint ComponentID { get; internal set; } = 0;
		public string ComponentName { get; set; } = "Component";
		public Actor ParentActor { get; set; } = null;
		public bool IsRootComponent { get; internal set; } = false;

		public Transformable ComponentTransform { get; set; } = new Transformable();
		public bool CanTick { get; set; } = true;

		public virtual TVector2f Position
		{
			get => IsRootComponent ? (TVector2f)ComponentTransform.Position : ParentActor.Position + ComponentTransform.Position;
			set => ComponentTransform.Position = value;
		}

		public virtual float Rotation
		{
			get => IsRootComponent ? ComponentTransform.Rotation : ParentActor.Rotation + ComponentTransform.Rotation;
			set => ComponentTransform.Rotation = value;
		}

		public virtual TVector2f Scale
		{
			get => IsRootComponent ? (TVector2f)ComponentTransform.Scale : new TVector2f(ParentActor.Scale.X * ComponentTransform.Scale.X, ParentActor.Scale.Y * ComponentTransform.Scale.Y);
			set => ComponentTransform.Scale = value;
		}

		public virtual TVector2f Origin
		{
			get => IsRootComponent ? (TVector2f)ComponentTransform.Origin : ParentActor.Origin;
			set => ComponentTransform.Origin = value;
		}

		protected TVector2f _componentBounds;
		public virtual TVector2f ComponentBounds
		{
			get => IsRootComponent ? _componentBounds : ParentActor.ActorBounds;
			set => _componentBounds = value;
		}

		public bool Movable { get; set; }

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

		public virtual void OnActorComponentDestroy()
		{
			Console.WriteLine("DESTROYING ACTORCOMPONENT: " + ComponentName + "-" + ComponentID);
		}

		public virtual void Move(float x, float y)
		{
			Position += new TVector2f(x, y);
		}

		public void SetPosition(float x, float y)
		{
			Position = new TVector2f(x, y);
		}

		public virtual void Move(TVector2f position)
		{
			Position += position;
		}

		public void SetPosition(TVector2f position)
		{
			Position = position;
		}

		public void Rotate(float angle)
		{
			Rotation += angle;
		}

		public void SetRotation(float angle)
		{
			Rotation = angle;
		}

		public void ScaleActor(float x, float y)
		{
			Scale += new TVector2f(x, y);
		}

		public void ScaleActor(TVector2f scale)
		{
			Scale += scale;
		}

		public void SetScale(float x, float y)
		{
			Scale = new TVector2f(x, y);
		}

		public void SetScale(TVector2f scale)
		{
			Scale = scale;
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
			ComponentTransform.Dispose();
		}

		public override string ToString()
		{
			return ParentActor + "|" + ComponentName + "-" + ComponentID;
		}

		protected bool Equals(ActorComponent other)
		{
			return ComponentID == other.ComponentID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ActorComponent) obj);
		}

		public override int GetHashCode()
		{
			return (int) ComponentID;
		}

		public static bool operator ==(ActorComponent left, ActorComponent right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ActorComponent left, ActorComponent right)
		{
			return !Equals(left, right);
		}
	}
}