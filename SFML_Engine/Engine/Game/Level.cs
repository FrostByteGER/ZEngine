using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Graphics;
using SFML_Engine.Engine.Physics;
using SFML_Engine.Engine.Utility;

namespace SFML_Engine.Engine.Game
{
	public class Level : IGameInterface, IDestroyable
    {

	    public uint LevelID { get; internal set; } = 0;

		public uint ActorIDCounter { get; private set; } = 0;

		internal List<Actor> Actors { get; set; } = new List<Actor>();

		/// <summary>
		/// Bounds of this level. To get actual height and width, multiply the X and Y value by 2.
		/// </summary>
	    public TVector2f LevelBounds { get; set; } = new TVector2f(float.MaxValue / 2.0f, float.MaxValue / 2.0f);

		/// <summary>
		/// Dummy Object for efficient Collision Debug Drawing.
		/// </summary>
	    public static CircleShape CollisionCircle { get; set; } = new CircleShape(10.0f);
		/// <summary>
		/// Dummy Object for efficient Collision Debug Drawing.
		/// </summary>
		public static RectangleShape CollisionRectangle { get; set; } = new RectangleShape(new Vector2f(10.0f,10.0f));

        public Core.Engine EngineReference { get; set; }

        public GameMode GameMode { get; set; } = new GameMode();

	    public VelcroPhysicsEngine PhysicsEngine { get; private set; } = new VelcroPhysicsEngine();

	    public bool LevelLoaded { get; set; } = false;
		internal bool LevelTicking { get; set; } = false;

		public List<PlayerController> Players { get; private set; } = new List<PlayerController>();

		/// <summary>
		/// Count of Layers. ZERO-BASED! ACTUAL LAYER COUNT IS LAYERS+1.
		/// <para>Layers are sorted from front to back. So 0 is the front and Layers.MaxValue is the back. Usually the most front layer(0) is the UI layer.</para>
		/// </summary>
	    public uint Layers { get; set; } = 2;


		public Level()
        {
        }

	    protected internal virtual void InitLevel()
	    {
		    Console.WriteLine("Initiating Level " + LevelID);
	    }


        protected internal virtual void LevelTick(float deltaTime)
        {
			//Console.WriteLine("Level Tick!");
			foreach (var pc in Players)
			{
				if (!pc.CanTick) continue;
				pc.Tick(deltaTime);
			}
			foreach (var actor in Actors)
            {
	            if (!actor.CanTick) continue;
				actor.Tick(deltaTime);
            }
	        if (!GameMode.CanTick) return;
			GameMode.Tick(deltaTime);
        }

        protected internal virtual void LevelDraw(ref RenderWindow renderWindow)
        {
	        foreach (var pc in Players)
	        {
		        renderWindow.SetView(pc.PlayerCamera);

				// TODO: Evaluate Performance!
		        var drawableActors = Actors.FindAll(a => a.Visible).OrderByDescending(a => a.LayerID).ToList();

				foreach (var actor in drawableActors)
				{
					var c1 = actor.GetComponents<RenderComponent>();
					var c11 = c1.FindAll(c => c.Visible);
					var c2 = c11.OrderByDescending(c => c.ComponentLayerID);
					var drawableComps = c2.ToList();
					if (drawableComps.Count <= 0) continue;
					foreach (var comp in drawableComps)
					{
						renderWindow.Draw(comp);
					}
				}
				//TODO: Instead of drawing them in the order the components are arranged in the actor.Components array, get all possible and visible RenderComponents and draw them based on layers.
				/*
				if (comp is CollisionComponent)
				{
					if (comp.ComponentName == "Test")
					{
						Console.WriteLine("THIS");
					}
					var colComp = comp as CollisionComponent;
					var colBody = colComp.CollisionBody;
					var colShape = colBody.CollisionShape;
					if (colComp.Visible && colBody.GetType() == typeof(RigidBody) && colShape.GetType() == typeof(BoxShape))
					{
						CollisionRectangle.Size = EngineMath.Vec3ToVec2f(((BoxShape)colShape).HalfExtentsWithoutMargin * 2.0f);
						CollisionRectangle.Position = colComp.Position;
						CollisionRectangle.Rotation = colComp.Rotation;
						CollisionRectangle.Scale = colComp.Scale;
						CollisionRectangle.Origin = colComp.Origin;
						CollisionRectangle.FillColor = colComp.ComponentColor;

						renderWindow.Draw(CollisionRectangle);
					}
					else if (colComp.Visible && colBody.GetType() == typeof(RigidBody) && colShape.GetType() == typeof(SphereShape))
					{
						CollisionCircle.Radius = ((SphereShape)colShape).Radius;
						CollisionCircle.Position = colComp.Position;
						CollisionCircle.Rotation = colComp.Rotation;
						CollisionCircle.Scale = colComp.Scale;
						CollisionCircle.Origin = colComp.Origin;
						CollisionCircle.FillColor = colComp.ComponentColor;

						renderWindow.Draw(CollisionCircle);
					}
				}*/
			}
		}

