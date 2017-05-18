using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Events;
using SFML_Engine.Engine.Physics;
using CircleShape = SFML_Engine.Engine.SFML.Graphics.CircleShape;
using RectangleShape = SFML_Engine.Engine.SFML.Graphics.RectangleShape;
using Text = SFML_Engine.Engine.SFML.Graphics.Text;

namespace SFML_Engine.Engine
{
	public class Level : IGameInterface
    {

	    public uint LevelID { get; internal set; } = 0;

		public uint ActorIDCounter { get; private set; } = 0;

		internal List<Actor> Actors { get; set; } = new List<Actor>();

	    public CircleShape CollisionCircle { get; set; } = new CircleShape(10.0f);
	    public RectangleShape CollisionRectangle { get; set; } = new RectangleShape(new Vector2f(10.0f,10.0f));

        public Engine EngineReference { get; set; }

        public GameMode GameMode { get; set; } = new GameMode();

	    internal bool LevelTicking { get; set; } = false;

		public List<PlayerController> Players { get; private set; } = new List<PlayerController>();


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
				if (!pc.IsActive) continue;
				pc.Tick(deltaTime);
			}
			foreach (var actor in Actors)
            {
                actor.Tick(deltaTime);
            }

	        GameMode.Tick(deltaTime);
        }

        protected internal virtual void LevelDraw(ref RenderWindow renderWindow)
        {
            foreach (var actor in Actors)
            {
                var drawableActor = actor as SpriteActor;

	            if (drawableActor != null && drawableActor.Visible)
	            {
		            if (drawableActor.CollisionShape.ShowCollisionShape &&
		                drawableActor.CollisionShape.GetType() == typeof(BoxShape))
		            {
			            CollisionRectangle.Size = ((BoxShape) drawableActor.CollisionShape).BoxExtent;

			            CollisionRectangle.Position = actor.Position;
			            CollisionRectangle.FillColor = drawableActor.CollisionShape.CollisionShapeColor;

			            renderWindow.Draw(CollisionRectangle);
		            }
		            else if (drawableActor.CollisionShape.ShowCollisionShape &&
		                     drawableActor.CollisionShape.GetType() == typeof(SphereShape))
		            {
			            CollisionCircle.Radius = ((SphereShape) drawableActor.CollisionShape).SphereDiameter / 2.0f;

			            CollisionCircle.Position = actor.Position;
			            CollisionCircle.FillColor = drawableActor.CollisionShape.CollisionShapeColor;

			            renderWindow.Draw(CollisionCircle);
		            }

		            renderWindow.Draw(drawableActor);
	            }
	            else if(drawableActor == null)
	            {
					var drawableText = actor as Text;
		            if (drawableText != null && drawableText.Visible)
		            {
						renderWindow.Draw(drawableText);
					}
				}
            }
		}

	    public virtual void OnLevelLoad()
	    {
		    Console.WriteLine("Level #" + LevelID + " Loaded");
		    OnGameStart();
	    }

        public void RegisterActor(Actor actor)
        {
			actor.ActorID = ActorIDCounter;
	        ++ActorIDCounter;
	        actor.LevelID = LevelID;
	        actor.LevelReference = this;
			Console.WriteLine("Trying to register Actor: " + actor.ActorName + "-" + actor.ActorID);
			Actors.Add(actor);
        }

		public bool UnregisterActor(Actor actor)
		{
			Console.WriteLine("Trying to remove Actor: " + actor.ActorName + "-" + actor.ActorID);
			actor.OnActorDestroy();
			var removal = Actors.Remove(actor);
			removal = EngineReference.PhysicsEngine.RemoveActorFromGroups(actor);
			return removal;
		}

		public bool UnregisterActor(uint actorID)
		{
			Console.WriteLine("Trying to remove Actor with ActorID: #" + actorID);
			var actor = FindActorInLevel(actorID);
			return UnregisterActor(actor);
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
		    throw new NotImplementedException();
			//TODO: Call OnGamePause event on all actors
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

		public Actor FindActorInLevel(string name)
	    {
		    return Actors.Find(x => x.ActorName == name);
	    }

	    public Actor FindActorInLevel(uint id)
	    {
		    return Actors.Find(x => x.ActorID == id);
		}

	    public List<Actor> FindActorsInLevel(string name)
	    {
		    return Actors.FindAll(x => x.ActorName == name);
		}

	    public List<Actor> FindActorsInLevel(Type actor)
	    {
		    return Actors.FindAll(x => x.GetType() == actor);
		}

	    public ReadOnlyCollection<Actor> GetActors()
	    {
		    return Actors.AsReadOnly();
	    }

	    public void SpawnActor(Actor instigator, Actor actor)
	    {
			Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorEventParams>(new SpawnActorEventParams(instigator, actor, LevelID)));
		}

		public void SpawnActor(Actor actor)
		{
			Engine.Instance.RegisterEvent(new SpawnActorEvent<SpawnActorEventParams>(new SpawnActorEventParams(this, actor, LevelID)));
		}

		public void DestroyActor(Actor instigator, Actor actor)
	    {
			Engine.Instance.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(instigator, actor)));
		}

		public void DestroyActor(Actor actor)
		{
			Engine.Instance.RegisterEvent(new RemoveActorEvent<RemoveActorParams>(new RemoveActorParams(this, actor)));
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
    }
}
