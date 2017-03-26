using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public class Level
    {

        public List<ITickable> Actors{ get; set; }

        public Level()
        {
            Actors = new List<ITickable>();
        }


        internal void LevelTick(float deltaTime)
        {
            Console.WriteLine("Level Tick!");
            foreach (ITickable actor in Actors)
            {
                actor.Tick(deltaTime);
            }
        }

        internal void LevelDraw(ref RenderWindow renderWindow)
        {
            var actors = Actors.Cast<Drawable>().ToList();
            foreach (var actor in actors)
            {
                renderWindow.Draw(actor);
            }
        }

        public void RegisterActor(ITickable actor)
        {
            Actors.Add(actor);
        }

    }
}
