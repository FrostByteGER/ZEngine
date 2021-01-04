using System;
using System.Numerics;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Game
{
	public class ActorComponent : ITickable, ITransformable, IDestroyable
	{
		public uint ComponentID { get; internal set; } = 0;
		public string ComponentName { get; set; } = "Component";
		public Actor ParentActor { get; internal set; } = null;
		public bool IsRootComponent { get; internal set; } = false;

		public Transform ComponentTransform { get; set; } = new();

		public Transform WorldTransform { get; } //=> IsRootComponent ? ComponentTransform : ParentActor.ActorTransform + ComponentTransform;

		public bool CanTick { get; set; } = true;

		public virtual Vector2 LocalPosition
		{
			get => ComponentTransform.Position;
			set => ComponentTransform.Position = value;
		}

		public virtual float LocalRotation
		{
			get => ComponentTransform.Rotation;
			set => ComponentTransform.Rotation = value;
		}

		public virtual Vector2 LocalScale
		{
			get => ComponentTransform.Scale;
			set => ComponentTransform.Scale = value;
		}

		public virtual Vector2 Origin
		{
			get => ComponentTransform.Origin;
			set => ComponentTransform.Origin = value;
		}

		public virtual Vector2 ComponentBounds { get; set; }

		public Vector2 WorldPosition
		{
			get => IsRootComponent ? (Vector2)ComponentTransform.Position : ParentActor.Position + ComponentTransform.Position;
            set => LocalPosition = new Vector2(); //ComponentTransform.InverseTransform * value;
        }

		public virtual bool Movable { get; set; }


		public ActorComponent()
		{
			ComponentName = GetType().Name;
		}

		public ActorComponent(string componentName)
		{
			ComponentName = componentName;
		}

		public virtual void Tick(float deltaTime)
		{
			//Debug.LogDebug("Component Tick | Position: " + Position + " Rotation: " + Rotation + " Scale: " + Scale, DebugLogCategories.Engine);
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

		protected internal virtual void OnInitializeActorComponent()
		{
			
		}

		public virtual void OnActorComponentDestroy()
		{
			Debug.Log("Destroying Actor-Component: " + ComponentName + "-" + ComponentID, DebugLogCategories.Engine);
		}

		public virtual void MoveLocal(float x, float y)
		{
			LocalPosition += new Vector2(x, y);
		}

		public void MoveWorld(Vector2 position)
		{
			throw new NotImplementedException();
		}

		public void SetLocalPosition(float x, float y)
		{
			LocalPosition = new Vector2(x, y);
		}

		public virtual void MoveLocal(Vector2 position)
		{
			LocalPosition += position;
		}

		public void MoveWorld(float x, float y)
		{
			WorldPosition += new Vector2(x, y);
		}

		public void SetLocalPosition(Vector2 position)
		{
			LocalPosition = position;
		}

		public void SetWorldPosition(float x, float y)
		{
			WorldPosition = new Vector2(x, y);
		}

		public void SetWorldPosition(Vector2 position)
		{
			WorldPosition = position;
		}

		public void RotateLocal(float angle)
		{
			LocalRotation += angle;
		}

		public void RotateWorld(float angle)
		{
			throw new NotImplementedException();
		}

		public void SetLocalRotation(float angle)
		{
			LocalRotation = angle;
		}

		public void SetWorldRotation(float angle)
		{
			throw new NotImplementedException();
		}

		public void ScaleLocal(float x, float y)
		{
			LocalScale += new Vector2(x, y);
		}

		public void ScaleLocal(Vector2 scale)
		{
			LocalScale += scale;
		}

		public void ScaleWorld(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void ScaleWorld(Vector2 scale)
		{
			throw new NotImplementedException();
		}

		public void SetLocalScale(float x, float y)
		{
			LocalScale = new Vector2(x, y);
		}

		public void SetLocalScale(Vector2 scale)
		{
			LocalScale = scale;
		}

		public void SetWorldScale(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void SetWorldScale(Vector2 scale)
		{
			throw new NotImplementedException();
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
			//ComponentTransform.Dispose();
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
			if (obj is null) return false;
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