	    public virtual void OnLevelLoad()
	    {
		    Console.WriteLine("Level #" + LevelID + " Loaded");
		    foreach (var pc in Players)
		    {
			    pc.IsActive = pc.IsActive; // If Player Controllers were set to active, register their input now.
		    }
		    OnGameStart();
	    }

		public virtual void OnGameStart()
	    {
			GameMode.LevelReference = this;
		    GameMode.OnGameStart();
			foreach (var pc in Players)
			{
				if (!pc.IsActive) continue;
				pc.OnGameStart();
			}
			foreach (var actor in Actors)
		    {
			    actor.OnGameStart();
		    }
	    }

	    public virtual void OnGamePause()
	    {
			GameMode.OnGamePause();
			foreach (var pc in Players)
			{
				if (!pc.IsActive) continue;
				pc.OnGamePause();
			}
			foreach (var actor in Actors)
			{
				actor.OnGamePause();
			}
		}

	    public virtual void OnGameResume()
	    {
			GameMode.OnGameResume();
			foreach (var pc in Players)
			{
				if (!pc.IsActive) continue;
				pc.OnGameResume();
			}
			foreach (var actor in Actors)
			{
				actor.OnGameResume();
			}
		}

	    public virtual void OnGameEnd()
	    {
			GameMode.OnGameEnd();
			LevelTicking = false;
			foreach (var pc in Players)
			{
				pc.OnGameEnd();
			}
			foreach (var actor in Actors)
			{
				actor.OnGameEnd();
			}
		}

	    internal virtual void ShutdownLevel()
	    {
		    LevelLoaded = false;
		    PhysicsEngine.ShutdownPhysicsEngine();
			Dispose();
	    }

		public void RegisterActorComponent(ActorComponent component)
		{
			component.ComponentID = ActorIDCounter;
			++ActorIDCounter;
			Console.WriteLine("Trying to register ActorComponent: " + component);
		}

		public void RegisterActor(Actor actor)
		{
			actor.ActorID = ActorIDCounter;
			++ActorIDCounter;
			actor.LevelID = LevelID;
			actor.LevelReference = this;
			Console.WriteLine("Trying to register Actor: " + actor);
			foreach (var component in actor.Components)
			{
				RegisterActorComponent(component);
			}
			Actors.Add(actor);
		}

		public bool UnregisterActor(Actor actor)
		{
			Console.WriteLine("Trying to remove Actor: " + actor);
			actor.OnActorDestroy();
			var removal = Actors.Remove(actor);
			foreach (var comp in actor.Components)
			{
				var physComp = (PhysicsComponent)comp;
				if (physComp != null)
				{
					PhysicsEngine.UnregisterPhysicsComponent(physComp);
				}
			}
			return removal;
		}

		public bool UnregisterActor(uint actorID)
		{
			Console.WriteLine("Trying to remove Actor with ActorID: #" + actorID);
			var actor = FindActorInLevel(actorID);
			return UnregisterActor(actor);
		}

		public Actor FindActorInLevel(string name)
	    {
		    return Actors.Find(x => x.ActorName == name);
	    }

