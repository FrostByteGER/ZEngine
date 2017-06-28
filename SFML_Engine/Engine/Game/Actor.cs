using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Dynamics;

namespace SFML_Engine.Engine.Game
{
	public class Actor : IActorable, IGameInterface, IDestroyable, ICollidable
	{

		public uint ActorID { get; internal set; } = 0;
		public uint LevelID { get; internal set; } = 0;
		public uint LayerID { get; set; } = 1;
		public uint ComponentIDCounter { get; private set; } = 0;
		public Level LevelReference { get; internal set; }
		public string ActorName { get; set; } = "Actor";

		[JsonIgnore]
		public TVector2f ActorBounds
		{
			get => RootComponent.ComponentBounds;
			set => RootComponent.ComponentBounds = value;
		}

		[JsonIgnore]
		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();
		[JsonIgnore]
		public virtual ActorComponent RootComponent { get; private set; } = null;
		/// <summary>
		/// TODO: CURRENTLY USELESS, NEEDS REWORK!
		/// </summary>
		public bool HasGravity { get; set; } = false;

		public bool MarkedForRemoval { get; internal set; } = false;
		public bool Visible { get; set; } = true;
		public bool CanTick { get; set; } = true;
		private bool _collisionCallbacksEnabled = true;
		[JsonIgnore]
		public bool CollisionCallbacksEnabled
		{
			get => _collisionCallbacksEnabled;
			set
			{
				_collisionCallbacksEnabled = value;
				foreach (var comp in Components)
				{
					var physComp = comp as PhysicsComponent;
					if (physComp != null) physComp.CollisionCallbacksEnabled = value;
				}
			}
		}

		[JsonIgnore]
		// TODO: Move to a PhysicsActor, not an Actor.
		public bool CanOverlap
		{
			get
			{
				var canoverlap = false;
				foreach (var comp in Components)
				{
					var physComp = comp as PhysicsComponent;
					if (physComp == null) continue;
					canoverlap = physComp.CanOverlap;
					if (canoverlap) break;
				}
				return canoverlap;
			}
			set
			{
				foreach (var comp in Components)
				{
					var physComp = comp as PhysicsComponent;
					if (physComp != null) physComp.CanOverlap = value;
				}
			}
		}

		[JsonIgnore]
		// TODO: Useless?
		public bool CanOverlapAll
		{
			get
			{
				var canoverlap = false;
				foreach (var comp in Components)
				{
					var physComp = comp as PhysicsComponent;
					if (physComp == null) continue;
					canoverlap = physComp.CanOverlap;
					if (!canoverlap) break;
				}
				return canoverlap;
			}
			set => CanOverlap = value;
		}

		[JsonIgnore]
		public TVector2f Position
		{
			get => RootComponent.LocalPosition;
			set => RootComponent.LocalPosition = value;
		}

		[JsonIgnore]
		public float Rotation
		{
			get => RootComponent.LocalRotation;
			set => RootComponent.LocalRotation = value;
		}

		[JsonIgnore]
		public TVector2f Scale
		{
			get => RootComponent.LocalScale;
			set => RootComponent.LocalScale = value;
		}

		[JsonIgnore]
		public TVector2f Origin
		{
			get => RootComponent.Origin;
			set => RootComponent.Origin = value;
		}

		[JsonIgnore]
		public TTransformable ActorTransform
		{
			get => RootComponent.ComponentTransform;
			set => RootComponent.ComponentTransform = value;
		}

		public Actor(Level level)
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
			RootComponent.MoveLocal(new TVector2f(x, y));
		}

		public void MoveAbsolute(float x, float y)
		{
			RootComponent.SetLocalPosition(new TVector2f(x, y));
		}

		public virtual void Move(TVector2f position)
		{
			RootComponent.MoveLocal(position);
		}


		public void Rotate(float angle)
		{
			RootComponent.RotateLocal(angle);
		}

		public void RotateAbsolute(float angle)
		{
			RootComponent.SetLocalRotation(angle);
		}

		public void ScaleActor(float x, float y)
		{
			RootComponent.ScaleLocal(new TVector2f(x, y));
		}

