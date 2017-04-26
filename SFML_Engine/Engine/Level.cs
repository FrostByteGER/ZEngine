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
	    public CircleShape CollisionSphere { get; set; } = new CircleShape(10.0f);
	    public RectangleShape CollisionBox { get; set; } = new RectangleShape(new Vector2f(10.0f,10.0f));
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
						CollisionBox.Size = ((BoxShape)drawableActor.CollisionShape).BoxExtent;

						CollisionBox.Position = actor.Position;
						CollisionBox.FillColor = drawableActor.CollisionShape.CollisionShapeColor;

						renderWindow.Draw(CollisionBox);
					}
					else if (drawableActor.CollisionShape.ShowCollisionShape && drawableActor.CollisionShape.GetType() == typeof(SphereShape))
					{
						CollisionSphere.Radius = ((SphereShape)drawableActor.CollisionShape).SphereDiameter / 2.0f;

						CollisionSphere.Position = actor.Position;
						CollisionSphere.FillColor = drawableActor.CollisionShape.CollisionShapeColor;

						renderWindow.Draw(CollisionSphere);
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
			actor.ActorID = Engine.ActorIDCounter;
	        ++Engine.ActorIDCounter;
	        actor.LevelID = (int)LevelID; // Maybe use int in the first place?
			Console.WriteLine("Trying to register Actor: " + actor.ActorName + " ActorID: " + actor.ActorID);
			Actors.Add(actor);
        }

		public bool UnregisterActor(Actor actor)
		{
			Console.WriteLine("Trying to remove Actor: " + actor.ActorName + " ActorID: " + actor.ActorID);
			return Actors.Remove(actor);
		}

		public int UnregisterActor(uint actorID)
		{
			Console.WriteLine("Trying to remove Actor with ActorID: " + actorID);
			return Actors.RemoveAll(actor => actor.ActorID == actorID); // Guaranteed to be unique
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
	}
}