		public T FindActorInLevel<T>(string name) where T : Actor
		{
			return (T)Actors.Find(x => x.ActorName == name);
		}

		public Actor FindActorInLevel(uint id)
	    {
		    return Actors.Find(x => x.ActorID == id);
		}

		public T FindActorInLevel<T>(uint id) where T : Actor
		{
			return (T)Actors.Find(x => x.ActorID == id);
		}

		public List<Actor> FindActorsInLevel(string name)
	    {
		    return Actors.FindAll(x => x.ActorName == name);
		}

		public List<T> FindActorsInLevel<T>(string name) where T : Actor
		{
			return Actors.FindAll(x => x.ActorName == name).Cast<T>().ToList();
		}

		public List<Actor> FindActorsInLevel(Type actor)
	    {
		    return Actors.FindAll(x => x.GetType() == actor);
		}

		public List<T> FindActorsInLevel<T>() where T : Actor
		{
			return Actors.FindAll(x => x is T).Cast<T>().ToList();
		}

		public ReadOnlyCollection<Actor> GetActors()
	    {
		    return Actors.AsReadOnly();
	    }

	    public void SpawnActor(Actor instigator, Actor actor)
	    {
			Core.Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, actor, LevelID)));
		}

		public void SpawnActor(Actor actor)
		{
			Core.Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, actor, LevelID)));
		}

		public void PauseActor(Actor instigator, Actor actor)
		{

		}
		public void PauseActor(Actor actor)
		{

		}

		public void DestroyActor(Actor instigator, Actor actor)
	    {
			Core.Engine.Instance.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(instigator, actor)));
		}

		public void DestroyActor(Actor actor)
		{
			Core.Engine.Instance.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, actor)));
		}

		/// <summary>
		/// Registers the given PlayerController in this level and registers its input.
		/// </summary>
		/// <param name="pc"></param>
		public void RegisterPlayer(PlayerController pc)
		{
			Players.Add(pc);
			pc.LevelReference = this;
			pc.ID = (uint)Players.Count - 1;
			pc.IsActive = true;
		}

		/// <summary>
		/// Registers the given PlayerController in this level and registers its input depending on the given boolean.
		/// </summary>
		/// <param name="pc"></param>
		/// <param name="isActive">Wether or not the input of the PlayerController should be registered or not.</param>
		public void RegisterPlayer(PlayerController pc, bool isActive)
		{
			Players.Add(pc);
			pc.LevelReference = this;
			pc.ID = (uint)Players.Count - 1;
			pc.IsActive = isActive;
		}

		public bool UnregisterPlayer(PlayerController pc)
		{
			Console.WriteLine("Trying to remove Player with PlayerID: #" + pc.ID);
			pc.IsActive = false;
			return Players.Remove(pc);
		}

		public bool UnregisterPlayer(uint playerID)
		{
			Console.WriteLine("Trying to remove Player with PlayerID: #" + playerID);
			var player = FindPlayer(playerID);
			return UnregisterPlayer(player);
		}

		public PlayerController FindPlayer(uint playerID)
		{
			return Players.Find(x => x.ID == playerID);
		}

		public T FindPlayer<T>(uint playerID) where T : PlayerController
		{
			return (T)Players.Find(x => x.ID == playerID);
		}

		protected bool Equals(Level other)
	    {
		    return LevelID == other.LevelID;
	    }

	    public override bool Equals(object obj)
	    {
		    if (ReferenceEquals(null, obj)) return false;
		    if (ReferenceEquals(this, obj)) return true;
		    if (obj.GetType() != this.GetType()) return false;
		    return Equals((Level) obj);
	    }

	    public override int GetHashCode()
	    {
		    return (int) LevelID;
	    }

	    public static bool operator ==(Level left, Level right)
	    {
		    return Equals(left, right);
	    }

	    public static bool operator !=(Level left, Level right)
	    {
		    return !Equals(left, right);
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

		public void Destroy(bool disposing)
		{
			foreach (var actor in Actors)
			{
				foreach (var comp in actor.Components)
				{
					comp.Dispose();
				}
				actor.Dispose();
			}
			Actors.Clear();
		}
	}
}