		public void ScaleActor(TVector2f scale)
		{
			RootComponent.ScaleLocal(scale);
		}

		public void ScaleAbsolute(float x, float y)
		{
			RootComponent.SetLocalScale(new TVector2f(x, y));
		}

		public void ScaleAbsolute(TVector2f scale)
		{
			RootComponent.SetLocalScale(scale);
		}

		public virtual void Tick(float deltaTime)
		{
			foreach (var component in Components)
			{
				component.Tick(deltaTime);
			}
			//Console.WriteLine(Position);
		}

		public virtual void OnCollide(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnCollideEnd(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnOverlapBegin(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnOverlapEnd(Fixture self, Fixture other, Contact contactInfo)
		{

		}

		public virtual void OnGameStart()
		{
		}

		public virtual void OnGamePause()
		{
			foreach (var component in Components)
			{
				component.CanTick = false;
			}
			CanTick = false;
		}

		public virtual void OnGameResume()
		{
			foreach (var component in Components)
			{
				component.CanTick = true;
			}
			CanTick = true;
		}

		public virtual void OnGameEnd()
		{
			
		}

		public bool SetRootComponent(ActorComponent root)
		{
			if (root == null && RootComponent == null) return false;
			if (root == null && RootComponent != null) return RemoveRootComponent();
			RemoveRootComponent();

			if (Components.Contains(root))
			{
				var comp = Components.Find(x => x.ComponentID == root.ComponentID);
				if (comp == null) return false;
				RootComponent = comp;
				comp.IsRootComponent = true;
				return true;
			}
			Components.Add(root);
			root.ParentActor = this;
			RootComponent = root;
			root.IsRootComponent = true;
			return true;
		}

		public bool SetRootComponent(int rootIndex)
		{
			if (rootIndex < 0 || rootIndex >= Components.Count) return false;
			return SetRootComponent(Components[rootIndex]);
		}

		public bool RemoveRootComponent()
		{
			if (RootComponent == null) return false;
			RemoveComponent(RootComponent);
			RootComponent.IsRootComponent = false;
			RootComponent = null;
			return true;
		}

		public T GetRootComponent<T>() where T : ActorComponent
		{
			return (T)RootComponent;
		}

		public ActorComponent GetComponent(uint componentID)
		{
			return Components.Find(comp => comp.ComponentID == componentID);
		}

		/// <summary>
		/// Generic Version of GetComponent. Returns the component with the corresponding ID and casts it to T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="componentID"></param>
		/// <returns></returns>
		public T GetComponent<T>(uint componentID) where T : ActorComponent
		{
			return (T)Components.Find(comp => comp.ComponentID == componentID);
		}

		public T GetComponent<T>() where T : ActorComponent
		{
			return (T)Components.Find(comp => comp is T);
		}

		public IEnumerable<T> GetComponents<T>() where T : ActorComponent
		{
			return Components.FindAll(comp => comp is T).Cast<T>();
		}


		public bool AddComponent(ActorComponent component)
		{
			Console.WriteLine("Trying to add ActorComponent: " + component + " to Actor: " + this);
			if (Components.Contains(component)) return false;
			Components.Add(component);
			component.ComponentID = ++ComponentIDCounter;
			component.ParentActor = this;
			return true;
		}

		public void RemoveComponent(ActorComponent component)
		{
			if (!Components.Contains(component)) return;
			Components.Remove(component);
			component.ParentActor = null;
		}

		public void RemoveComponent(int index)
		{
			Components[index].ParentActor = null;
			Components.RemoveAt(index);
		}

		public void RemoveAllComponents()
		{
			foreach (var component in Components)
			{
				component.ParentActor = null;
			}
			Components.Clear();
		}

		public virtual void OnActorDestroy()
		{
			Console.WriteLine("DESTROYING ACTOR: " + ActorName + "-" + ActorID);
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
			return Equals((Actor)obj);
		}

		protected bool Equals(Actor other)
		{
			return ActorID == other.ActorID;
		}

		public override int GetHashCode()
		{
			return (int)ActorID;
		}

		public static bool operator ==(Actor left, Actor right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Actor left, Actor right)
		{
			return !Equals(left, right);
		}
	}
}