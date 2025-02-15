using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using ZEngine.Engine.Messaging;
using ZEngine.Engine.Utility;
namespace ZEngine.Engine.Game
{
	public class Level : ITickable
    {
		public ulong ActorIDCounter { get; private set; } = 0;
		public bool CanTick { get; set; }
		//private readonly List<Actor> _actors = new();
        private IMessageBus _bus;
	    //internal ReadOnlyCollection<Actor> Actors => new(_actors);
	    /// <summary>
	    /// Bounds of this level. To get actual height and width, multiply the X and Y value by 2.
	    /// </summary>
	    public Vector2 Bounds { get; set; } = new(float.MaxValue / 2.0f, float.MaxValue / 2.0f);
        public GameMode GameMode { get; set; } = new();
	    public bool Loaded { get; set; }
		//public List<PlayerController> Players { get; } = new();
	    public TimerManager TimerManager { get; } = new();
        
        protected internal virtual void OnLevelLoad()
        {
            InitLevel();
            Log.Info("Level Loaded", DebugLogCategories.Engine);
            // foreach (var actor in _actors)
            // {
            //     actor.InitializeActor();
            // }
            OnGameStart();
        }
        
        protected internal virtual void InitLevel()
        {
            Log.Info("Initiating Level", DebugLogCategories.Engine);
            //_bus = Core.Engine.Instance.GetService<IEngineMessageBus>();
        }
        
		protected internal virtual void OnGameStart()
        {
            GameMode.OnGameStart();
            // foreach (var actor in _actors)
            // {
            //     actor.OnGameStart();
            // }
            // foreach (var pc in Players)
            // {
            //     pc.OnGameStart();
            // }
		}
		
		void ITickable.Tick(float deltaTime)
		{
			Tick(deltaTime);
		}

		protected internal virtual void Tick(float deltaTime)
        {
	        //Debug.LogDebug("Level Tick!", DebugLogCategories.Engine);
			// foreach (var pc in Players)
			// {
			// 	if (!pc.CanTick) 
   //                  continue;
			// 	pc.Tick(deltaTime);
			// }
			// foreach (var actor in _actors)
   //          {
	  //           if (!actor.CanTick) 
   //                  continue;
			// 	actor.Tick(deltaTime);
   //          }
	        if (GameMode.CanTick) GameMode.Tick(deltaTime);
	        if (TimerManager.CanTick) TimerManager.Tick(deltaTime);
        }
        
        protected internal virtual void LevelDraw()
        {
			//foreach (var pc in Players)
	        //{
				/*
				renderWindow.SetView(pc.PlayerCamera);
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
			//}
		}
        
        protected internal virtual void OnGamePause()
	    {
		    CanTick = false;
			GameMode.OnGamePause();
			// foreach (var pc in Players)
			// {
   //              pc.OnGamePause();
			// }
			// foreach (var actor in _actors)
			// {
			// 	actor.OnGamePause();
			// }
		}
        
        protected internal virtual void OnGameResume()
	    {
            GameMode.OnGameResume();
			// foreach (var pc in Players)
			// {
   //              pc.OnGameResume();
			// }
			// foreach (var actor in _actors)
			// {
			// 	actor.OnGameResume();
			// }
		}
        
        protected internal virtual void OnGameEnd()
	    {
			GameMode.OnGameEnd();
			CanTick = false;
			// foreach (var pc in Players)
			// {
			// 	pc.OnGameEnd();
			// }
			// foreach (var actor in _actors)
			// {
			// 	actor.OnGameEnd();
			// }
		}
	    
	    internal virtual void ShutdownLevel()
	    {
		    Log.Info("Shutting down Level", DebugLogCategories.Engine);
		    Loaded = false;
		    //UnregisterActors();
	    }
		
		// public void RegisterActor(Actor actorOld)
		// {
		// 	if (ContainsActorInLevel(actorOld)) 
  //               return;
		// 	actorOld.ActorID = ActorIDCounter;
		// 	++ActorIDCounter;
		// 	actorOld.LevelID = LevelID;
		// 	actorOld.LevelOldReference = this;
		// 	Debug.LogDebug("Trying to register Actor: " + actorOld, DebugLogCategories.Engine);
		// 	_actors.Add(actorOld);
		// }
		//
	 //    public void UnregisterActors()
	 //    {
		// 	Debug.LogDebug("Removing all Actors!", DebugLogCategories.Engine);
		// 	foreach (var actor in _actors)
		// 	{
		// 		actor.OnActorDestroy();
		// 		foreach (var comp in actor.Components)
		// 		{
		// 			/*
		// 			var physComp = comp as PhysicsComponent;
		// 			if (physComp != null)
		// 			{
		// 				PhysicsWorld.UnregisterPhysicsComponent(physComp);
		// 			}
		// 			*/
		// 		}
		// 	}
		//     _actors.Clear();
	 //    }
	 //    
		// public bool UnregisterActor(Actor actorOld)
		// {
		// 	Debug.LogDebug("Trying to remove Actor: " + actorOld, DebugLogCategories.Engine);
		// 	actorOld.OnActorDestroy();
		// 	var removal = _actors.Remove(actorOld);
		// 	foreach (var comp in actorOld.Components)
		// 	{
		// 		/*
		// 		var physComp = comp as PhysicsComponent;
		// 		if (physComp != null)
		// 		{
		// 			PhysicsWorld.UnregisterPhysicsComponent(physComp);
		// 		}
		// 		*/
		// 	}
		// 	return removal;
		// }
		//
  //       public bool ContainsActorInLevel(string name)
	 //    {
		//     return FindActorInLevel(name) != null;
	 //    }
  //       
	 //    public bool ContainsActorInLevel(Actor actorOld)
	 //    {
		//     return _actors.Contains(actorOld);
	 //    }
	 //    
		// public Actor FindActorInLevel(string name)
	 //    {
		//     return _actors.Find(x => x.ActorName == name);
	 //    }
		//
		// public T FindActorInLevel<T>(string name) where T : Actor
		// {
		// 	return (T)_actors.Find(x => x.ActorName == name);
		// }
		//
		// public Actor FindActorInLevel(uint id)
	 //    {
		//     return _actors.Find(x => x.ActorID == id);
		// }
		//
		// public T FindActorInLevel<T>(uint id) where T : Actor
		// {
		// 	return (T)_actors.Find(x => x.ActorID == id);
		// }
		//
		// public IEnumerable<Actor> FindActorsInLevel(string name)
	 //    {
		//     return _actors.FindAll(x => x.ActorName == name);
		// }
		//
		// public IEnumerable<T> FindActorsInLevel<T>(string name) where T : Actor
		// {
		// 	return _actors.FindAll(x => x.ActorName == name).Cast<T>();
		// }
		//
		// public IEnumerable<Actor> FindActorsInLevel(Type actor)
	 //    {
		//     return _actors.FindAll(x => x.GetType() == actor);
		// }
		//
		// public IEnumerable<T> FindActorsInLevel<T>() where T : Actor
		// {
		// 	return _actors.FindAll(x => x is T).Cast<T>();
		// }
	}
}