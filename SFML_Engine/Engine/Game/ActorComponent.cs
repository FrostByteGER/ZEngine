using System;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
	public class ActorComponent : ITickable, ITransformable, IDestroyable
	{
		public uint ComponentID { get; internal set; } = 0;
		public string ComponentName { get; set; } = "Component";
		public Actor ParentActor { get; internal set; } = null;
		public bool IsRootComponent { get; internal set; } = false;

		public TTransformable ComponentTransform { get; set; } = new TTransformable();

		public TTransformable WorldTransform => IsRootComponent ? ComponentTransform : ParentActor.ActorTransform + ComponentTransform;

		public bool CanTick { get; set; } = true;

		public virtual TVector2f LocalPosition
		{
			get => ComponentTransform.Position;
			set => ComponentTransform.Position = value;
		}

		public virtual float LocalRotation
		{
			get => ComponentTransform.Rotation;
			set => ComponentTransform.Rotation = value;
		}

		public virtual TVector2f LocalScale
		{
			get => ComponentTransform.Scale;
			set => ComponentTransform.Scale = value;
		}

		public virtual TVector2f Origin
		{
			get => ComponentTransform.Origin;
			set => ComponentTransform.Origin = value;
		}

		public virtual TVector2f ComponentBounds { get; set; }

		public TVector2f WorldPosition
		{
			get => IsRootComponent ? (TVector2f)ComponentTransform.Position : ParentActor.Position + ComponentTransform.Position;
			set => LocalPosition = ComponentTransform.InverseTransform * value;
		}

		public virtual bool Movable { get; set; }

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

		protected internal virtual void OnInitializeActorComponent()
		{
			
		}

		public virtual void OnActorComponentDestroy()
		{
			Console.WriteLine("DESTROYING ACTORCOMPONENT: " + ComponentName + "-" + ComponentID);
		}

		public virtual void MoveLocal(float x, float y)
		{
			LocalPosition += new TVector2f(x, y);
		}

		public void MoveWorld(TVector2f position)
		{
			throw new NotImplementedException();
		}

		public void SetLocalPosition(float x, float y)
		{
			LocalPosition = new TVector2f(x, y);
		}

		public virtual void MoveLocal(TVector2f position)
		{
			LocalPosition += position;
		}

		public void MoveWorld(float x, float y)
		{
			WorldPosition += new TVector2f(x, y);
		}

		public void SetLocalPosition(TVector2f position)
		{
			LocalPosition = position;
		}

		public void SetWorldPosition(float x, float y)
		{
			WorldPosition = new TVector2f(x, y);
		}

		public void SetWorldPosition(TVector2f position)
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
			LocalScale += new TVector2f(x, y);
		}

		public void ScaleLocal(TVector2f scale)
		{
			LocalScale += scale;
		}

		public void ScaleWorld(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void ScaleWorld(TVector2f scale)
		{
			throw new NotImplementedException();
		}

		public void SetLocalScale(float x, float y)
		{
			LocalScale = new TVector2f(x, y);
		}

		public void SetLocalScale(TVector2f scale)
		{
			LocalScale = scale;
		}

		public void SetWorldScale(float x, float y)
		{
			throw new NotImplementedException();
		}

		public void SetWorldScale(TVector2f scale)
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