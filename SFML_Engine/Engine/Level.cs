using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace SFML_Engine.Engine
{
    public class Level
    {

        private List<ITickable> Actors;

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

        public void RegisterActor(ITickable actor)
        {
            Actors.Add(actor);
        }

    }
}
