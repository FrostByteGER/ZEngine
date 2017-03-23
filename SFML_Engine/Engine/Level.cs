using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public class Level
    {

        private List<ITickableInterface> Actors;

        public Level()
        {
            Actors = new List<ITickableInterface>();
        }


        internal void LevelTick(double deltaTime)
        {
            Console.WriteLine("Level Tick!");
            foreach (ITickableInterface actor in Actors)
            {
                actor.Tick(deltaTime);
            }
        }

        public void RegisterActor(ITickableInterface actor)
        {
            Actors.Add(actor);
        }

    }
}
