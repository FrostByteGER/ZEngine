using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ZEngine.Engine.Utility;
using Vector2 = System.Numerics.Vector2;

namespace ZEngine.Engine.Game
{
	public class Actor_OLD : ITickable, IDestroyable
	{

		public ulong ActorID { get; internal set; } = 0;
		public ulong LevelID { get; internal set; } = 0;
		public uint LayerID { get; set; } = 1;
		public uint ComponentIDCounter { get; private set; }
		public Level.Level_OLD LevelOldReference { get; internal set; }
		public string ActorName { get; set; }

		[JsonIgnore]
		public List<ActorComponent_OLD> Components { get; } = [];
		[JsonIgnore]
		public virtual ActorComponent_OLD RootComponentOld { get; private set; }

		public bool MarkedForRemoval { get; internal set; } = false;
		public virtual bool Visible { get; set; } = true;
		public bool CanTick { get; set; } = true;

		[JsonIgnore]
		public virtual Vector2 Position
		{
			get => RootComponentOld.LocalPosition;
			set => RootComponentOld.LocalPosition = value;
		}

		[JsonIgnore]
		public virtual float Rotation
		{
			get => RootComponentOld.LocalRotation;
			set => RootComponentOld.LocalRotation = value;
		}

		[JsonIgnore]
		public virtual Vector2 Scale
		{
			get => RootComponentOld.LocalScale;
			set => RootComponentOld.LocalScale = value;
		}

		[JsonIgnore]
		public virtual Vector2 Origin
		{
			get => RootComponentOld.Origin;
			set => RootComponentOld.Origin = value;
		}

		[JsonIgnore]
		public virtual Transform ActorTransform
		{
			get => RootComponentOld.ComponentTransform;
			set => RootComponentOld.ComponentTransform = value;
		}

		public Actor_OLD()
		{
			ActorName = GetType().Name;
		}

		protected internal virtual void InitializeActor()
		{
			InitializeComponents();
		}

		protected internal virtual void InitializeComponents()
		{
			foreach (var comp in Components)
			{
				comp.OnInitializeActorComponent();
			}
		}

		public virtual void Move(float x, float y)
		{
			RootComponentOld.MoveLocal(new Vector2(x, y));
		}

		public void MoveAbsolute(float x, float y)
		{
			RootComponentOld.SetLocalPosition(new Vector2(x, y));
		}

		public virtual void Move(Vector2 position)
		{
			RootComponentOld.MoveLocal(position);
		}


		public void Rotate(float angle)
		{
			RootComponentOld.RotateLocal(angle);
		}

		public void RotateAbsolute(float angle)
		{
			RootComponentOld.SetLocalRotation(angle);
		}

		public void ScaleActor(float x, float y)
		{
			RootComponentOld.ScaleLocal(new Vector2(x, y));
		}

		public void ScaleActor(Vector2 scale)
		{
			RootComponentOld.ScaleLocal(scale);
		}

		public void ScaleAbsolute(float x, float y)
		{
			RootComponentOld.SetLocalScale(new Vector2(x, y));
		}

		public void ScaleAbsolute(Vector2 scale)
		{
			RootComponentOld.SetLocalScale(scale);
		}

        public virtual void Tick(float deltaTime)
		{
			for (int i = 0 ; i < Components.Count ; i++)
			{
				if (Components.Count >= i)
				{
					Components[i].Tick(deltaTime);
				}
				else
				{
					break;
				}
			}
		}
        protected internal virtual void OnGameStart()
		{
		}

        protected internal virtual void OnGamePause()
		{
			foreach (var component in Components)
			{
				component.CanTick = false;
			}
			CanTick = false;
		}

        protected internal virtual void OnGameResume()
		{
			foreach (var component in Components)
			{
				component.CanTick = true;
			}
			CanTick = true;
		}

        protected internal virtual void OnGameEnd()
		{
			
		}

