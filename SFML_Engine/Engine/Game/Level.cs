using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
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

	    [JsonProperty()]
		private List<Actor> _actors = new List<Actor>();

	    [JsonIgnore]
	    internal ReadOnlyCollection<Actor> Actors => new ReadOnlyCollection<Actor>(_actors);

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

	    [JsonIgnore]
		public Core.Engine EngineReference { get; set; }

        public GameMode GameMode { get; set; } = new GameMode();

	    [JsonIgnore]
		public VelcroPhysicsEngine PhysicsEngine { get; private set; } = new VelcroPhysicsEngine();

	    public bool LevelLoaded { get; set; } = false;
		internal bool LevelTicking { get; set; } = false;
	    public bool LevelPaused { get; private set; } = false;

		public List<PlayerController> Players { get; private set; } = new List<PlayerController>();

	    public TimerManager TimerManager { get; private set; } = new TimerManager();

		/// <summary>
		/// Count of Layers. ZERO-BASED! ACTUAL LAYER COUNT IS LAYERS+1.
		/// <para>Layers are sorted from front to back. So 0 is the front and Layers.MaxValue is the back. Usually the most front layer(0) is the UI layer.</para>
		/// </summary>
	    public uint Layers { get; set; } = 2;

	    public ActorSpawner Spawner { get; set; } = new ActorSpawner();


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
			foreach (var actor in _actors)
            {
	            if (!actor.CanTick) continue;
				actor.Tick(deltaTime);
            }
	        if (GameMode.CanTick) GameMode.Tick(deltaTime);
	        if (TimerManager.CanTick) TimerManager.Tick(deltaTime);
        }

        protected internal virtual void LevelDraw(ref RenderWindow renderWindow)
        {
	        foreach (var pc in Players)
	        {
		        renderWindow.SetView(pc.PlayerCamera);
				// TODO: Evaluate Performance!
		        var drawableActors = _actors.FindAll(a => a.Visible).OrderByDescending(a => a.LayerID);
				foreach (var actor in drawableActors)
				{
					var c1 = actor.GetComponents<RenderComponent>();
					var c11 = c1.Where(c => c.Visible);
					var drawableComps = c11.OrderByDescending(c => c.ComponentLayerID);
					foreach (var comp in drawableComps)
					{
						renderWindow.Draw(comp);
					}
				}
			}
		}

	    public virtual void OnLevelLoad()
	    {
		    InitLevel();
			Console.WriteLine("Level #" + LevelID + " Loaded");
			foreach (var actor in _actors)
		    {
			    actor.InitializeActor();
		    }
		    foreach (var pc in Players)
		    {
			    pc.IsActive = pc.MarkedForInputRegistering; // If Player Controllers were marked for Input Registration, register their input now.
			    pc.MarkedForInputRegistering = false;
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
			foreach (var actor in _actors)
		    {
			    actor.OnGameStart();
		    }
	    }

	    public virtual void OnGamePause()
	    {
		    LevelTicking = false;
		    LevelPaused = true;
		    PhysicsEngine.CanTick = false;
			GameMode.OnGamePause();
			foreach (var pc in Players)
			{
				if (!pc.IsActive) continue;
				pc.OnGamePause();
			}
			foreach (var actor in _actors)
			{
				actor.OnGamePause();
			}
		}

	    public virtual void OnGameResume()
	    {
		    LevelPaused = false;
		    LevelTicking = true;
		    PhysicsEngine.CanTick = true;
			GameMode.OnGameResume();
			foreach (var pc in Players)
			{
				if (!pc.IsActive) continue;
				pc.OnGameResume();
			}
			foreach (var actor in _actors)
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
			foreach (var actor in _actors)
			{
				actor.OnGameEnd();
			}
		}

	    internal virtual void OnUnloadLevel()
	    {
		}

	    internal virtual void ShutdownLevel()
	    {
		    LevelLoaded = false;

		    UnregisterActors();
			UnregisterPlayers();
			PhysicsEngine.ShutdownPhysicsEngine();
			Dispose();
	    }

	    public T SpawnActor<T>() where T : Actor
	    {
			var actor =  Spawner.SpawnObject<T>(this);
		    RegisterActor(actor);
			return actor;
	    }

		public Actor SpawnActor(Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) return null;
			var actor = Spawner.SpawnObject(actorType, this) as Actor;
			RegisterActor(actor);
			return actor;
		}

		public void SpawnActorDeferred<T>(Actor instigator) where T : Actor
		{
			Core.Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, typeof(T), this)));
		}

		public void SpawnActorDeferred<T>() where T : Actor
		{
			Core.Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, typeof(T), this)));
		}

		public void SpawnActorDeferred(Actor instigator, Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) return;
			Core.Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, actorType, this)));
		}

		public void SpawnActorDeferred(Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) return;
			Core.Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, actorType, this)));
		}

		public void RegisterActor(Actor actor)
		{
			if (ContainsActorInLevel(actor)) return;
			actor.ActorID = ActorIDCounter;
			++ActorIDCounter;
			actor.LevelID = LevelID;
			actor.LevelReference = this;
			Console.WriteLine("Trying to register Actor: " + actor);
			_actors.Add(actor);
		}

	    public void UnregisterActors()
	    {
			Console.WriteLine("Removing all Actors!");

			foreach (var actor in _actors)
			{
				actor.OnActorDestroy();
				foreach (var comp in actor.Components)
				{
					var physComp = comp as PhysicsComponent;
					if (physComp != null)
					{
						PhysicsEngine.UnregisterPhysicsComponent(physComp);
					}
				}
			}
		    _actors.Clear();
	    }

		public bool UnregisterActor(Actor actor)
		{
			Console.WriteLine("Trying to remove Actor: " + actor);
			actor.OnActorDestroy();
			var removal = _actors.Remove(actor);
			foreach (var comp in actor.Components)
			{
				var physComp = comp as PhysicsComponent;
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

	    public bool ContainsActorInLevel(string name)
	    {
		    return FindActorInLevel(name) != null;
	    }

	    public bool ContainsActorInLevel(Actor actor)
	    {
		    return _actors.Contains(actor);
	    }

		public Actor FindActorInLevel(string name)
	    {
		    return _actors.Find(x => x.ActorName == name);
	    }

		public T FindActorInLevel<T>(string name) where T : Actor
		{
			return (T)_actors.Find(x => x.ActorName == name);
		}

		public Actor FindActorInLevel(uint id)
	    {
		    return _actors.Find(x => x.ActorID == id);
		}

		public T FindActorInLevel<T>(uint id) where T : Actor
		{
			return (T)_actors.Find(x => x.ActorID == id);
		}

		public IEnumerable<Actor> FindActorsInLevel(string name)
	    {
		    return _actors.FindAll(x => x.ActorName == name);
		}

		public IEnumerable<T> FindActorsInLevel<T>(string name) where T : Actor
		{
			return _actors.FindAll(x => x.ActorName == name).Cast<T>();
		}

		public IEnumerable<Actor> FindActorsInLevel(Type actor)
	    {
		    return _actors.FindAll(x => x.GetType() == actor);
		}

		public IEnumerable<T> FindActorsInLevel<T>() where T : Actor
		{
			return _actors.FindAll(x => x is T).Cast<T>();
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
		/// Registers the given PlayerController in this level and Marks for Input Registering.
		/// </summary>
		/// <param name="pc"></param>
		public void RegisterPlayer(PlayerController pc)
		{
			RegisterPlayer(pc, true);
		}

		/// <summary>
		/// Registers the given PlayerController in this level and registers its input.
		/// </summary>
		/// <param name="pc"></param>
		public void RegisterPlayer(PlayerController pc, bool active)
		{
			pc.LevelReference = this;
			pc.ID = (uint)Players.Count - 1;
			Players.Add(pc);
			pc.MarkedForInputRegistering = active;
		}

		public void UnregisterPlayers()
		{
			Console.WriteLine("Removing all Players!");
			foreach (var pc in Players)
			{
				pc.IsActive = false;
			}
			Players.Clear();
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

	    public void RegisterTimer(Timer timer)
	    {
		    TimerManager.AddTimer(timer);
	    }

		public void UnregisterTimer(Timer timer)
		{
			TimerManager.RemoveTimer(timer);
		}

		public void UnregisterTimer(int index)
		{
			TimerManager.RemoveTimer(index);
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
			foreach (var actor in _actors)
			{
				actor.Dispose();
			}
			_actors.Clear();
		}
	}
}
