using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace SFML_Engine.Engine
{
    public class Level
    {

        public List<Actor> Actors{ get; set; } = new List<Actor>();

        public Engine Engine { get; set; }

        public GameMode GameMode { get; private set; } = new GameMode();

        internal bool LevelTicking { get; set; }

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
					if (drawableActor.CollisionShape.show && drawableActor.CollisionShape.GetType() == typeof(BoxShape))
					{
						BoxShape box = (BoxShape)drawableActor.CollisionShape;

						RectangleShape hit = new RectangleShape(new SFML.System.Vector2f(box.BoxExtent.X, box.BoxExtent.Y));

						hit.Position = actor.Position;

						renderWindow.Draw(hit);
					}
					else if (drawableActor.CollisionShape.show && drawableActor.CollisionShape.GetType() == typeof(SphereShape))
					{
						SphereShape sphere = (SphereShape)drawableActor.CollisionShape;

						CircleShape hit = new CircleShape(sphere.SphereDiameter/2.0f);

						hit.Position = actor.Position;

						renderWindow.Draw(hit);
					}

					renderWindow.Draw(drawableActor);
                }
            }
        }

        public void RegisterActor(Actor actor)
        {
            Actors.Add(actor);

        }
    }
}