		public bool SetRootComponent(ActorComponent_OLD root)
		{
			if (root == null && RootComponentOld == null) 
                return false;
			if (root == null && RootComponentOld != null) 
                return RemoveRootComponent();
			Debug.LogDebug("Trying to set Root-ActorComponent " + root.ComponentName + " on Actor " + this, DebugLogCategories.Engine);
			RemoveRootComponent();

			if (Components.Contains(root))
			{
				var comp = Components.Find(x => x.ComponentID == root.ComponentID);
				if (comp == null) 
                    return false;
				root.ComponentID = 1;
				RootComponentOld = comp;
				comp.IsRootComponent = true;
				return true;
			}
			Components.Add(root);
			root.ComponentID = 1;
			root.ParentActorOld = this;
			RootComponentOld = root;
			root.IsRootComponent = true;
			return true;
		}

		public bool SetRootComponent(int rootIndex)
		{
			if (rootIndex < 0 || rootIndex >= Components.Count) 
                return false;
			return SetRootComponent(Components[rootIndex]);
		}

		public bool RemoveRootComponent()
		{
			if (RootComponentOld == null) 
                return false;
			Debug.LogDebug("Trying to remove RootComponent " + RootComponentOld.ComponentName + " from " + this, DebugLogCategories.Engine);
			RemoveComponent(RootComponentOld);
			RootComponentOld.IsRootComponent = false;
			RootComponentOld = null;
			return true;
		}

		public T GetRootComponent<T>() where T : ActorComponent_OLD
		{
			return (T)RootComponentOld;
		}

		public ActorComponent_OLD GetComponent(uint componentID)
		{
			return Components.Find(comp => comp.ComponentID == componentID);
		}

		/// <summary>
		/// Generic Version of GetComponent. Returns the component with the corresponding ID and casts it to T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="componentID"></param>
		/// <returns></returns>
		public T GetComponent<T>(uint componentID) where T : ActorComponent_OLD
		{
			return (T)Components.Find(comp => comp.ComponentID == componentID);
		}

		public T GetComponent<T>() where T : ActorComponent_OLD
		{
			return (T)Components.Find(comp => comp is T);
		}

		public IEnumerable<T> GetComponents<T>() where T : ActorComponent_OLD
		{
			return Components.FindAll(comp => comp is T).Cast<T>();
		}


		public bool AddComponent(ActorComponent_OLD componentOld)
		{
			if (componentOld == null || componentOld.ComponentName == null)
			{
				Debug.LogError("Failed to add component to Actor: " + this + ". Component is null or has null name!", DebugLogCategories.Engine);
                return false;
            }

			Debug.LogDebug("Trying to add "+ componentOld.ComponentName + " to " + this, DebugLogCategories.Engine);
			if (Components.Contains(componentOld)) 
                return false;
			Components.Add(componentOld);
			componentOld.ComponentID = ++ComponentIDCounter;
			componentOld.ParentActorOld = this;
			return true;
		}

		public void RemoveComponent(ActorComponent_OLD componentOld)
		{
			if (!Components.Contains(componentOld)) 
                return;
			Components.Remove(componentOld);
			componentOld.ParentActorOld = null;
		}

		public void RemoveComponent(int index)
		{
			Components[index].ParentActorOld = null;
			Components.RemoveAt(index);
		}

		public void RemoveAllComponents()
		{
			foreach (var component in Components)
			{
				component.ParentActorOld = null;
			}
			Components.Clear();
		}

		public virtual void OnActorDestroy()
		{
			Debug.LogDebug("Destroying Actor: " + GenerateFullName(), DebugLogCategories.Engine);
		}

		public string GenerateFullName()
		{
			return ActorName + "-" + ActorID;
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
			foreach (var comp in Components)
			{
				comp.Dispose();
			}
		}

		public override string ToString()
		{
			return GenerateFullName();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Actor_OLD)obj);
		}

		protected bool Equals(Actor_OLD other)
		{
			return ActorID == other.ActorID;
		}

		public override int GetHashCode()
		{
			return (int)ActorID;
		}

		public static bool operator ==(Actor_OLD left, Actor_OLD right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Actor_OLD left, Actor_OLD right)
		{
			return !Equals(left, right);
		}
	}
}