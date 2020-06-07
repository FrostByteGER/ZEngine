using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;
using ZEngine.Engine.Events;
using ZEngine.Engine.Events.Messages;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Game
{
	public class Level : IDestroyable
    {

	    public uint LevelID { get; internal set; } = 0;

		public uint ActorIDCounter { get; private set; } = 0;

	    [JsonProperty()]
		private List<Actor> _actors = new List<Actor>();

        private IMessageBus _bus;

        [JsonIgnore]
	    internal ReadOnlyCollection<Actor> Actors => new ReadOnlyCollection<Actor>(_actors);

	    /// <summary>
	    /// Bounds of this level. To get actual height and width, multiply the X and Y value by 2.
	    /// </summary>
	    public Vector2 LevelBounds { get; set; } = new Vector2(float.MaxValue / 2.0f, float.MaxValue / 2.0f);
		/*
		/// <summary>
		/// Dummy Object for efficient Collision Debug Drawing.
		/// </summary>
	    public static CircleShape CollisionCircle { get; set; } = new CircleShape(10.0f);
		/// <summary>
		/// Dummy Object for efficient Collision Debug Drawing.
		/// </summary>
		public static RectangleShape CollisionRectangle { get; set; } = new RectangleShape(new Vector2(10.0f,10.0f));
		*/
	    [JsonIgnore]
		public Core.Engine EngineReference { get; set; }

        public GameMode GameMode { get; set; } = new GameMode();

		/*
	    [JsonIgnore]
		public PhysicsWorld PhysicsWorld { get; private set; } = new PhysicsWorld();
		*/

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

	    public ActorSpawner Spawner { get; } = new ActorSpawner();


		public Level()
		{
		}

	    protected internal virtual void InitLevel()
	    {
		    Debug.Log("Initiating Level " + LevelID, DebugLogCategories.Engine);
            _bus = EngineReference.GetService<IEngineMessageBus>();
        }


        protected internal virtual void LevelTick(float deltaTime)
        {
	        //Debug.LogDebug("Level Tick!", DebugLogCategories.Engine);
			foreach (var pc in Players)
			{
				if (!pc.CanTick) continue;
				pc.Tick(deltaTime);
			    //pc.Hud?.Tick(deltaTime);
			}
			foreach (var actor in _actors)
            {
	            if (!actor.CanTick) continue;
				actor.Tick(deltaTime);
            }
	        if (GameMode.CanTick) GameMode.Tick(deltaTime);
	        if (TimerManager.CanTick) TimerManager.Tick(deltaTime);
        }

        protected internal virtual void LevelDraw()
        {
			foreach (var pc in Players)
	        {
				/*
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
				if (pc.Hud != null)
				renderWindow.Draw(pc.Hud);
				*/
			}
		}

	    public virtual void OnLevelLoad()
	    {
		    InitLevel();
			Debug.Log("Level #" + LevelID + " Loaded", DebugLogCategories.Engine);
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

        protected internal virtual void OnGameStart()
	    {
			GameMode.LevelReference = this;
		    GameMode.OnGameStart();
			foreach (var pc in Players)
			{
				if (!pc.IsActive) 
                    continue;
				pc.OnGameStart();
			}
			foreach (var actor in _actors)
		    {
			    actor.OnGameStart();
		    }
	    }

        protected internal virtual void OnGamePause()
	    {
		    LevelTicking = false;
		    LevelPaused = true;
		    //PhysicsWorld.CanTick = false;
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

        protected internal virtual void OnGameResume()
	    {
		    LevelPaused = false;
		    LevelTicking = true;
		    //PhysicsWorld.CanTick = true;
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

        protected internal virtual void OnGameEnd()
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
			//PhysicsWorld.ShutdownPhysicsEngine();
			Dispose();
	    }

	    /// <summary>
		/// Spawns the Actor instantly. Only allowed after LevelTick has finished, otherwise causes crash due to Collection Modification!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
	    internal T SpawnActorInternal<T>() where T : Actor
	    {
			var actor = Spawner.SpawnObject<T>(this);
		    RegisterActor(actor);
		    actor.OnGameStart();
			return actor;
	    }

		/// <summary>
		/// Spawns the Actor instantly. Only allowed after LevelTick has finished, otherwise causes crash due to Collection Modification!
		/// </summary>
		/// <param name="actorType"></param>
		/// <returns></returns>
		internal Actor SpawnActorInternal(Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) return null;
			var actor = Spawner.SpawnObject(actorType, this) as Actor;
			RegisterActor(actor);
			actor?.OnGameStart();
			return actor;
		}

	    /// <summary>
		/// Constructs the actor instantly but spawns it at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T SpawnActor<T>() where T : Actor
		{
			var actor = Spawner.SpawnObject<T>(this);
			_bus.Publish(new RegisterEventMessage(this, new RegisterActorEvent<RegisterActorParams>(new RegisterActorParams(this, actor, this))));
			return actor;
		}

		/// <summary>
		/// Constructs the actor instantly but spawns it at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <param name="actorType"></param>
		/// <returns></returns>
		public Actor SpawnActor(Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) return null;
			var actor = Spawner.SpawnObject(actorType, this) as Actor;
            _bus.Publish(new RegisterEventMessage(this, new RegisterActorEvent<RegisterActorParams>(new RegisterActorParams(this, actor, this))));
			return actor;
		}

		/// <summary>
		/// Constructs and spawns the actor at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instigator"></param>
		public void SpawnActorDeferred<T>(Actor instigator) where T : Actor
		{
            _bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, typeof(T), this))));
		}

		public void SpawnActorDeferred<T>() where T : Actor
		{
            _bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, typeof(T), this))));
		}

		/// <summary>
		/// Constructs and spawns the actor at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <param name="instigator"></param>
		/// <param name="actorType"></param>
		public void SpawnActorDeferred(Actor instigator, Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) 
                return;
            _bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, actorType, this))));
		}

		/// <summary>
		/// Constructs and spawns the actor at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <param name="actorType"></param>
		public void SpawnActorDeferred(Type actorType)
		{
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) 
                return;
            _bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, actorType, this))));
		}

		public void RegisterActor(Actor actor)
		{
			if (ContainsActorInLevel(actor)) 
                return;
			actor.ActorID = ActorIDCounter;
			++ActorIDCounter;
			actor.LevelID = LevelID;
			actor.LevelReference = this;
			Debug.LogDebug("Trying to register Actor: " + actor, DebugLogCategories.Engine);
			_actors.Add(actor);
		}

	    public void UnregisterActors()
	    {
			Debug.LogDebug("Removing all Actors!", DebugLogCategories.Engine);

			foreach (var actor in _actors)
			{
				actor.OnActorDestroy();
				foreach (var comp in actor.Components)
				{
					/*
					var physComp = comp as PhysicsComponent;
					if (physComp != null)
					{
						PhysicsWorld.UnregisterPhysicsComponent(physComp);
					}
					*/
				}
			}
		    _actors.Clear();
	    }

		public bool UnregisterActor(Actor actor)
		{
			Debug.LogDebug("Trying to remove Actor: " + actor, DebugLogCategories.Engine);
			actor.OnActorDestroy();
			var removal = _actors.Remove(actor);
			foreach (var comp in actor.Components)
			{
				/*
				var physComp = comp as PhysicsComponent;
				if (physComp != null)
				{
					PhysicsWorld.UnregisterPhysicsComponent(physComp);
				}
				*/
			}
			return removal;
		}

		public bool UnregisterActor(uint actorID)
		{
			Debug.LogDebug("Trying to remove Actor with ActorID: #" + actorID, DebugLogCategories.Engine);
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
            _bus.Publish(new RegisterEventMessage(this, new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(instigator, actor))));
		}

		public void DestroyActor(Actor actor)
		{
            _bus.Publish(new RegisterEventMessage(this, new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, actor))));
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
			if (Players.Contains(pc)) 
                return;
			pc.LevelReference = this;
			pc.ID = Players.Count > 0 ? (uint)Players.Count - 1 : 0;
			Players.Add(pc);
			pc.MarkedForInputRegistering = active;
			Debug.LogDebug("Trying to Register Player: " + pc, DebugLogCategories.Engine);
		}

		public void UnregisterPlayers()
		{
			Debug.LogDebug("Removing all Players!", DebugLogCategories.Engine);
			foreach (var pc in Players)
			{
				pc.IsActive = false;
			}
			Players.Clear();
		}

		public bool UnregisterPlayer(PlayerController pc)
		{
			Debug.LogDebug("Trying to remove Player with PlayerID: #" + pc.ID, DebugLogCategories.Engine);
			pc.IsActive = false;
			return Players.Remove(pc);
		}

		public bool UnregisterPlayer(uint playerID)
		{
			Debug.LogDebug("Trying to remove Player with PlayerID: #" + playerID, DebugLogCategories.Engine);
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
		    if (obj is null) return false;
		    if (ReferenceEquals(this, obj)) return true;
		    return obj.GetType() == this.GetType() && Equals((Level) obj);
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
