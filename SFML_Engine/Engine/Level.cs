using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SFML.Graphics;
using SFML.System;
using SFML_Engine.Engine.Physics;
using CircleShape = SFML_Engine.Engine.SFML.Graphics.CircleShape;
using RectangleShape = SFML_Engine.Engine.SFML.Graphics.RectangleShape;

namespace SFML_Engine.Engine
{
	public class Level : IGameInterface
    {

	    public uint LevelID { get; internal set; } = 0;

	    internal List<Actor> Actors { get; set; } = new List<Actor>();

	    public CircleShape CollisionCircle { get; set; } = new CircleShape(10.0f);
	    public RectangleShape CollisionRectangle { get; set; } = new RectangleShape(new Vector2f(10.0f,10.0f));

        public Engine Engine { get; set; }

        public GameMode GameMode { get; set; } = new GameMode();

	    internal bool LevelTicking { get; set; } = false;

        public Level()
        {
        }


        internal void LevelTick(float deltaTime)
        {
            //Console.WriteLine("Level Tick!");
            foreach (var actor in Actors)
            {
                actor.Tick(deltaTime);
            }

	        GameMode.Tick(deltaTime);
        }

        internal void LevelDraw(ref RenderWindow renderWindow)
        {
            foreach (var actor in Actors)
            {
                var drawableActor = actor as SpriteActor;
                if (drawableActor != null && drawableActor.Visible)
                {
					if (drawableActor.CollisionShape.ShowCollisionShape && drawableActor.CollisionShape.GetType() == typeof(BoxShape))
					{
						CollisionRectangle.Size = ((BoxShape)drawableActor.CollisionShape).BoxExtent;

						CollisionRectangle.Position = actor.Position;
						CollisionRectangle.FillColor = drawableActor.CollisionShape.CollisionShapeColor;

						renderWindow.Draw(CollisionRectangle);
					}
					else if (drawableActor.CollisionShape.ShowCollisionShape && drawableActor.CollisionShape.GetType() == typeof(SphereShape))
					{
						CollisionCircle.Radius = ((SphereShape)drawableActor.CollisionShape).SphereDiameter / 2.0f;

						CollisionCircle.Position = actor.Position;
						CollisionCircle.FillColor = drawableActor.CollisionShape.CollisionShapeColor;

						renderWindow.Draw(CollisionCircle);
					}

					renderWindow.Draw(drawableActor);
                }
            }
		}

        public void RegisterActor(Actor actor)
        {
			actor.ActorID = Engine.ActorIDCounter;
	        ++Engine.ActorIDCounter;
	        actor.LevelID = (int)LevelID; // Maybe use int in the first place?
	        actor.LevelReference = this;
			Console.WriteLine("Trying to register Actor: " + actor.ActorName + "-" + actor.ActorID);
			Actors.Add(actor);
        }

		public bool UnregisterActor(Actor actor)
		{
			Console.WriteLine("Trying to remove Actor: " + actor.ActorName + "-" + actor.ActorID);
			actor.OnActorDestroy();
			var removal = Actors.Remove(actor);
			removal = Engine.PhysicsEngine.RemoveActorFromGroups(actor);
			return removal;
		}

		public bool UnregisterActor(uint actorID)
		{
			Console.WriteLine("Trying to remove Actor with ActorID: #" + actorID);
			var actor = FindActorInLevel(actorID);
			return UnregisterActor(actor);
		}

		public void OnGameStart()
	    {
			GameMode.LevelReference = this;
		    GameMode.OnGameStart();
		    foreach (var actor in Actors)
		    {
			    actor.OnGameStart();
		    }
	    }

	    public void OnGamePause()
	    {
		    throw new NotImplementedException();
			//TODO: Call OnGamePause event on all actors
	    }

	    public void OnGameEnd()
	    {
			GameMode.OnGameEnd();
		    LevelTicking = false;
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
	}
}
