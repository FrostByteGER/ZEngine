using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using ZEngine.Engine.Events;
using ZEngine.Engine.Events.Messages;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Utility;

namespace ZEngine.Engine.Game.Level
{
	public class Level : IDestroyable
    {
	    public ulong LevelID { get; internal set; } = 0;

		public ulong ActorIDCounter { get; private set; } = 0;

		private readonly List<Actor> _actors = new List<Actor>();

        private IMessageBus _bus;

	    internal ReadOnlyCollection<Actor> Actors => new ReadOnlyCollection<Actor>(_actors);

	    /// <summary>
	    /// Bounds of this level. To get actual height and width, multiply the X and Y value by 2.
	    /// </summary>
	    public Vector2 Bounds { get; set; } = new Vector2(float.MaxValue / 2.0f, float.MaxValue / 2.0f);

        public GameMode GameMode { get; set; } = new GameMode();

	    public bool Loaded { get; set; }
		internal bool Ticking { get; set; }

		public List<PlayerController> Players { get; } = new List<PlayerController>();

	    public TimerManager TimerManager { get; } = new TimerManager();

        public ActorSpawner Spawner { get; } = new ActorSpawner();

        protected internal virtual void OnLevelLoad()
        {
            InitLevel();
            Debug.Log("Level #" + LevelID + " Loaded", DebugLogCategories.Engine);
            foreach (var actor in _actors)
            {
                actor.InitializeActor();
            }
            OnGameStart();
        }

        protected internal virtual void InitLevel()
        {
            Debug.Log("Initiating Level " + LevelID, DebugLogCategories.Engine);
            _bus = Core.Engine.Instance.GetService<IEngineMessageBus>();
        }

		protected internal virtual void OnGameStart()
        {
            GameMode.OnGameStart();
            foreach (var actor in _actors)
            {
                actor.OnGameStart();
            }

            foreach (var pc in Players)
            {
                pc.OnGameStart();
            }
		}

        protected internal virtual void Tick(float deltaTime)
        {
	        //Debug.LogDebug("Level Tick!", DebugLogCategories.Engine);
			foreach (var pc in Players)
			{
				if (!pc.CanTick) 
                    continue;
				pc.Tick(deltaTime);
			    //pc.Hud?.Tick(deltaTime);
			}
			foreach (var actor in _actors)
            {
	            if (!actor.CanTick) 
                    continue;
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

        protected internal virtual void OnGamePause()
	    {
		    Ticking = false;
			GameMode.OnGamePause();
			foreach (var pc in Players)
			{
                pc.OnGamePause();
			}
			foreach (var actor in _actors)
			{
				actor.OnGamePause();
			}
		}

        protected internal virtual void OnGameResume()
	    {
            GameMode.OnGameResume();
			foreach (var pc in Players)
			{
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
			Ticking = false;
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
		    Loaded = false;
		    UnregisterActors();
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
			if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) 
                return null;
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
			//var actor = Spawner.SpawnObject<T>(this);
			//_bus.Publish(new RegisterEventMessage(this, new RegisterActorEvent<RegisterActorParams>(new RegisterActorParams(this, actor, this))));
			//return actor;
            return null;
		}

		/// <summary>
		/// Constructs the actor instantly but spawns it at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <param name="actorType"></param>
		/// <returns></returns>
		public Actor SpawnActor(Type actorType)
		{
			//if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) 
            //    return null;
			//var actor = Spawner.SpawnObject(actorType, this) as Actor;
            //_bus.Publish(new RegisterEventMessage(this, new RegisterActorEvent<RegisterActorParams>(new RegisterActorParams(this, actor, this))));
			//return actor;
            return null;
        }

		/// <summary>
		/// Constructs and spawns the actor at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instigator"></param>
		public void SpawnActorDeferred<T>(Actor instigator) where T : Actor
		{
            //_bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, typeof(T), this))));
		}

		public void SpawnActorDeferred<T>() where T : Actor
		{
            //_bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, typeof(T), this))));
		}

		/// <summary>
		/// Constructs and spawns the actor at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <param name="instigator"></param>
		/// <param name="actorType"></param>
		public void SpawnActorDeferred(Actor instigator, Type actorType)
		{
			//if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) 
            //    return;
            //_bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(instigator, actorType, this))));
		}

		/// <summary>
		/// Constructs and spawns the actor at the end of the current Tick. Actor will tick next LevelTick.
		/// </summary>
		/// <param name="actorType"></param>
		public void SpawnActorDeferred(Type actorType)
		{
			//if (!actorType.IsSubclassOf(typeof(Actor)) && actorType != typeof(Actor)) 
            //    return;
            //_bus.Publish(new RegisterEventMessage(this, new SpawnActorEvent<SpawnActorParams>(new SpawnActorParams(this, actorType, this))));
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

        public void DestroyActor(Actor instigator, Actor actor)
	    {
            _bus.Publish(new RegisterEventMessage(this, new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(instigator, actor))));
		}

		public void DestroyActor(Actor actor)
		{
            _bus.Publish(new RegisterEventMessage(this, new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, actor))));
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
