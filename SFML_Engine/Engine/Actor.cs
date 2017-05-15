using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;
using System;
using System.Collections.Generic;
using BulletSharp;
using SFML;
using Quaternion = System.Numerics.Quaternion;

namespace SFML_Engine.Engine
{
	public class Actor : IActorable, IGameInterface, Drawable, IDestroyable, ICollidable
	{

		public uint ActorID { get; internal set; } = 0;
		public uint LevelID { get; internal set; } = 0;
		public uint LayerID { get; set; } = 1;
		public Level LevelReference { get; internal set; }
		public string ActorName { get; set; } = "Actor";
		public FloatRect ActorBounds { get; set; } = new FloatRect();
		public bool Movable { get; set; } = true;
		public Vector2f Velocity { get; set; }
		public float MaxVelocity { get; set; } = -1.0f;
		public Vector2f Acceleration { get; set; }
		public float MaxAcceleration { get; set; } = -1f;

		public float Friction = 0.0f;
		public float Mass { get; set; } = 1.0f;
		public List<ActorComponent> Components { get; set; } = new List<ActorComponent>();
		public virtual ActorComponent RootComponent { get; private set; } = null;
		public bool HasGravity { get; set; } = false;

		public bool MarkedForRemoval { get; internal set; } = false;
		public bool Visible { get; set; } = true;
		public bool CanTick { get; set; } = true;
		private bool collisionCallbacksEnabled = true;
		public bool CollisionCallbacksEnabled
		{
			get => collisionCallbacksEnabled;
			set
			{
				collisionCallbacksEnabled = value;
				foreach (var comp in Components)
				{
					var colComp = (PhysicsComponent) comp;
					if (colComp != null) colComp.CollisionCallbacksEnabled = value;
				}
			}
		}

		public Vector2f Position
		{
			get => RootComponent.Position;
			set => RootComponent.Position = value;
		}

		public float Rotation
		{
			get => RootComponent.Rotation;
			set => RootComponent.Rotation = value;
		}

		public Vector2f Scale
		{
			get => RootComponent.Scale;
			set => RootComponent.Scale = value;
		}

		public Vector2f Origin
		{
			get => RootComponent.Origin;
			set => RootComponent.Origin = value;
		}

		public Actor()
		{
		}


		public virtual void Move(float x, float y)
		{
			RootComponent.Move(new Vector2f(x, y));
		}

		public void MoveAbsolute(float x, float y)
		{
			RootComponent.MoveAbsolute(new Vector2f(x, y));
		}

		public virtual void Move(Vector2f position)
		{
			RootComponent.Move(position);
		}

		public void MoveAbsolute(Vector2f position)
		{
			RootComponent.MoveAbsolute(position);
		}

		public void Rotate(float angle)
		{
			RootComponent.Rotate(angle);
		}

		public void Rotate(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public void RotateAbsolute(float angle)
		{
			RootComponent.RotateAbsolute(angle);
		}

		public void RotateAbsolute(Quaternion angle)
		{
			throw new NotImplementedException();
		}

		public void ScaleActor(float x, float y)
		{
			RootComponent.ScaleActor(new Vector2f(x, y));
		}

		public void ScaleActor(Vector2f scale)
		{
			RootComponent.ScaleActor(scale);
		}

		public void ScaleAbsolute(float x, float y)
		{
			RootComponent.ScaleAbsolute(new Vector2f(x, y));
		}

		public void ScaleAbsolute(Vector2f scale)
		{
			RootComponent.ScaleAbsolute(scale);
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

		public bool AddComponent(ActorComponent component)
		{
			if (Components.Contains(component)) return false;
			Components.Add(component);
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

		public ActorInformation GenerateActorInformation()
		{
			return new ActorInformation(ActorID, LevelID, RootComponent.Position, RootComponent.Rotation, RootComponent.Scale, new Vector2f(), Movable, Velocity, MaxVelocity,
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

		public void Draw(RenderTarget target, RenderStates states)
		{
			RootComponent.Draw(target, states);
			foreach (var drawable in Components)
			{
				drawable.Draw(target, states);
			}
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
				RootComponent.Dispose();
				comp.Dispose();
			}
		}

		public override string ToString()
		{
			return GenerateFullName();
		}

		public void OnCollide(ManifoldPoint cp, CollisionObjectWrapper collider1, int partId1, int index1,
			CollisionObjectWrapper collider2, int partId2, int index2)
		{
			
		}
	}
}