using System;
using System.Collections.Generic;
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
        public List<Actor> Actors{ get; set; } = new List<Actor>();
		private List<Actor> RemoveActer { get; set;} = new List<Actor>();

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
                if (drawableActor != null)
                {
					if (drawableActor.CollisionShape.ShowCollisionShape && drawableActor.CollisionShape.GetType() == typeof(BoxShape))
					{
						BoxShape box = (BoxShape)drawableActor.CollisionShape;

						RectangleShape hit = new RectangleShape(new Vector2f(box.BoxExtent.X, box.BoxExtent.Y));

						hit.Position = actor.Position;
						hit.FillColor = box.CollisionShapeColor;

						renderWindow.Draw(hit);
					}
					else if (drawableActor.CollisionShape.ShowCollisionShape && drawableActor.CollisionShape.GetType() == typeof(SphereShape))
					{
						SphereShape sphere = (SphereShape)drawableActor.CollisionShape;

						CircleShape hit = new CircleShape(sphere.SphereDiameter/2.0f);

						hit.Position = actor.Position;
						hit.FillColor = sphere.CollisionShapeColor;

						renderWindow.Draw(hit);
					}

					renderWindow.Draw(drawableActor);
                }
            }

			//Remove Acter from Game 
			foreach (var actor in RemoveActer)
			{
				Engine.PhysicsEngine.RemoveActorFromGroup(actor);

				Console.WriteLine(Actors.Remove(actor));
				
			}

			RemoveActer.Clear();
		}

        public void RegisterActor(Actor actor)
        {
			actor.ID = Engine.ActorIDCounter;
	        ++Engine.ActorIDCounter;
	        actor.LevelID = (int)LevelID; // Maybe use int in the first place?
			Console.WriteLine("Trying to register Actor: " + actor.ActorName + " ID: " + actor.ID);
			Actors.Add(actor);
        }

		public bool UnregisterActor(Actor actor)
		{
			Console.WriteLine("Trying to remove Actor: " + actor.ActorName + " ID: " + actor.ID);
			return Actors.Remove(actor);
		}

		public int UnregisterActor(uint actorID)
		{
			Console.WriteLine("Trying to remove Actor with ID: " + actorID);
			return Actors.RemoveAll(actor => actor.ID == actorID); // Guaranteed to be unique
		}

		public void OnGameStart()
	    {
			GameMode.LevelReference = this;
		    GameMode.OnGameStart();
			//TODO: Call OnGameStart event on all preregistered actors
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
		    //TODO: Call OnGamePause event on all actors
	    }

		public Actor FindActorInLevel(string name)
	    {
		    return Actors.Find(x => x.ActorName == name);
	    }

	    public Actor FindActorInLevel(uint id)
	    {
		    return Actors.Find(x => x.ID == id);
		}

	    public List<Actor> FindActorsInLevel(string name)
	    {
		    return Actors.FindAll(x => x.ActorName == name);
		}

	    public List<Actor> FindActorsInLevel(Type actor)
	    {
		    return Actors.FindAll(x => x.GetType() == actor);
		}
	}
}
